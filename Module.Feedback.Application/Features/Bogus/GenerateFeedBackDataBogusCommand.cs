using Bogus;
using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Bogus
{
    public sealed record GenerateFeedBackDataBogusCommand() : IRequest<Result<bool>>, ITransactionalCommand;

    public class GenerateFeedBackDataBogusCommandHandler(
        IRoomRepository roomRepository,
        IFeedbackRepository feedbackRepository) : IRequestHandler<GenerateFeedBackDataBogusCommand, Result<bool>>
    {
        async Task<Result<bool>> IRequestHandler<GenerateFeedBackDataBogusCommand, Result<bool>>
            .Handle(GenerateFeedBackDataBogusCommand request, CancellationToken cancellationToken)
        {

            try
            {
                string[] rooms = ["Kantinen", "Reception", "MMR1", "DMV1", "MMT3", "HAV2", "HAV1", "PUV1"];
                string[] description = ["Dette rum er til at give feedback om kantitnen", "Dette rum er til at give feedback om Recptionen",
                "Dette rum er til at give feedback om "];
                var roomsWithDescription = new Dictionary<string, string>()
                {
                    {
                        "Kantinen","Dette rum er til at give feedback om kantitnen"
                    },
                    {
                        "Reception","Dette rum er til at give feedback om Recptionen"
                    },
                    {
                         "MMR1", "Dette rum er til at give feedback om MMR1"
                    },
                    {
                         "DMV1", "Dette rum er til at give feedback om DMV1"
                    },
                    {
                         "MMT3", "Dette rum er til at give feedback om MMT3"
                    },
                    {
                         "HAV2", "Dette rum er til at give feedback om HAV2"
                    },
                    {
                         "HAV1", "Dette rum er til at give feedback om HAV1"
                    },
                    {
                         "PUV1", "Dette rum er til at give feedback om PUV1"
                    },
                };





                //var roomFake = new Faker<Domain.Entities.Room>()
                //.CustomInstantiator(f=>
                //{
                //    return Domain.Entities.Room.Create(
                //        f.PickRandom(rooms),
                //        f.PickRandom()
                //});
                return Result<bool>.Create("Data is Created", true, ResultStatus.Success);


            }
            catch (Exception e)
            {

                return Result<bool>.Create(e.Message, false, ResultStatus.Error);
            }









        }
    }
}
