using Bogus;
using MediatR;
using School.Application.Abstractions.Semester;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using School.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;
using SharedKernel.Enums.Features.Semester;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.Bogus.Command;

public record GenerateSchoolDataCommand : IRequest<Result<bool>>, ITransactionalCommand;

public class GenerateSchoolDataCommandHandler(
    IUserRepository userRepository,
    ISemesterRepository semesterRepository,
    IAccountClaimRepository accountClaimRepository,
    IUserDomainService userDomainService) : IRequestHandler<GenerateSchoolDataCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(GenerateSchoolDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var semesterId = 1;
            var classId = 1;
            Role[] roles = [Role.User, Role.User, Role.User, Role.User, Role.Teacher];
            string[] subject = ["Matematik", "Fysik", "Termodynamik", "Mekanik", "Elektroteknik"];
            var subjectAndLecture = new Dictionary<string, List<string>>
            {
                {
                    "Matematik", new List<string>
                    {
                        "Introduktion til lineær algebra",
                        "Differentialregning",
                        "Integralregning",
                        "Vektorregning",
                        "Komplekse tal",
                        "Differentialligninger",
                        "Numeriske metoder",
                        "Statistik og sandsynlighedsregning",
                        "Diskret matematik",
                        "Optimering"
                    }
                },
                {
                    "Fysik", new List<string>
                    {
                        "Klassisk mekanik",
                        "Elektromagnetisme",
                        "Termodynamik",
                        "Optik",
                        "Bølgefysik",
                        "Kvantemekanik",
                        "Atomfysik",
                        "Kernefysik",
                        "Faststoffysik",
                        "Astrofysik"
                    }
                },
                {
                    "Termodynamik", new List<string>
                    {
                        "Termodynamiske systemer og processer",
                        "Varmelære",
                        "Entropi",
                        "Termodynamikkens love",
                        "Ideelle gasser",
                        "Ægte gasser",
                        "Varmekraftmaskiner",
                        "Kølekredsløb",
                        "Varmetransmission",
                        "Faseovergange"
                    }
                },
                {
                    "Mekanik", new List<string>
                    {
                        "Statik",
                        "Dynamik",
                        "Kinematik",
                        "Kraft og moment",
                        "Energi og arbejde",
                        "Svingninger",
                        "Materialelære",
                        "Fluidmekanik",
                        "Maskinelementer",
                        "Konstruktionsteknik"
                    }
                },
                {
                    "Elektroteknik", new List<string>
                    {
                        "Elektriske kredsløb",
                        "Ohms lov",
                        "Kirchhoffs love",
                        "Vekselstrøm",
                        "Jævnstrøm",
                        "Elektromagnetisme",
                        "Elektriske maskiner",
                        "Effektelektronik",
                        "Styringsteknik",
                        "Digital elektronik"
                    }
                }
            };

            var userFake = new Faker<User>()
                .CustomInstantiator(f =>
                    User.Create(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Person.Email,
                        "Password123.",
                        f.PickRandom(roles),
                        userDomainService,
                        accountClaimRepository)).UseSeed(420);

            var subjectFake = new Faker<Subject>()
                .CustomInstantiator(f =>
                {
                    var chosenSubject = f.PickRandom(subject);
                    var lectureFake = new Faker<Lecture>()
                        .CustomInstantiator(f2 =>
                            Lecture.Create( 
                                f2.PickRandom(subjectAndLecture[chosenSubject]),
                                "Dette er en lektions beskrivelse",
                                new TimeOnly(12, 00), new TimeOnly(13, 00),
                                DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                                "CR420"));
                    return Subject.Create(chosenSubject, "Dette er en subject beskrivelse",
                        lectureFake.GenerateLazy(10), []);
                }).UseSeed(420);

            var classFake = new Faker<Class>()
                .CustomInstantiator(f =>
                    Class.Create(
                        "MM" + classId++,
                        "Dette er en klasse beskrivelse",
                        15,
                        userFake.GenerateLazy(5),
                        subjectFake.GenerateLazy(5),
                        [])).UseSeed(420);

            var semesterFake = new Faker<Semester>()
                .CustomInstantiator(f =>
                {
                    var semesterNum = f.Random.Int(1, 5);
                    return Semester.Create(
                        "FMS" + semesterId++,
                        semesterNum,
                        DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                        DateOnly.FromDateTime(DateTime.Today.AddMonths(6 * 6 - semesterNum * 6)), SchoolType.Fredericia,
                        classFake.GenerateLazy(3),
                        []);
                }).UseSeed(420);


            var adminFake = new Faker<User>()
                .CustomInstantiator(f =>
                    User.Create(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Person.Email,
                        "Password123.",
                        Role.Admin,
                        userDomainService,
                        accountClaimRepository)).UseSeed(50);

            await semesterRepository.CreateSemestersAsync(semesterFake.GenerateLazy(2));

            await userRepository.CreateUsersAsync(adminFake.GenerateLazy(5));

            return Result<bool>.Create("Data has been seeded", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}