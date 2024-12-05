using Module.ExitSlip.Domain.ValueObjects;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities
{
    public class ExitSlip : Entity
    {
        #region Properties

        public Guid? SubjectId { get; protected set; }
        public Guid? LectureId { get; protected set; }

        public Title Title { get; protected set; }

        public MaxQuestionCount MaxQuestionCount { get; protected set; }
        public ExitSlipActiveStatus ActiveStatus { get; protected set; }

        private readonly List<Question> _questions = [];
        public IReadOnlyCollection<Question> Questions => _questions;

        #endregion

        #region Constructors

        protected ExitSlip()
        {
        }

        private ExitSlip(Guid? subjectId, Guid? lectureId, string title, MaxQuestionCount maxQuestionCount, ExitSlipActiveStatus activeStatus)
        {
            SubjectId = subjectId;
            LectureId = lectureId;
            Title = title;
            MaxQuestionCount = maxQuestionCount;
            ActiveStatus = activeStatus;
        }

        #endregion

        #region Exit Slip Methodes

        public static ExitSlip Create(Guid? subjectId, Guid? lectureId, string title, MaxQuestionCount maxQuestionCount, ExitSlipActiveStatus activeStatus)
            => new ExitSlip(subjectId, lectureId, title, maxQuestionCount, activeStatus);

        #region Creation

        public void Update(string title)
        {
            AssureExitSlipInactive();
            Title = title;
        }
        public void Delete()
        {
            AssureExitSlipInactive();            
        }

        public void UpdateActiveStatus(ExitSlipActiveStatus activeStatus)
        {
            ActiveStatus = activeStatus;
        }
        private void AssureExitSlipInactive()
        {
            if (ActiveStatus == ExitSlipActiveStatus.Active)
                throw new ArgumentException("ExitSlip skal være inaktiv, før de kan redigeres.");
        }

        #endregion

        #region QuestionHandling

        public Question AddQuestion(string text, Guid userId)
        {
            if (_questions.Count >= MaxQuestionCount)
                throw new InvalidOperationException("Kan ikke tilføje flere spørgsmål end det maksimalt tilladte.");

            var question = Question.Create(text);
            _questions.Add(question);
            return question;
        }

        public void DeleteQuestion(Guid questionId)
        {
            EnsureInactiveStatus();

            var question = GetQuestionById(questionId);

            _questions.Remove(question);
        }

        public Question UpdateQuestion(Guid questionId, string newText)
        {
            EnsureInactiveStatus();

            var question = GetQuestionById(questionId);

            question.UpdateQuestion(newText);
            return question;
        }

        #endregion

        #region AnswerHandling

        public Answer AddAnswerToQuestion(Guid userId, Guid questionId, string text)
        {
            EnsureActiveStatus();

            var question = GetQuestionById(questionId);

            var answer = question.AddAnswer(text, userId);
            return answer;
        }

        public Answer UpdateAnswerOnQuestion(Guid questionId, Guid answerId, string newText)
        {
            EnsureActiveStatus();

            var question = GetQuestionById(questionId);

            return question.UpdateAnswer(answerId, newText);
        }

        #endregion

        #region StatusHandling

        private void EnsureInactiveStatus()
        {
            if(ActiveStatus!=ExitSlipActiveStatus.Inactive)
                throw new InvalidOperationException("Kan ikke ændre spørgsmål på en aktiv ExitSlip.");
        }
        private void EnsureActiveStatus()
        {
            if (ActiveStatus != ExitSlipActiveStatus.Active)
                throw new InvalidOperationException("Kan ikke tilføje svar til en inaktiv ExitSlip.");
        }
        #endregion

        #region HelperMethods

        private Question GetQuestionById(Guid questionId)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question is null)
                throw new InvalidOperationException("Spørgsmål ikke fundet.");
            return question;
        }
        #endregion

        #endregion
    }
}

