using System;
using System.Collections;
using System.Collections.Generic;
using TennisCourt.Domain.Enums;
using TennisCourt.Domain.Interfaces;
using TennisCourt.Domain.Models.Base;

namespace TennisCourt.Domain.Models
{
    public class Reservation : BaseEntity, IAggregateRoot
    {
        public Reservation(DateTime reservedDate,decimal value)
        {
            ReservedDate = reservedDate;
            Value = value;
        }
        public decimal Value { get; }
        public decimal RefundValue { get; }
        public DateTime ReservedDate { get; }
        public ReservationStatusHistory ReservationStatus => ReservationHistory.SingleOrDefault(p => p.IsActive());
        private IList<ReservationStatusHistory> ReservationHistory { get; set; }
    }
}
