using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.ValueObjects
{
    public sealed class SubjectDescription : IEquatable<SubjectDescription>
    {
        public string Value { get; init; }

        public SubjectDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty or whitespace.", nameof(value));

            if (value.Length > 500)
                throw new ArgumentException("Description cannot be longer than 500 characters.", nameof(value));

            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is SubjectDescription description && Value == description.Value;
        }

        public bool Equals(SubjectDescription? other)
        {
            return other != null && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator string(SubjectDescription description) => description.Value;
        public static implicit operator SubjectDescription(string description) => new(description);
    }
}
