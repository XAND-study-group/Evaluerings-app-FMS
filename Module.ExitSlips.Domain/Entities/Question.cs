using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Domain.Entities
{
    public class Question : Entity
    {
        public string Text { get; protected set; }
        private readonly List<Answer> _answers = [];
        public IReadOnlyCollection<Answer> Answers => _answers;
    }
}
