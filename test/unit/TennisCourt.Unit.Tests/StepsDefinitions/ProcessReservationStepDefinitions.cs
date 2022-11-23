using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Unit.Tests.API;

namespace TennisCourt.Unit.Tests.StepsDefinitions
{

    [Binding]
    public sealed class ProcessReservationStepDefinitions
    {
        private DateTime _selectedReservationDate;
        private decimal _amount;
        private RootOutput<ProcessReservationOutput> _output;

        private readonly ReservationAPI _reservationAPI;

        public ProcessReservationStepDefinitions(ReservationAPI reservationAPI)
        {
            _reservationAPI = reservationAPI;
        }
        [Given(@"selected date D plus (.*) and amount of (.*)")]
        public void GivenSelectedDate(int addDays, decimal amount)
        {
           _selectedReservationDate = DateTime.Today.AddDays(addDays);
            _amount = amount;
        }
        [When("reservation is requested")]
        public async Task WhenRservationRequested()
        {
            _output = await _reservationAPI.ProcessReservation(_selectedReservationDate, _amount);
        }
        [Then("process reservation returing a valid GUID reservation id")]
        public void ThenReservationNewID()
        {
            _output.Success.Should().BeTrue();
            _output.Data.Should().NotBeNull();
            _output.Data.ReservationId.Should<Guid>().NotBeNull();
        }
        [When("date selected is already reserved")]
        public async Task WhenDateIsNotAvailable()
        {
            await _reservationAPI.ProcessReservation(_selectedReservationDate, _amount);
            _output = await _reservationAPI.ProcessReservation(_selectedReservationDate, _amount);
        }
        [Then("process reservation should return not available error message")]
        public void ThenShouldReturnError()
        {
            _output.Messages.Should().Contain("Date not availabe");
        }

        [Then("process reservation should return invalid date error message")]
        public void ThenShouldReturnInvalidDateError()
        {
            _output.Messages.Should().Contain("Invalid reserved date");
        }

        [Then("process reservation should return invalid amount error message")]
        public void ThenShouldReturnInvalidAmountError()
        {
            _output.Messages.Should().Contain("Amount can't be zero or less than zero");
        }
    }
}
