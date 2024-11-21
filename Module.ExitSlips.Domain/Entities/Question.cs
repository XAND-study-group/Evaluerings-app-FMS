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
        private readonly List<Answer> _answers = new();
        public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

        public Question(string text)
        {
            Text = text;
        }

        public void AddAnswer(Answer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer), "Answer cannot be null.");
            }

            _answers.Add(answer);
        }

        public void RemoveAnswer(Answer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer), "Answer cannot be null.");
            }

            _answers.Remove(answer);
        }

        public void ClearAnswers()
        {
            _answers.Clear();
        }

        public void UpdateText(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
            {
                throw new ArgumentException("Question text cannot be null or empty.", nameof(newText));
            }

            Text = newText;
        }
    }
}
