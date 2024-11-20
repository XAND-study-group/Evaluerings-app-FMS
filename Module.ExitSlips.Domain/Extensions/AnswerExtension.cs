using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Domain.Extensions
{
    public static class AnswerExtension
    {
        public static GetSimpleAnswerResponse MapToGetSimpleAnswerResponse(this Entities.Answer answer) =>
         new();


        public static IEnumerable<GetSimpleAnswerResponse> MapToIEnumerableGetSimpleAnswerResponse(this IEnumerable<Entities.Answer> answer)
            => answer.Select(answer => answer.MapToGetSimpleAnswerResponse());


        public static GetDetailsAnswerResponse MapToGetDetailsAnswerResponse(this Entities.Answer answer) =>
          new();


        public static IEnumerable<GetDetailsAnswerResponse> MapToIEnumerableGetDetailsAnswerResponse(this IEnumerable<Entities.Answer> answer)
            => answer.Select(answer => answer.MapToGetDetailsAnswerResponse());

    }
}
