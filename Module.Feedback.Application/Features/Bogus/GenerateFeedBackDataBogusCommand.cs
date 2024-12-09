using Bogus;
using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Vote;
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
                VoteScale[] randomVoteScale = [VoteScale.UpVote, VoteScale.DownVote];

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

                string[] titles = ["Ikke stor protioner", "Kold lokale", "Bedre møbler", "God undervisning", "Manglende lektierhjælp", "God biblíotek"];

                string[] problems = [
                    "Eleverne får for små mængder mad i kantinen.",
                    "Lokalet er så koldt, at det er svært at koncentrere sig.",
                    "Stolene er slidte og ubehagelige for ryggen.",
                    "Undervisningen er generelt god, men mangler praktiske øvelser.",
                    "Eleverne har svært ved at få hjælp til lektierne efter skoletid.",
                    "Biblioteket er veludstyret, men det er svært at finde de rigtige bøger."];

                string[] solutions = [
                    "Øg mængden af mad i hver servering.",
                    "Sørg for bedre isolering eller juster varmesystemet.",
                    "Udskift stolene med ergonomiske modeller.",
                    "Indfør flere øvelser og gruppearbejde i undervisningen.",
                    "Tilbyd en daglig lektiecafé eller online lektiehjælp.",
                    "Opret et overskueligt katalog eller en digital søgefunktion."];

                string[] kommentarer =
                {
                    "Jeg synes også portionerne er for små.",
                    "Måske kunne man tilbyde en ekstra skål til nedsat pris?",
                    "Kantinen bør overveje billigere men større portioner.",

                    "Jeg tager altid en ekstra trøje med.",
                    "Kunne vi få et tæppe til at dele?",
                    "Et lille varmeapparat ville hjælpe meget.",

                    "Min ryg gør ondt efter en times sidning.",
                    "Ergonomiske stole ville være en god investering.",
                    "Et par puder kunne også hjælpe.",

                    "Jeg lærte virkelig meget i dag!",
                    "Læreren gør komplekse emner lette at forstå.",
                    "Flere eksempler ville gøre det endnu bedre.",

                    "Jeg har brug for hjælp efter skoletid.",
                    "Måske en online chatsupport?",
                    "En ugentlig studiegruppe kunne være en løsning.",

                    "Super udvalg af bøger.",
                    "Måske lidt mere behagelige læsekroge?",
                    "En bedre søgefunktion på bibliotekets pc ville være fantastisk."
                };

                var titlesWithProblems = new Dictionary<string, string>()
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
                    },
                    {
                        "God undervisning",
                        "Undervisningen er generelt god, men mangler praktiske øvelser."
                    },
                    {
                        "Manglende lektierhjælp",
                        "Eleverne har svært ved at få hjælp til lektierne efter skoletid."
                    },
                    {
                        "God biblíotek",
                        "Biblioteket er veludstyret, men det er svært at finde de rigtige bøger."
                    }
                };

                var titlesWithSoultions = new Dictionary<string, string>()
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
                    },
                    {
                        "God undervisning",
                        "Indfør flere øvelser og gruppearbejde i undervisningen."
                    },
                    {
                        "Manglende lektierhjælp",
                        "Tilbyd en daglig lektiecafé eller online lektiehjælp."
                    },
                    {
                        "God biblíotek",
                        "Opret et overskueligt katalog eller en digital søgefunktion."
                    }
                };

                var titlesWithComments = new Dictionary<string, List<string>>()
                {
                    {
                        "Ikke stor protioner",
                        new List<string>()
                        {
                            "Jeg synes også portionerne er for små.",
                            "Måske kunne man tilbyde en ekstra skål til nedsat pris?",
                            "Kantinen bør overveje billigere men større portioner."
                        }
                    },
                    {
                         "Kold lokale",
                         new List<string>()
                         {
                            "Jeg tager altid en ekstra trøje med.",
                            "Kunne vi få et tæppe til at dele?",
                            "Et lille varmeapparat ville hjælpe meget."
                         }
                    },
                    {
                         "Bedre møbler",
                         new List<string>()
                         {
                            "Min ryg gør ondt efter en times sidning.",
                            "Ergonomiske stole ville være en god investering.",
                            "Et par puder kunne også hjælpe."
                         }
                    },
                    {
                         "God undervisning",
                         new List<string>()
                         {
                            "Jeg lærte virkelig meget i dag!",
                            "Læreren gør komplekse emner lette at forstå.",
                            "Flere eksempler ville gøre det endnu bedre."
                         }
                    },
                    {
                         "Manglende lektierhjælp",
                         new List<string>()
                         {
                             "Jeg har brug for hjælp efter skoletid.",
                            "Måske en online chatsupport?",
                            "En ugentlig studiegruppe kunne være en løsning."
                         }
                    },
                    {
                         "God biblíotek",
                         new List<string>()
                         {
                            "Super udvalg af bøger.",
                            "Måske lidt mere behagelige læsekroge?",
                            "En bedre søgefunktion på bibliotekets pc ville være fantastisk."
                         }
                    },

                };


                var faker = new Faker();



                var feedbackFake = new Faker<Domain.Entities.Feedback>()
                    .UseSeed(123)
                    .CustomInstantiator(f =>
                    {
                        var chosenTitle = faker.PickRandom(titles.ToList());
                        var chosenProblem = faker.PickRandom(titlesWithProblems[chosenTitle].ToList());
                        var chosenSolution = faker.PickRandom(titlesWithSoultions[chosenTitle].ToList());

                        var userId = Guid.NewGuid();
                        HashedUserId hashedUserId = HashedUserId.Create(userId);
                        var feedbackCreated = Domain.Entities.Feedback.CreateBogus(
                            f.Random.Guid(),
                            chosenTitle,
                            chosenProblem,
                            chosenSolution,
                            

                            )
                    });
                


                var roomFake = new Faker<Domain.Entities.Room>()
                    .UseSeed(123)
                    .CustomInstantiator(f =>
                    {
                        var chosenRoom = f.PickRandom(roomsWithDescription.Keys.ToList());
                        return Domain.Entities.Room.Create(
                            chosenRoom,
                            roomsWithDescription[chosenRoom],
                            feedbackFake.Generate(7));
                    });



                return Result<bool>.Create("Data is Created", true, ResultStatus.Success);
            }
            catch (Exception e)
            {

                return Result<bool>.Create(e.Message, false, ResultStatus.Error);
            }









        }
    }
}


so
    I stopped here. I was thinking about changing the titlesWithComments to titlesWithCommentsAndSubComments making it like ExitSLip. So a tree form. 

    Other than that an important question ... why do we have Room property in Feedback?? 