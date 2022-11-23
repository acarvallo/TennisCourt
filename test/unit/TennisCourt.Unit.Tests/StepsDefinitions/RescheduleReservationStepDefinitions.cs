using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.RescheduleReservation;
using TennisCourt.Unit.Tests.API;

namespace TennisCourt.Unit.Tests.StepsDefinitions
{
    [Binding]
    public sealed class RescheduleReservationStepDefinitions
    {
        private readonly ReservationAPI _reservationAPI;
        private RootOutput<RescheduleReservationOutput> _output;
        private Guid _reservationId;
        private DateTime _reservationDate;
        private DateTime _rescheduleDate;
        public RescheduleReservationStepDefinitions(ReservationAPI reservationAPI)
        {
            _reservationAPI = reservationAPI;
        }

        [Given("the id of existing and active reservation and with date (.*) ahead and new date available to reschedule")]
        public async Task GivenIdReservationAndAvailableDate(int addDays)
        {
            _reservationDate = DateTime.Today.AddDays(addDays);
            _rescheduleDate = _reservationDate.AddDays(addDays);
            var output = await _reservationAPI.ProcessReservation(_reservationDate, 100);
            _reservationId = output.Data.ReservationId;
        }
        [When("reschedule reservation is requested")]
        public async Task WhenRescheduleRequested()
        {
           _output = await _reservationAPI.RescheduleReservation(_reservationId, _rescheduleDate);
        }
        [Then("same reservation id with status (.*) and date changed")]
        public void ThenReservationStatusChanged(string status)
        {
            _output.Success.Should().BeTrue();
            _output.Data.ReservationId.Should().Be(_reservationId);
            _output.Data.ReservationStatus.Should().Be(status);
            _output.Data.ReservedDate.Should().Be(_rescheduleDate);
        }
        [Given("the id of existing and active reservation and with date (.*) ahead and new date unavailable to reschedule")]
        public async Task GivenIdReservationAndUnavailableDate(int addDays)
        {
            _reservationDate = DateTime.Today.AddDays(addDays);
            _rescheduleDate = _reservationDate.AddDays(addDays);

            await _reservationAPI.ProcessReservation(_rescheduleDate, 100);
            var output = await _reservationAPI.ProcessReservation(_reservationDate, 100);
            _reservationId = output.Data.ReservationId;
        }
        [Then("should return error message (.*)")]
        public void ShoudReturnErrorMessage(string message)
        {
            _output.Success.Should().BeFalse();
            _output.Messages.Should().Contain(message);
        }
    }
}
