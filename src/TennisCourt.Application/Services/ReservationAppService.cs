using AutoMapper;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
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

            return ConvertToOutput<CancelReservationOutput, Reservation>(domainResult);

        }

        public Task<Reservation> GetReservation(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<RootOutput<ProcessReservationOutput>> ProcessReservation(ProcessReservationInput input)
        {
            var reservationsByDate = await _repository.GetByDate(input.RequestedDate);

            var domainResult = _reservationManager.ProcessReservation(input.RequestedDate, input.Amount, reservationsByDate.ToList());

            if (domainResult.IsValid())
            {
                await _repository.AddAsync(domainResult.Entity);
            }
            return ConvertToOutput<ProcessReservationOutput, Reservation>(domainResult);
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
            return ConvertToOutput<RescheduleReservationOutput,Reservation>(domainResult);

        }
        private RootOutput<TOutput> ConvertToOutput<TOutput, TEntity>(DomainResult<TEntity> domainResult) where TEntity : BaseEntity
        {
            if (domainResult.IsValid())
            {
                var dataOutput = _mapper.Map<TOutput>(domainResult.Entity);
                return RootOutput<TOutput>.Sucess<TOutput>(dataOutput);
            }

            return RootOutput<TOutput>.WithErrors(domainResult.Errors);
        }
    }
}