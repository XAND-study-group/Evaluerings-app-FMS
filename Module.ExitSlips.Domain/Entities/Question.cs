using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities
{
    public class Question : Entity
    {
        #region Properties

        public Text Text { get; protected set; }
        private readonly List<Answer> _answers = [];
        public IReadOnlyCollection<Answer> Answers => _answers;

        #endregion

        #region Constructors

        protected Question() { }
        private Question(string text)
        {
            Text = text;
        }

        public static Question Create(string text)
            => new Question(text);

        #endregion

        #region Methods

        #region AnswerHandling

        public Answer AddAnswer(string text, Guid userId)
        {
            var answer = Answer.Create(text, userId);
            _answers.Add(answer);
            return answer;
        }

        public Answer UpdateAnswer(Guid answerId, string newText)
        {
            var answer = GetAnswerById(answerId);
            answer.UpdateAnswer(newText);
            return answer;
        }

        public void DeleteAnswer(Guid answerId)
        {
            var answer = GetAnswerById(answerId);
            _answers.Remove(answer);
        }

        #endregion

        #region QuestionHandling

        public void UpdateQuestion(string newText)
        {
            Text = newText;
        }

        #endregion

        #region HelperMethods

        private Answer GetAnswerById(Guid answerId)
        {
            var answer = _answers.FirstOrDefault(a => a.Id == answerId);
            if (answer is null)
                throw new InvalidOperationException("Svar ikke fundet.");
            return answer;
        }

        #endregion

        #endregion
    }
}

