using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Domain.ValueObjects;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Domain.Test.Fakes
{
    public class FakeExitSlip : Entities.ExitSlip
    {
        public FakeExitSlip()
        {
        }

        public FakeExitSlip(ExitSlipActiveStatus activeStatus)
        {
            ActiveStatus = activeStatus;
        }

        public void SetTitle(string title)
            => Title = title;

        public void SetMaxQuestionCount(int maxQuestionCount)
            => MaxQuestionCount.Create(maxQuestionCount);

        
    }
}
