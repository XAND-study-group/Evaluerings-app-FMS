using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Domain.Entities
{
    public class Answer : Entity
    {
        #region Properties
        public string Text { get; protected set; }
        #endregion

        #region Constructors
        public Answer(Guid questionId, string text)
        {
            Text = text;
        }
        #endregion

        #region Methods
        public static Answer Create(Guid questionId, string text)
        => new Answer(questionId, text);

        public void UpdateAnswer(string newText)
        {
            Text = newText;
        }
        #endregion
    }
}
