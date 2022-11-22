using AutoMapper;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Application.Interface;
using TennisCourt.Domain.Interfaces.Repositories;
using TennisCourt.Domain.Models;
using TennisCourt.Infra.CrossCutting.Commons.Extensions;

namespace TennisCourt.Application.Services
{
    public class ReservationAppService : IReservationAppService
    {

        private readonly IReservationRepository _repository;
        private readonly IMapper _mapper;

        public ReservationAppService(IReservationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<Reservation> CancelReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation> GetReservation(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessReservationOutput> ProcessReservation(ProcessReservationInput input)
        {
            Reservation newReservation = new(input.RequestedDate, input.Amount);
            await _repository.AddAsync(newReservation);
            return _mapper.Map<Reservation, ProcessReservationOutput>(newReservation);
        }

        public Task<Reservation> RescheduleReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}