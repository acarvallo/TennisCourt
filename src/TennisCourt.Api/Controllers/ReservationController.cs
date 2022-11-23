using Microsoft.AspNetCore.Mvc;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
using TennisCourt.Application.DTO.GetReservation;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Application.DTO.RescheduleReservation;
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


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RootOutput<GetReservationOuput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var output = await _reservationService.GetReservation(id);
            return Result(output);
        }

        [HttpPost("reschedule")]
        [ProducesResponseType(typeof(RootOutput<RescheduleReservationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] RescheduleReservationInput input)
        {
            var output = await _reservationService.RescheduleReservation(input);
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