using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TennisCourt.Unit.Tests.API;

namespace TennisCourt.Unit.Tests.StepsDefinitions
{
    [Binding]
    public sealed class CancelReservationStepDefinitions
    {
        private readonly ReservationAPI _reservationAPI;
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

        }
        [Then("resevation status should change to (.*)")]
        public void ThenReservationStatusShouldChange(string reservationStatus) 
        { 
        }

    }
}
