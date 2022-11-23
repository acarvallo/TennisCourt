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

            ValidateReservation(newReservation, result);

            if (!result.IsValid())
                return result;

            ValidateDateAvailability(newReservation, reservationsByDate, result);

            if (!result.IsValid())
                return result;

            return result.WithSucess(newReservation);
        }
        public DomainResult<Reservation> CancelReservation(Reservation reservation)
        {
            var result = DomainResult<Reservation>.Create();

            ValidateReservation(reservation, result);

            if (!result.IsValid())
                return result;

            reservation.Cancel();
            reservation.MakeRefund();

            return result.WithSucess(reservation);
        }
        public DomainResult<Reservation> RescheduleReservation(Reservation reservation,
                                                              DateTime newDate,
                                                              IList<Reservation> reservationsByNewDate)
        {
            var result = DomainResult<Reservation>.Create();

            ValidateReservationIsActive(reservation, result);

            if (!result.IsValid())
                return result;

            reservation.UpdateDate(newDate);

            ValidateDateAvailability(reservation, reservationsByNewDate, result);

            if (!result.IsValid())
                return result;

            ValidateReservation(reservation, result);

            if (!result.IsValid())
                return result;

            reservation.SetAsReschedule();

            return result.WithSucess(reservation);

        }
        private void ValidateDateAvailability(Reservation reservation, IList<Reservation> reservationsByDate, DomainResult<Reservation> result)
        {
            if (!IsDateAvailable(reservation, reservationsByDate))
            {
                result.AddMessage("Date not availabe");
            }
        }
        private bool IsDateAvailable(Reservation reservation, IList<Reservation> reservationsByDate)
        {
            return !reservationsByDate.Any(p => p.ReservedDate == reservation.ReservedDate
            && p.ReservationStatus == Enums.ReservationStatusEnum.READY_TO_PLAY);
        }
        private void ValidateReservationIsActive(Reservation reservation, DomainResult<Reservation> result)
        {
            if (reservation.ReservationStatus != Enums.ReservationStatusEnum.READY_TO_PLAY)
            {
                result.AddMessage("Reservation is not active");
 
            }
        }
        private void ValidateReservation(Reservation reservation, DomainResult<Reservation> result)
        {
            var errors = new List<string>();

            if (!reservation.IsValid(errors))
            {
                result.AddMessages(errors);
            }
        }
    }
}
