using System;
using TechTalk.SpecFlow;

namespace TennisCourt.Unit.Tests.StepsDefinitions
{

    [Binding]
    public sealed class ProcessReservationStepDefinitions
    {
        private DateTime _selectedReservationDate;

        [Given("selected date (.*)")]
        public void GivenSelectedDate(DateTime date)
        {
            _selectedReservationDate = date;
        }
        [When("date is available to reservation")]
        public void WhenDateAvailable()
        {

        }
        [Then("process reservation returing a valid GUID reservation id")]
        public void ThenReservationNewID()
        {

        }
    }
}
