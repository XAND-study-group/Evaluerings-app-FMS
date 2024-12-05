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
        private Answer(string text, Guid userId)
        {
            Text = text;
            UserId = userId;
        }
        #endregion

        #region Methods
        internal static Answer Create(string text, Guid userId)
          => new Answer(text, userId);

        internal void UpdateAnswer(string newText)
        {
            Text = newText;
        }


        #endregion
    }
}
