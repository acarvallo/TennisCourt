using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourt.Domain.ValueObjects.Base;

namespace TennisCourt.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        Money(decimal value)
        {
            Value = value;
        }
        public decimal Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static explicit operator decimal(Money money) => money.Value;

        public static explicit operator Money(decimal value) => new(value);

    }
}
