﻿using Microsoft.AspNetCore.Mvc;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Application.Interface;

namespace TennisCourt.Api.Controllers
{
    [Route("reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationAppService _reservationService;

        public ReservationController(IReservationAppService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(ProcessReservationOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult>Post(ProcessReservationInput input)
        {
            return Ok(await _reservationService.ProcessReservation(input));
        }
    }
}