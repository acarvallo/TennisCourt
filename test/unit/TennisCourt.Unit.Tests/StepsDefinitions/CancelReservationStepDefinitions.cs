using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
using TennisCourt.Unit.Tests.API;

namespace TennisCourt.Unit.Tests.StepsDefinitions
{
    [Binding]
    public sealed class CancelReservationStepDefinitions
    {
        private readonly ReservationAPI _reservationAPI;
        private RootOutput<CancelReservationOutput> _output;
        Guid _validReservationId;
        public CancelReservationStepDefinitions(ReservationAPI reservationAPI)
        {
            _reservationAPI = reservationAPI;
        }
        [Given("the id of a existing and active reservation")]
        public async Task GivenValidReservationId()
        {
            var output = await _reservationAPI.ProcessReservation(DateTime.Now, 100);
            _validReservationId = output.Data.ReservationId;
        }
        [When("canceling is requested")]
        public async Task WhenCancelingIsRequested()
        {
            _output = await _reservationAPI.CancelReservation(_validReservationId);
        }
        [Then("resevation status should change to (.*)")]
        public void ThenReservationStatusShouldChange(string reservationStatus)
        {
            _output.Success.Should().BeTrue();
            _output.Data.ReservationStatus.Should().Be(reservationStatus);
        }

    }
}
