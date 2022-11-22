using TennisCourt.Domain.Enums;
using TennisCourt.Domain.Models.Base;

namespace TennisCourt.Domain.Models
{
    public class ReservationStatusHistory : BaseEntity
    {
        private ReservationStatusHistory() { }
        public ReservationStatusHistory(ReservationStatusEnum status)
        {
            ReservationStatus = status;
        }
        public ReservationStatusEnum ReservationStatus { get; }
        public Reservation Reservation { get; }
        public Guid ReservationId { get; }
    }
}
