using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourt.Domain.Enums;
using TennisCourt.Domain.Models.Base;

namespace TennisCourt.Domain.Models
{
    public class ReservationStatusHistory : BaseEntity
    {
        public ReservationStatusHistory(ReservationStatusEnum status)
        {
            ReservationStatus = status;
        }
        public ReservationStatusEnum ReservationStatus { get; }

    }
}
