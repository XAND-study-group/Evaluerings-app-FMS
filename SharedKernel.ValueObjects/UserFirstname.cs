using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.ValueObjects
{
    public sealed class UserFirstname : IEquatable<UserFirstname>
    {
        public string Value { get; init; }

        private UserFirstname(string value)
        {
            Validate(value);
            Value = value;
        }

        public static UserFirstname Create(string value)
            => new UserFirstname(value);

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Firstname cannot be empty or whitespace", nameof(value));

            if (value.Length > 50)
                throw new ArgumentException("Firstname cannot exceed 50 characters", nameof(value));

            if(!value.All(char.IsLetter))
                throw new ArgumentException("Firstname can only contain letters", nameof(value));

        }

        bool IEquatable<UserFirstname>.Equals(UserFirstname? other)
        {
            if (other is null)
                return false;

            // Case-Insensetive Comparison
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UserFirstname);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }
        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(UserFirstname value) => value.Value;

        public static bool operator == (UserFirstname left, UserFirstname right)
        {
            if (left is null)
               return right is null;

            return left.Equals(right);
        }

        public static bool operator != (UserFirstname left, UserFirstname right)
        {
            return !(left == right);
        }

    }
}
