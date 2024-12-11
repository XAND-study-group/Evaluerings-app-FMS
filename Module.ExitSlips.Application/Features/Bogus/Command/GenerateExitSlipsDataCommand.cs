using Bogus;
using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Bogus.Command;

public record GenerateExitSlipsDataCommand : IRequest<Result<bool>>, ITransactionalCommand;

public class GeneratExitSlipsDataCommandHandler(IExitSlipRepository exitSlipRepository)
    : IRequestHandler<GenerateExitSlipsDataCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<GenerateExitSlipsDataCommand, Result<bool>>
        .Handle(GenerateExitSlipsDataCommand request, CancellationToken cancellationToken)
    {
        ExitSlipActiveStatus[] activeStatus = [ExitSlipActiveStatus.Active, ExitSlipActiveStatus.Inactive];
        string[] exitSlips = ["Differentialregning", "Integralregning", "Lineær algebra", "Statistik"];

        string[] questions =
        [
            "Hvad er differentialregning?", "Hvad kan differentialregning bruges til?",
            "Kan du nævne et praktisk eksempel på differentialregning?", "Hvilke problemer løser differentialregning?",
            "Hvad er integralregning?", "Hvordan relaterer integralregning sig til differentialregning?",
            "Kan du forklare begrebet areal under en kurve?", "Hvad er lineær algebra?",
            "Hvad er anvendelserne af matricer i lineær algebra?", "Hvad er statistik?",
            "Hvad er forskellen mellem median og gennemsnit?"
        ];


        try
        {
            var exitSlipWithQuestionWithAnswers = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {
                    "Differentialregning", new Dictionary<string, List<string>>
                    {
                        {
                            "Hvad er differentialregning?",
                            new List<string>
                            {
                                "Det handler om at beregne ændringer i funktioner.",
                                "Det bruges til at finde hældninger og hastigheder.",
                                "En central del af calculus, der fokuserer på det lokale forløb af funktioner."
                            }
                        },
                        {
                            "Hvad kan differentialregning bruges til?",
                            new List<string>
                            {
                                "Til at analysere bevægelse i fysik.",
                                "Til at optimere processer og finde maksimum/minimum punkter.",
                                "I økonomi til at beregne marginalomkostninger og marginalprofit."
                            }
                        },
                        {
                            "Kan du nævne et praktisk eksempel på differentialregning?",
                            new List<string>
                            {
                                "Bestemmelse af bilens hastighed ud fra dens positionskurve.",
                                "Beregning af væksthastigheden for en bakteriekultur.",
                                "Analyse af ændringer i aktiekurser."
                            }
                        },
                        {
                            "Hvilke problemer løser differentialregning?",
                            new List<string>
                            {
                                "Optimering af funktioner.",
                                "Modellering af fysiske og matematiske systemer.",
                                "Forudsigelse af ændringer over tid."
                            }
                        }
                    }
                },
                {
                    "Integralregning", new Dictionary<string, List<string>>
                    {
                        {
                            "Hvad er integralregning?",
                            new List<string>
                            {
                                "Det er studiet af arealer og samlede mængder.",
                                "Integralregning er modstykket til differentialregning.",
                                "Bruges til at finde den samlede effekt af kontinuerlige ændringer."
                            }
                        },
                        {
                            "Hvordan relaterer integralregning sig til differentialregning?",
                            new List<string>
                            {
                                "Integralregning er omvendt proces til differentialregning.",
                                "De to er forbundet via den fundamentale sætning i calculus.",
                                "Differentiation nedbryder funktioner, mens integration samler dem."
                            }
                        },
                        {
                            "Kan du forklare begrebet areal under en kurve?",
                            new List<string>
                            {
                                "Arealet repræsenterer integralet af funktionen.",
                                "Det kan bruges til at finde samlede mængder som distance eller arbejde.",
                                "Det beregnes ved at summere små områder under kurven."
                            }
                        }
                    }
                },
                {
                    "Lineær algebra", new Dictionary<string, List<string>>
                    {
                        {
                            "Hvad er lineær algebra?",
                            new List<string>
                            {
                                "Det er studiet af vektorer, matricer og lineære transformationer.",
                                "Et grundlæggende værktøj i mange grene af matematik og fysik.",
                                "Anvendes i datavidenskab, computergrafik og maskinlæring."
                            }
                        },
                        {
                            "Hvad er anvendelserne af matricer i lineær algebra?",
                            new List<string>
                            {
                                "Til at repræsentere lineære transformationer.",
                                "Bruges i computergrafik til at manipulere 3D-objekter.",
                                "Til løsning af lineære ligningssystemer."
                            }
                        }
                    }
                },
                {
                    "Statistik", new Dictionary<string, List<string>>
                    {
                        {
                            "Hvad er statistik?",
                            new List<string>
                            {
                                "Det er studiet af data og metoder til at analysere dem.",
                                "En måde at finde mønstre og sammenhænge i store datasæt.",
                                "Bruges til at træffe informerede beslutninger baseret på data."
                            }
                        },
                        {
                            "Hvad er forskellen mellem median og gennemsnit?",
                            new List<string>
                            {
                                "Medianen er den midterste værdi i et sorteret datasæt.",
                                "Gennemsnittet er summen af alle værdier delt med deres antal.",
                                "Medianen er mindre påvirket af ekstreme værdier end gennemsnittet."
                            }
                        }
                    }
                }
            };


            var faker = new Faker();

            var maxQuestionCount = faker.Random.Int(1, 6);
            var numberOfAnswers = faker.Random.Int(1, 10);

            var exitSlipFaker = new Faker<Domain.Entities.ExitSlip>()
                .UseSeed(123)
                .CustomInstantiator(f =>
                {
                    var chosenExitSlip = faker.PickRandom(exitSlipWithQuestionWithAnswers.Keys.ToList());

                    var exitSlipCreated = Domain.Entities.ExitSlip.Create(
                        f.Random.Guid(),
                        f.Random.Guid(),
                        chosenExitSlip,
                        maxQuestionCount,
                        f.PickRandom(activeStatus));

                    for (var i = 0; i < maxQuestionCount; i++)
                    {
                        var chosenQuestion =
                            faker.PickRandom(exitSlipWithQuestionWithAnswers[chosenExitSlip].Keys.ToList());
                        var questionCreated = exitSlipCreated.AddQuestion(
                            chosenQuestion);


                        for (var j = 0; j < numberOfAnswers; j++)
                            if (exitSlipCreated.ActiveStatus == ExitSlipActiveStatus.Active)
                            {
                                var chosenAnswer =
                                    faker.PickRandom(exitSlipWithQuestionWithAnswers[chosenExitSlip][chosenQuestion]
                                        .ToList());

                                var answerCreated = exitSlipCreated.AddAnswer(
                                    faker.Random.Guid(),
                                    questionCreated.Id,
                                    chosenAnswer);
                            }
                    }

                    return exitSlipCreated;
                });


            await exitSlipRepository.CreateExitSlipsAsync(exitSlipFaker.GenerateLazy(4));

            return Result<bool>.Create("Data is Created", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}