using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities
{
    public class Answer : Entity
    {
        #region Properties
        public Text Text { get; protected set; }
        #endregion

        #region Constructors

        protected Answer(){}
        private Answer(string text)
        {
            Text = text;
        }
        #endregion

        #region Methods
        public static Answer Create(string text)
        => new (text);

        public void UpdateAnswer(string newText)
        {
            Text = newText;
        }
        #endregion
    }
}
