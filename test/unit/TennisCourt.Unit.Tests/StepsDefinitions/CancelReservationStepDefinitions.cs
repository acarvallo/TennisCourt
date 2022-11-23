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
        [Given("the id of a existing and active reservation of (.*)")]
        public async Task GivenValidReservationId(decimal amount)
        {
            var output = await _reservationAPI.ProcessReservation(DateTime.Now, amount);
            _validReservationId = output.Data.ReservationId;
        }
        [Given("the id of a non existing reservation")]
        public void GivenInvalidReservationId()
        {
            _validReservationId = Guid.NewGuid();
        }

        [When("canceling is requested")]
        public async Task WhenCancelingIsRequested()
        {
            _output = await _reservationAPI.CancelReservation(_validReservationId);
        }
        [Then("resevation status should change to (.*) and Refund same as (.*)")]
        public void ThenReservationStatusShouldChange(string reservationStatus,decimal amount)
        {
            _output.Success.Should().BeTrue();
            _output.Data.ReservationStatus.Should().Be(reservationStatus);
            _output.Data.RefundAmount.Should().Be(amount);
        }
        [Then("should return essage error (.*)")]
        public void ShouldReturnMessageError(string message)
        {
            _output.Success.Should().BeFalse();
            _output.Messages.Should().Contain(message);
        }

    }
}
