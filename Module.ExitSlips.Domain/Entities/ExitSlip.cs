using Module.ExitSlip.Domain.ValueObjects;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;
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



    }
}

