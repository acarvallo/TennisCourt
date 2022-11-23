using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Infra.CrossCutting.Commons.Extensions;

namespace TennisCourt.Unit.Tests.API
{
    public class ReservationAPI
    {
        private HttpClient _client;
        private HttpResponseMessage _response;
        public ReservationAPI()
        {
            _client = Hooks.Hooks._client;
        }

        public async Task<RootOutput<ProcessReservationOutput>> ProcessReservation(DateTime requestedDate, decimal amount)
        {
            var input = new ProcessReservationInput()
            {
                RequestedDate = requestedDate,
                Amount = amount
            };

            var response = await _client.PostAsync("reservation", ToContent(input));

            return await GetOutput<RootOutput<ProcessReservationOutput>>(response);
        }
        public async Task<RootOutput<CancelReservationOutput>> CancelReservation(Guid id)
        {
            var response = await _client.PutAsync($"reservation/{id}/cancel", null);

            return await GetOutput<RootOutput<CancelReservationOutput>>(response);
        }
        private async Task<TOutput> GetOutput<TOutput>(HttpResponseMessage response)
        {
            return (await response.Content.ReadAsStringAsync()).ToObject<TOutput>();
        }

        private static StringContent ToContent(ProcessReservationInput input)
        {
            var content = new StringContent(input.ToJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}

