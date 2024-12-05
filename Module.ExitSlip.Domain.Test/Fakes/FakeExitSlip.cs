using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace Module.ExitSlip.Domain.Test.Fakes
{
    public class FakeExitSlip : Entities.ExitSlip
    {
        public FakeExitSlip()
        {
        }
        public FakeExitSlip(string title, ExitSlipActiveStatus activeStatus)
        {
            Title = title;
            ActiveStatus = activeStatus;
        }
        public FakeExitSlip( ExitSlipActiveStatus activeStatus)
        {

            ActiveStatus = activeStatus;
        }

        public void SetTitle(string title)
            => Title = title;

        public void SetMaxQuestionCount(int maxQuestionCount)
            => MaxQuestionCount = maxQuestionCount;
    }
}