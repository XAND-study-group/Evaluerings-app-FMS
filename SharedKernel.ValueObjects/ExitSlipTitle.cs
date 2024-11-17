using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.ValueObjects
{
    public sealed class ExitSlipTitle : IEquatable<ExitSlipTitle>
    {
        public string Value { get; init; }

        public ExitSlipTitle(string value)
        {
            Value = value;
        }

        bool IEquatable<ExitSlipTitle>.Equals(ExitSlipTitle? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString();
        }



    }
}
