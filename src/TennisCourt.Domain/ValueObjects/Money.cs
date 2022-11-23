using System.Globalization;
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
        public override string ToString()
        {
            return Value.ToString($"C", CultureInfo.CurrentCulture);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static Money Create(Money money) => new(money.Value);

        public static explicit operator decimal(Money money) => money.Value;

        public static explicit operator Money(decimal value) => new(value);
        public static bool operator ==(Money right, decimal left) => right.Value == left;
        public static bool operator !=(Money right, decimal left) => right.Value != left;
        public static bool operator <=(Money right, decimal left) => right.Value <= left;
        public static bool operator >=(Money right, decimal left) => right.Value >= left;
        public static bool operator >(Money right, decimal left) => right.Value > left;
        public static bool operator <(Money right, decimal left) => right.Value < left;


    }
}
