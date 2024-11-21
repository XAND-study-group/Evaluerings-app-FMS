using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedKernel.ValueObjects
{
    public sealed class UserEmail : IEquatable<UserEmail>
    {
        public string Value { get; init; }

        private UserEmail()
        {
            
        }
        private UserEmail(string value, IEnumerable<string> otherUsersEmails)
        {
            Validate(value, otherUsersEmails);
            Value = value;
        }

        public static UserEmail Create(string value, IEnumerable<string> otherUsersEmails)
            => new UserEmail(value, otherUsersEmails);

        private void Validate(string value, IEnumerable<string> otherUsersEmails)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty or whitespace", nameof(value));

            if (value.Length > 254)
                throw new ArgumentException("Email cannot exceed 254 characters", nameof(value));

            if (otherUsersEmails.Any(otherEmails => otherEmails == value))
                throw new ArgumentException($"A User with email '{value}' already exists.");

            if (!IsValidEmail(value))
                throw new ArgumentException("Email format is invalid", nameof(value));


            string normalizedEmail = NormalizeEmail(value);
        }


        private bool IsValidEmail(string value)
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }

        private string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }
        bool IEquatable<UserEmail>.Equals(UserEmail? other)
        {
            if (other is null)
                return false;

            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj is UserEmail);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(UserEmail value) => value.Value;

        public static bool operator ==(UserEmail left, UserEmail right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(UserEmail left, UserEmail right)
        {
            return !(left == right);
        }


        // this is in case if we wanna hide a part of the email 
        public string LocalPart => Value.Substring(0, Value.IndexOf('@'));
        public string DomainPart => Value.Substring(Value.IndexOf('@') + 1);
    }
}
