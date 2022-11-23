using Microsoft.AspNetCore.Mvc;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
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
        [ProducesResponseType(typeof(RootOutput<ProcessReservationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] ProcessReservationInput input)
        {
            var output = await _reservationService.ProcessReservation(input);
            return Result(output);
        }

        [HttpPut("{id}/cancel")]
        [ProducesResponseType(typeof(RootOutput<CancelReservationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromRoute] Guid id)
        {
            var input = new CancelReservationInput { ReservationId = id };
            var output = await _reservationService.CancelReservation(input);
            return Result(output);
        }

        private IActionResult Result<TOutput>(RootOutput<TOutput> output)
        {
            if (output.Success)
                return Ok(output);
            return BadRequest(output);
        }
    }
}