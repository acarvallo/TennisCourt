using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourt.Application.DTO.RescheduleReservation
{
    public class RescheduleReservationInput
    {
        public Guid ReservationId { get; set; }
        public DateTime NewDate { get; set; }
    }
}
