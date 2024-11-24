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
        private readonly List<Answer> _answers = new();
        public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

        #endregion

        #region Constructors

        public Question(Guid exitSlipId, string text)
        {
            Text = text;
        }

        public static Question Create(Guid exitSlipId, string text)
            => new Question(exitSlipId, text);

        #endregion

        #region Methods

        #region AnswerHandling

        public void AddAnswer(string text)
        {
            var answer = Answer.Create(Id, text);
            _answers.Add(answer);
        }

        public void UpdateAnswer(Guid answerId, string newText)
        {
            var answer = _answers.FirstOrDefault(a => a.Id == answerId);
            if (answer is null)
                throw new InvalidOperationException("Svar ikke fundet");
            answer.UpdateAnswer(newText);
        }

        public Answer GetAnswerById(Guid answerId)
        {
            var answer = _answers.FirstOrDefault(a => a.Id == answerId);
            if (answer is null)
                throw new InvalidOperationException("Svar ikke fundet");
            return answer;
        }

        public void RemoveAnswer(Guid answerId)
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

