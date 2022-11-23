using AutoMapper;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
using TennisCourt.Application.DTO.GetReservation;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Application.DTO.RescheduleReservation;
using TennisCourt.Application.Interface;
using TennisCourt.Domain.Interfaces.Repositories;
using TennisCourt.Domain.Models;
using TennisCourt.Domain.Models.Base;
using TennisCourt.Domain.Services;
using TennisCourt.Infra.CrossCutting.Commons.Extensions;

namespace TennisCourt.Application.Services
{
    public class ReservationAppService : IReservationAppService
    {

        private readonly IReservationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ReservationManager _reservationManager;

        public ReservationAppService(IReservationRepository repository,
                                     IMapper mapper,
                                     ReservationManager reservationManager)
        {
            _repository = repository;
            _mapper = mapper;
            _reservationManager = reservationManager;
        }

        public async Task<RootOutput<CancelReservationOutput>> CancelReservation(CancelReservationInput input)
        {
            var reservation = await _repository.GetByIdAsync(input.ReservationId);

            if (reservation == null)
            {
                return RootOutput<CancelReservationOutput>.WithErrors("Invalid reservation ID");
            }

            var domainResult = _reservationManager.CancelReservation(reservation);

            if (domainResult.IsValid())
            {
               await _repository.UpdateAsync(reservation);
            }

            return ConvertDomainResultToOutput<CancelReservationOutput>(domainResult);

        }

        public async Task<RootOutput<GetReservationOuput>> GetReservation(Guid id)
        {
            var reservation = await _repository.GetByIdAsync(id);

            if (reservation == null)
            {
                return RootOutput<GetReservationOuput>.WithErrors("Invalid reservation ID");
            }

            return MapOutput<GetReservationOuput>(reservation);

        }

        public async Task<RootOutput<ProcessReservationOutput>> ProcessReservation(ProcessReservationInput input)
        {
            var reservationsByDate = await _repository.GetByDate(input.RequestedDate);

            var domainResult = _reservationManager.ProcessReservation(input.RequestedDate, input.Amount, reservationsByDate.ToList());

            if (domainResult.IsValid())
            {
                await _repository.AddAsync(domainResult.Entity);
            }
            return ConvertDomainResultToOutput<ProcessReservationOutput>(domainResult);
        }
        public async Task<RootOutput<RescheduleReservationOutput>> RescheduleReservation(RescheduleReservationInput input)
        {
            var reservation = await _repository.GetByIdAsync(input.ReservationId);
            var reservationsByNewDate = await _repository.GetByDate(input.NewDate);

            if (reservation == null)
            {
                return RootOutput<RescheduleReservationOutput>.WithErrors("Invalid reservation ID");
            }

            var domainResult = _reservationManager.RescheduleReservation(reservation, 
                                                                        input.NewDate,
                                                                         reservationsByNewDate.ToList());
            if (domainResult.IsValid())
            {
                var updatedReservation = domainResult.Entity;
                await _repository.UpdateAsync(updatedReservation);
            }
            return ConvertDomainResultToOutput<RescheduleReservationOutput>(domainResult);

        }
        private RootOutput<TOutput> ConvertDomainResultToOutput<TOutput>(DomainResult<Reservation> domainResult)
        {
            if (domainResult.IsValid())
            {
                return MapOutput<TOutput>(domainResult.Entity);
            }

            return RootOutput<TOutput>.WithErrors(domainResult.Errors);
        }

        private RootOutput<TOutput> MapOutput<TOutput>(Reservation entity)
        {
            var dataOutput = _mapper.Map<TOutput>(entity);
            return RootOutput<TOutput>.Sucess<TOutput>(dataOutput);
        }
    }
}