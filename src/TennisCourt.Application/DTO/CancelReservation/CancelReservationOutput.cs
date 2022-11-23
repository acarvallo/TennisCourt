using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourt.Application.DTO.CancelReservation
{
    public class CancelReservationOutput
    {
        public Guid ReservationId { get; set; }
        public string ReservationStatus { get; set; }
        public DateTime ReservedDate { get;  set; }
        public string Amount { get; set; }
    }
}
