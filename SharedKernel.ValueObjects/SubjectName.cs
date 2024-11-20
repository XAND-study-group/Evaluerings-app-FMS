using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.ValueObjects
{
    public sealed class SubjectName : IEquatable<SubjectName>
    {
        public string Value { get; init; }

        public SubjectName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));

            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is SubjectName name && Value == name.Value;
        }

        public bool Equals(SubjectName? other)
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
        public static implicit operator string(SubjectName name) => name.Value;
        public static implicit operator SubjectName(string name) => new(name);
    }
}
