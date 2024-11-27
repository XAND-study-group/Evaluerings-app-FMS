using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Domain.ValueObjects
{
    public record MaxQuestionCount
    {

        public int Value { get; set; }

        public MaxQuestionCount(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value <= 0)
                throw new ArgumentException("The number of questions in an exitslip can't be 0 or negative.");
            if (value > 10)
                throw new ArgumentException("The number of questions in an exitslip can't exceed 10.");
        }

        public static implicit operator MaxQuestionCount(int value)
            => new(value);

        public static implicit operator int(MaxQuestionCount value)
            => value.Value;
    }
}
