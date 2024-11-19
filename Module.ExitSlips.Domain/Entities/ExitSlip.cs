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

        public Guid OwnerId { get; protected set; }
        public Guid LectureId { get; set; }
        public Title Title { get; protected set; }

        public int MaxQuestionCount { get; protected set; }
        public ExitSlipActiveStatus ActiveStatus { get; protected set; }

        private readonly List<Question> _questions = [];
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




        #region Exit Slip Methodes

        public static ExitSlip Create(Guid userId, Guid lectureId, string title, int maxQuestionCount, ExitSlipActiveStatus activeStatus)
            => new ExitSlip(userId, lectureId, title, maxQuestionCount, activeStatus);

        #endregion



    }
}
