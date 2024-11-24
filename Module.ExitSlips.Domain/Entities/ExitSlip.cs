﻿using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using SharedKernel.Models;
using SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Domain.Entities
{
    public class ExitSlip : Entity
    {
        #region Properties

        public Guid LectureId { get; set; }
        public Title Title { get; protected set; }

        public int MaxQuestionCount { get; protected set; }
        public ExitSlipActiveStatus ActiveStatus { get; protected set; }

        private readonly List<Question> _questions = new();
        public IReadOnlyCollection<Question> Questions => _questions;

        #endregion

        #region Constructors

        protected ExitSlip()
        {
        }

        private ExitSlip(Guid userId, Guid lectureId, string title, int maxQuestionCount, ExitSlipActiveStatus activeStatus)
        {
            Title = title;
            MaxQuestionCount = maxQuestionCount;
            ActiveStatus = activeStatus;
        }

        #endregion

        #region Methods

        #region Creation

        public static ExitSlip Create(Guid userId, Guid lectureId, string title, int maxQuestionCount, ExitSlipActiveStatus activeStatus)
            => new ExitSlip(userId, lectureId, title, maxQuestionCount, activeStatus);

        #endregion

        #region QuestionHandling

        public void AddQuestion(string text)
        {
            if (_questions.Count >= MaxQuestionCount)
                throw new InvalidOperationException("Kan ikke tilføje flere spørgsmål end det maksimalt tilladte.");

            var question = Question.Create(Id, text);
            _questions.Add(question);
        }

        public void DeleteQuestion(Guid questionId)
        {
            if (ActiveStatus != ExitSlipActiveStatus.Inactive)
                throw new InvalidOperationException("Kan ikke slette spørgsmål i en aktiv ExitSlip.");

            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question is null)
                throw new InvalidOperationException("Spørgsmål ikke fundet.");

            _questions.Remove(question);
        }

        public void UpdateQuestion(Guid questionId, string newText)
        {
            if (ActiveStatus != ExitSlipActiveStatus.Inactive)
                throw new InvalidOperationException("Kan ikke redigere spørgsmål i en aktiv ExitSlip.");

            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question is null)
                throw new InvalidOperationException("Spørgsmål ikke fundet.");

            question.UpdateQuestion(newText);
        }

        public Question GetQuestionById(Guid questionId)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question is null)
                throw new InvalidOperationException("Spørgsmål ikke fundet.");

            return question;
        }

        #endregion

        #region AnswerHandling

        public void AddAnswer(Guid questionId, string text)
        {
            if (ActiveStatus == ExitSlipActiveStatus.Inactive)
                throw new InvalidOperationException("Kan ikke tilføje svar til en inaktiv ExitSlip.");

            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question is null)
                throw new InvalidOperationException("Spørgsmål ikke fundet.");

            question.AddAnswer(text);
        }

        public void UpdateAnswer(Guid questionId, Guid answerId, string newText)
        {
            if (ActiveStatus == ExitSlipActiveStatus.Inactive)
                throw new InvalidOperationException("Kan ikke opdatere svar i en inaktiv ExitSlip.");

            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question is null)
                throw new InvalidOperationException("Spørgsmål ikke fundet.");

            question.UpdateAnswer(answerId, newText);
        }

        #endregion

        #endregion
    }
}
