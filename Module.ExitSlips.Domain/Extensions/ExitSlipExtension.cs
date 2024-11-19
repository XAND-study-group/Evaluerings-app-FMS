using Module.ExitSlip.Domain.Entities;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;

namespace Module.ExitSlip.Domain.Extensions
{
    public static class ExitSlipExtension
    {
        public static GetSimpleExitSlipsResponse MapToGetSimpleExitSlipResponse(this Entities.ExitSlip exitSlip) =>
            new(exitSlip.LectureId, exitSlip.Title, exitSlip.MaxQuestionCount, exitSlip.ActiveStatus);


        public static IEnumerable<GetSimpleExitSlipsResponse> MapToGetSimpleIEnumerableGetExitSlipsResponse(this IEnumerable<Entities.ExitSlip> exitSlips)
            => exitSlips.Select(exitSlip => exitSlip.MapToGetSimpleExitSlipResponse());


        public static GetDetailsExitSlipResponse MapToDetailsExitSlipResponse(this Entities.ExitSlip exitSlip)
            => new(exitSlip.LectureId, exitSlip.Title, exitSlip.MaxQuestionCount, exitSlip.ActiveStatus, exitSlip.Questions.Select(q => q.MapToGetSimpleQuestionResponse()));

        public static IEnumerable<GetDetailsExitSlipResponse> MapToIEnumerableGetExitSlipsResponse(this IEnumerable<Entities.ExitSlip> exitSlips)
            => exitSlips.Select(exitSlip => exitSlip.MapToDetailsExitSlipResponse());
    }
}
