using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourt.Application.DTO.ProcessReservation
{
    public class ProcessReservationInput
    {
        public DateTime RequestedDate { get; set; }
        public decimal Amount { get; set; }
    }
}
