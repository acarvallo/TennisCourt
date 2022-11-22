using System;
using System.Collections;
using System.Collections.Generic;
using TennisCourt.Domain.Enums;
using TennisCourt.Domain.Interfaces;
using TennisCourt.Domain.Models.Base;
using TennisCourt.Domain.ValueObjects;

namespace TennisCourt.Domain.Models
{
    public class Reservation : BaseEntity, IAggregateRoot
    {
        private Reservation() { }
        public Reservation(DateTime reservedDate, decimal value)
        {
            ReservedDate = reservedDate;
            Amount = (Money)value;
            ReservationHistory.Add(new(ReservationStatusEnum.READY_TO_PLAY));
        }
        public Money Amount { get; private set; }
        public Money RefundAmount { get; private set; }
        public DateTime ReservedDate { get; }
        public ReservationStatusHistory ReservationStatus => ReservationHistory.Single(p => p.IsActive());
        public IList<ReservationStatusHistory> ReservationHistory { get; private set; }
    }
}
