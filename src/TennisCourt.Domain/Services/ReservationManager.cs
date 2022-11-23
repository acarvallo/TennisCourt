using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourt.Domain.Models;

namespace TennisCourt.Domain.Services
{
    public sealed class ReservationManager
    {
        public DomainResult<Reservation> ProcessReservation(DateTime reservedDate, decimal amount, IList<Reservation> reservationsByDate)
        {
            var result = DomainResult<Reservation>.Create();

            Reservation newReservation = new(reservedDate, amount);

            var errors = new List<string>();
            if (!newReservation.IsValid(errors))
            {
                result.AddMessages(errors);
                return result;
            }
            if(!IsDateAvailable(newReservation,reservationsByDate))
            {
                result.AddMessage("Date not availabe");
                return result;
            }

            return result.WithSucess(newReservation);
        }

        private bool IsDateAvailable(Reservation newReservation, IList<Reservation> reservationsByDate)
        {
            return !reservationsByDate.Any(p => p.ReservedDate == newReservation.ReservedDate
            && p.ReservationStatus == Enums.ReservationStatusEnum.READY_TO_PLAY);
        }
    }
}
