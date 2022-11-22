using System.ComponentModel;

namespace TennisCourt.Domain.Enums
{
    [DefaultValue(READY_TO_PLAY)]
    public enum ReservationStatusEnum
    {
        READY_TO_PLAY=1,
        CANCELED=2,
        RESCHEDULED=3,
        DATE_NOT_AVAILABLE = 4,
    }
}
