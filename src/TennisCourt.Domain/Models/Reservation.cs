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
        public Reservation(DateTime reservedDate, decimal value)
        {
            ReservedDate = reservedDate;
            Value = (Money)value;
        }
        public Money Value { get; private set; }
        public Money RefundValue { get; private set; }
        public DateTime ReservedDate { get; }
        public ReservationStatusHistory ReservationStatus => ReservationHistory.SingleOrDefault(p => p.IsActive());
        public IList<ReservationStatusHistory> ReservationHistory { get; private set; }
    }
}
