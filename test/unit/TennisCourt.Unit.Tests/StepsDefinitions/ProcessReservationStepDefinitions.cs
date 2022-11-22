using FluentAssertions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Infra.CrossCutting.Commons.Extensions;

namespace TennisCourt.Unit.Tests.StepsDefinitions
{

    [Binding]
    public sealed class ProcessReservationStepDefinitions
    {
        private DateTime _selectedReservationDate;
        private decimal _amount;
        private static HttpClient _client;
        private RootOutput<ProcessReservationOutput> _output;
        private ProcessReservationInput _input;
        private HttpResponseMessage _response;
        public ProcessReservationStepDefinitions()
        {
            _client = Hooks.Hooks._client;
        }
        [Given("selected advanced date and amount of (.*)")]
        public void GivenSelectedDate(decimal amount)
        {
            _selectedReservationDate = DateTime.Today.AddDays(5);
            _amount = amount;
        }
        [When("date is available to reservation")]
        public void WhenDateAvailable()
        {
            _input = new ProcessReservationInput()
            {
                RequestedDate = _selectedReservationDate,
                Amount = _amount
            };
            var content = new StringContent(_input.ToJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _response = _client.PostAsync("reservation", content).Result;
        }
        [Then("process reservation returing a valid GUID reservation id")]
        public void ThenReservationNewID()
        {
            _response.EnsureSuccessStatusCode();
            _output =_response.Content.ReadAsStringAsync().Result.ToObject<RootOutput<ProcessReservationOutput>>();
            _output.Success.Should().BeTrue();
            _output.Data.Should().NotBeNull();
            _output.Data.ReservationId.Should<Guid>().NotBeNull();
        }

    }
}
