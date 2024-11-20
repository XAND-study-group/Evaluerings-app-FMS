using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Domain.Extensions
{
    public static class QuestionExteinsion
    {
        public static GetSimpleQuestionsResponse MapToGetSimpleQuestionResponse(this Entities.Question question) =>
           new(question.Text, question.Answers.Select(answer => answer.MapToGetSimpleAnswerResponse());


        public static IEnumerable<GetSimpleQuestionsResponse> MapToIEnumerableGetSimpleQuestionResponse(this IEnumerable<Entities.Question> Question)
            => Question.Select(Question => Question.MapToGetSimpleQuestionResponse());


        public static GetDetailsQuestionsResponse MapToGetDetailsQuestionResponse(this Entities.Question question) =>
          new(question.Text);


        public static IEnumerable<GetDetailsQuestionsResponse> MapToIEnumerableGetDetailsQuestionResponse(this IEnumerable<Entities.Question> Question)
            => Question.Select(Question => Question.MapToGetDetailsQuestionResponse());


    }
}
