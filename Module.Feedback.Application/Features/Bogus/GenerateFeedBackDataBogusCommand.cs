using Bogus;
using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Bogus;

public sealed record GenerateFeedBackDataBogusCommand : IRequest<Result<bool>>, ITransactionalCommand;

public class GenerateFeedBackDataBogusCommandHandler(
    IRoomRepository roomRepository,
    IFeedbackRepository feedbackRepository) : IRequestHandler<GenerateFeedBackDataBogusCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<GenerateFeedBackDataBogusCommand, Result<bool>>
        .Handle(GenerateFeedBackDataBogusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            VoteScale[] randomVoteScale = [VoteScale.UpVote, VoteScale.DownVote];

            var roomsWithDescription = new Dictionary<string, string>
            {
                {
                    "Kantinen", "Dette rum er til at give feedback om kantitnen"
                },
                {
                    "Reception", "Dette rum er til at give feedback om Recptionen"
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
                }
            };

            string[] titles = ["Ikke stor protioner", "Kold lokale", "Bedre møbler"];


            var titlesWithProblems = new Dictionary<string, string>
            {
                {
                    "Ikke stor protioner",
                    "Eleverne får for små mængder mad i kantinen."
                },
                {
                    "Kold lokale",
                    "Lokalet er så koldt, at det er svært at koncentrere sig."
                },
                {
                    "Bedre møbler",
                    "Stolene er slidte og ubehagelige for ryggen."
                }
            };

            var titlesWithSoultions = new Dictionary<string, string>
            {
                {
                    "Ikke stor protioner",
                    "Øg mængden af mad i hver servering."
                },
                {
                    "Kold lokale",
                    "Sørg for bedre isolering eller juster varmesystemet."
                },
                {
                    "Bedre møbler",
                    "Udskift stolene med ergonomiske modeller."
                }
            };

            var titlesWithCommentsAndSubcomments = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {
                    titles[0],
                    new Dictionary<string, List<string>>
                    {
                        {
                            "Jeg synes også portionerne er for små.",
                            new List<string>
                            {
                                "Ja, det samme tænkte jeg sidste uge.",
                                "Det ville være rart med større portioner.",
                                "Måske kunne vi foreslå dette til kantinen."
                            }
                        },
                        {
                            "Måske kunne man tilbyde en ekstra skål til nedsat pris?",
                            new List<string>
                            {
                                "God idé! Det kunne tiltrække flere kunder.",
                                "Måske en slags rabatkort kunne fungere?",
                                "Kantinen kunne også teste idéen i en måned."
                            }
                        },
                        {
                            "Kantinen bør overveje billigere men større portioner.",
                            new List<string>
                            {
                                "Jeg er enig – det ville hjælpe mange studerende.",
                                "Vi kunne også spørge, om de vil lave en afstemning.",
                                "Kantinen burde i det mindste prøve denne løsning."
                            }
                        }
                    }
                },
                {
                    titles[1],
                    new Dictionary<string, List<string>>
                    {
                        {
                            "Jeg tager altid en ekstra trøje med.",
                            new List<string>
                            {
                                "Det er en god idé, men lidt upraktisk.",
                                "Kunne vi ikke bare bede om en bedre opvarmning?",
                                "Jeg har også overvejet en varmepude!"
                            }
                        },
                        {
                            "Kunne vi få et tæppe til at dele?",
                            new List<string>
                            {
                                "Det lyder hyggeligt, men tæpper skal vaskes tit.",
                                "Måske nogle tæpper, der er lette at rengøre?",
                                "Jeg vil gerne hjælpe med at skaffe dem."
                            }
                        },
                        {
                            "Et lille varmeapparat ville hjælpe meget.",
                            new List<string>
                            {
                                "Enig, men vil det ikke være dyrt i drift?",
                                "Vi kunne foreslå en energieffektiv model.",
                                "Hvad med flere radiatorer i hjørnerne?"
                            }
                        }
                    }
                },
                {
                    titles[2],
                    new Dictionary<string, List<string>>
                    {
                        {
                            "Min ryg gør ondt efter en times sidning.",
                            new List<string>
                            {
                                "Prøv at tage en lille pude med hjemmefra.",
                                "Jeg har samme problem – ergonomiske stole ville hjælpe.",
                                "Vi bør spørge om budgettet for nye møbler."
                            }
                        },
                        {
                            "Ergonomiske stole ville være en god investering.",
                            new List<string>
                            {
                                "Det ville nok være dyrt, men værdifuldt i længden.",
                                "Hvad med nogle justerbare stole?",
                                "Kan vi teste nogle prototyper først?"
                            }
                        },
                        {
                            "Et par puder kunne også hjælpe.",
                            new List<string>
                            {
                                "Ja, det ville være en billig løsning.",
                                "Jeg kan også tage en pude med hjemmefra.",
                                "Lad os finde nogle gode forslag online."
                            }
                        }
                    }
                }
            };


            var faker = new Faker();
            var createdRooms = new List<Domain.Entities.Room>();


            var feedbackFake = new Faker<Domain.Entities.Feedback>()
                .UseSeed(123)
                .CustomInstantiator(f =>
                {
                    var chosenTitle = faker.PickRandom(titles.ToList());
                    var chosenProblem = faker.PickRandom(titlesWithProblems[chosenTitle]);
                    var chosenSolution = faker.PickRandom(titlesWithSoultions[chosenTitle]);

                    var userId = Guid.NewGuid();
                    var hashedUserId = HashedUserId.Create(userId);
                    var feedbackCreated = Domain.Entities.Feedback.CreateBogus(
                        f.Random.Guid(),
                        chosenTitle,
                        chosenProblem,
                        chosenSolution,
                        f.PickRandom(createdRooms));

                    for (var i = 0; i < 3; i++)
                    {
                        var chosenComment = f.PickRandom(titlesWithCommentsAndSubcomments[chosenTitle].Keys.ToList());
                        var commentCreated = feedbackCreated.AddCommentBogus(
                            f.Random.Guid(),
                            chosenComment);

                        for (var j = 0; j < 3; j++)
                        {
                            var chosenSubComment =
                                f.PickRandom(titlesWithCommentsAndSubcomments[chosenTitle][chosenComment].ToList());
                            var subCommentCreated = feedbackCreated.AddSubCommentBogus(
                                commentCreated.Id,
                                f.Random.Guid(),
                                chosenSubComment);
                        }
                    }

                    var randomNumber = f.Random.Int(4, 20);
                    for (var v = 0; v < randomNumber; v++)
                    {
                        var voteCreated = feedbackCreated.AddVote(
                            f.Random.Guid(),
                            f.PickRandom(randomVoteScale));
                    }

                    return feedbackCreated;
                });


            var roomFake = new Faker<Domain.Entities.Room>()
                .UseSeed(123)
                .CustomInstantiator(f =>
                {
                    var chosenRoom = f.PickRandom(roomsWithDescription.Keys.ToList());
                    var roomCreated = Domain.Entities.Room.Create(
                        chosenRoom,
                        roomsWithDescription[chosenRoom]);

                    createdRooms.Add(roomCreated);
                    return roomCreated;
                });

            await roomRepository.CreateRoomsAsync(roomFake.GenerateLazy(5));

            await feedbackRepository.CreateFeedbacksAsync(feedbackFake.GenerateLazy(5));

            return Result<bool>.Create("Data is Created", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}