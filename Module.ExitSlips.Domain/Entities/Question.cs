using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities
{
    public class Question : Entity
    {
        #region Properties

        public Text Text { get; protected set; }
        public Guid UserId { get; protected set; }
        private readonly List<Answer> _answers = new();
        public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

        #endregion

        #region Constructors

        protected Question() { }
        private Question(Guid exitSlipId, string text, Guid userId)
        {
            Text = text;
            UserId = userId;
        }

        public static Question Create(Guid exitSlipId, string text, Guid userId)
            => new Question(exitSlipId, text, userId);

        #endregion

        #region Methods

        #region AnswerHandling

        public Answer AddAnswer(string text, Guid userId)
        {
            var answer = Answer.Create(Id, text, userId);
            _answers.Add(answer);
            return answer;
        }

        public Answer UpdateAnswer(Guid answerId, Guid userId, string newText)
        {
            var answer = _answers.FirstOrDefault(a => a.Id == answerId) ??
                throw new InvalidOperationException("Svar ikke fundet");
            answer.UpdateAnswer(newText, userId);
            return answer;
        }

        public void DeleteAnswer(Guid answerId)
        {
            var answer = _answers.FirstOrDefault(a => a.Id == answerId);
            if (answer is null)
                throw new InvalidOperationException("Svar ikke fundet");
            _answers.Remove(answer);
        }

        #endregion

        #region QuestionHandling

        public void UpdateQuestion(string newText)
        {
            Text = newText;
        }

        #endregion

        #endregion
    }
}

