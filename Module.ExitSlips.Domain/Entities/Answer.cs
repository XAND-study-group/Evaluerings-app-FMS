using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities
{
    public class Answer : Entity
    {
        #region Properties
        public Guid UserId { get; protected set; }
        public Text Text { get; protected set; }

        #endregion

        #region Constructors
        protected Answer(){}
        private Answer(Guid questionId, string text, Guid userId)
        {
            Text = text;
            UserId = userId;
        }
        #endregion

        #region Methods
        public static Answer Create(Guid questionId, string text, Guid userId)
        => new Answer(questionId, text, userId);

        public void UpdateAnswer(string newText, Guid userId)
        {
            Text = newText;
        }
        #endregion
    }
}
