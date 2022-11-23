using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.GetReservation;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Unit.Tests.API;

namespace TennisCourt.Unit.Tests.StepsDefinitions
{
    [Binding]
    public sealed class GetReservationDetailsStepDefinitions
    {
        private readonly ReservationAPI _reservationAPI;
        ProcessReservationOutput _currentReservation;
        RootOutput<GetReservationOutput> _output;
        public GetReservationDetailsStepDefinitions(ReservationAPI reservationAPI)
        {
            _reservationAPI = reservationAPI;
        }
        [Given("the id of a existing reservation")]
        public async Task GivenIdExistingReservation()
        {
            var output = await _reservationAPI.ProcessReservation(DateTime.Today, 100);
            _currentReservation = output.Data;
        }
        [When("reservation data is requested")]
        public async Task GetReservationData()
        {
            _output = await _reservationAPI.GetReservation(_currentReservation.ReservationId);
        }
        [Then("complete details of valid reservation")]
        public void CompareReservations()
        {
            _output.Success.Should().BeTrue();
            var reservation = _output.Data;

            reservation.ReservationId.Should().Be(_currentReservation.ReservationId);
            reservation.ReservedDate.Should().Be(_currentReservation.ReservedDate);
            reservation.Amount.Should().Be(_currentReservation.Amount);
            reservation.RefundAmount.Should().Be(_currentReservation.RefundAmount);
        }
    }
}
