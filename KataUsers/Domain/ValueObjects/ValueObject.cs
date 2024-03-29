namespace KataUsers.Domain.ValueObjects
{

    /*
        Why does not use records (c# 9) to define value objects?
        https://enterprisecraftsmanship.com/posts/csharp-records-value-objects/
    */

    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return one?.Equals(two) ?? false;
        }

        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return !(one?.Equals(two) ?? false);
        }

        public override string ToString()
        {
            return String.Join(" ", GetEqualityComponents());
        }
    }
}