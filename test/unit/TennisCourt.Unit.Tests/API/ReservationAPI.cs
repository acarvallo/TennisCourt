﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TennisCourt.Api.Requests;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
using TennisCourt.Application.DTO.GetReservation;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Application.DTO.RescheduleReservation;
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
        public async Task<RootOutput<RescheduleReservationOutput>> RescheduleReservation(Guid id, DateTime newDate)
        {
            var input = new RescheduleReservationRequest()
            { NewDate = newDate };

            var response = await _client.PostAsync($"reservation/{id}/reschedule", ToContent(input));

            return await GetOutput<RootOutput<RescheduleReservationOutput>>(response);

        }

        public async Task<RootOutput<CancelReservationOutput>> CancelReservation(Guid id)
        {
            var response = await _client.PutAsync($"reservation/{id}/cancel", null);

            return await GetOutput<RootOutput<CancelReservationOutput>>(response);
        }

        public async Task<RootOutput<GetReservationOutput>> GetReservation(Guid id)
        {
            var response = await _client.GetAsync($"reservation/{id}");
            return await GetOutput<RootOutput<GetReservationOutput>>(response);
        }
        private async Task<TOutput> GetOutput<TOutput>(HttpResponseMessage response)
        {
            return (await response.Content.ReadAsStringAsync()).ToObject<TOutput>();
        }

        private static StringContent ToContent(object input)
        {
            var content = new StringContent(input.ToJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}

