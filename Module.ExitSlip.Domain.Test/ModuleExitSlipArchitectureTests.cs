using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Interfaces;
using SharedKernel.Models;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Module.ExitSlip.Domain.Test;

public class ModuleExitSlipArchitectureTests
{
    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(ExitSlip.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Infrastructure.AssemblyReference.Assembly,
            AssemblyReference.Assembly)
        .Build();

    #region Layer Setup

    private readonly IObjectProvider<IType> _presentationLayer =
        Types().That().ResideInAssembly(ExitSlip.AssemblyReference.Assembly).As("Presentation Layer");

    private readonly IObjectProvider<IType> _applicationLayer =
        Types().That().ResideInAssembly(Application.AssemblyReference.Assembly).As("Application Layer");

    private readonly IObjectProvider<IType> _domainLayer =
        Types().That().ResideInAssembly(AssemblyReference.Assembly).As("Domain Layer");

    private readonly IObjectProvider<IType> _infrastructureLayer =
        Types().That().ResideInAssembly(Infrastructure.AssemblyReference.Assembly).As("Infrastructure Layer");

    #endregion


    #region Classes & Interfaces Setup

    private readonly IObjectProvider<Class> _endpointClasses =
        Classes().That().ImplementInterface(typeof(IEndpoint)).As("Endpoint Classes");

    private readonly IObjectProvider<Class> _transactionalClasses =
        Classes().That().ImplementInterface(typeof(ITransactionalCommand)).As("Transactional Classes");

    private readonly IObjectProvider<Class> _commandRecords =
        Classes().That().HaveNameEndingWith("Command").As("Command Records");

    private readonly IObjectProvider<Class> _commandHandlerClasses =
        Classes().That().HaveNameEndingWith("CommandHandler").As("CommandHandler Classes");

    private readonly IObjectProvider<Class> _iRequestRecord =
        Classes().That().ImplementInterface(typeof(IRequest<>)).As("Request Record");

    private readonly IObjectProvider<Class> _iRequestHandlerClasses =
        Classes().That().ImplementInterface(typeof(IRequestHandler<,>)).As("RequestHandler CLasses");

    private readonly IObjectProvider<Class> _queryRecords =
        Classes().That().HaveNameEndingWith("Query").As("Query Records");

    private readonly IObjectProvider<Class> _queryHandlerClasses =
        Classes().That().HaveNameEndingWith("QueryHandler").As("QueryHandler CLasses");

    private readonly IObjectProvider<Interface> _repositoryInterfaces =
        Interfaces().That().HaveNameEndingWith("Repository").As("Repository Interface");

    private readonly IObjectProvider<Class> _repositoryClasses =
        Classes().That().ImplementInterface(typeof(IExitSlipRepository))
            .Or().ImplementInterface(typeof(IQuestionRepository))
            .Or().ImplementInterface(typeof(IAnswerRepository)).As("Repository Classes");


    private readonly IObjectProvider<Interface> _iDbContext =
        Interfaces().That().HaveNameEndingWith("DbContext").As("IDbContext Interface");

    private readonly IObjectProvider<Class> _dbContextClasses =
        Classes().That().AreAssignableTo(typeof(DbContext)).As("DbContext Classes");

    private readonly IObjectProvider<Class> _domainClasses =
        Classes().That().AreAssignableTo(typeof(Entity)).As("Domain Classes");

    #endregion


    #region Tests

    [Fact]
    public void EndPointClassesShouldBeInPresentationLayer()
    {
        IArchRule endpointClassesShouldBeInPresentationLayer =
            Classes().That().Are(_endpointClasses).Should().Be(_presentationLayer);

        endpointClassesShouldBeInPresentationLayer.Check(Architecture);
    }

    [Fact]
    public void TransactionalClassesShouldBeInApplicationLayer()
    {
        IArchRule transactionalClassesShouldBeInApplicationLayer =
            Classes().That().Are(_transactionalClasses).Should().Be(_applicationLayer);

        transactionalClassesShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void CommandRecordsShouldBeInApplicationLayer()
    {
        IArchRule commandRecordsShouldBeInApplicationLayer =
            Classes().That().Are(_commandRecords)
                .Should().Be(_applicationLayer)
                .AndShould().ImplementInterface(typeof(IRequest<>))
                .AndShould().BeRecord();

        commandRecordsShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void CommandHandlerClassesShouldBeInApplicationLayer()
    {
        IArchRule commandHandlerShouldBeInApplicationLayer =
            Classes().That().Are(_commandHandlerClasses)
                .Should().Be(_applicationLayer)
                .AndShould().ImplementInterface(typeof(IRequestHandler<,>));

        commandHandlerShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void IRequestRecordShouldBeInApplicationLayer()
    {
        IArchRule iRequestRecordShouldBeInApplicationLayer =
            Classes().That().Are(_iRequestRecord)
                .Should().Be(_applicationLayer)
                .AndShould().BeRecord();

        iRequestRecordShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void IRequestHandlerClassesShouldBeInApplicationLayerOrInfrastructureLayer()
    {
        IArchRule iRequestHandlerClassesShouldBeInApplicationLayer =
            Classes().That().Are(_iRequestHandlerClasses)
                .Should().Be(_applicationLayer)
                .OrShould().Be(_infrastructureLayer);

        iRequestHandlerClassesShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void QueryRecordsShouldBeInApplicationLayer()
    {
        IArchRule queryRecordsShouldBeInApplicationLayer =
            Classes().That().Are(_queryRecords)
                .Should().Be(_applicationLayer)
                .AndShould().ImplementInterface(typeof(IRequest<>))
                .AndShould().BeRecord();

        queryRecordsShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void QueryHandlerClassesShouldBeInInfrastructureLayer()
    {
        IArchRule queryHandlerClassesShouldBeInInfrastructureLayer =
            Classes().That().Are(_queryHandlerClasses)
                .Should().Be(_infrastructureLayer)
                .AndShould().ImplementInterface(typeof(IRequestHandler<,>));

        queryHandlerClassesShouldBeInInfrastructureLayer.Check(Architecture);
    }

    [Fact]
    public void RepositoryInterfacesShouldBeInApplicationLayer()
    {
        IArchRule repositoryInterfacesShouldBeInApplicationLayer =
            Interfaces().That().Are(_repositoryInterfaces)
                .Should().Be(_applicationLayer);

        repositoryInterfacesShouldBeInApplicationLayer.Check(Architecture);
    }


    [Fact]
    public void RepositoryClassesShouldBeInInfrastructureLayer()
    {
        IArchRule repositoryClassesShouldBeInInfrastructureLayer =
            Classes().That().Are(_repositoryClasses)
                .Should().Be(_infrastructureLayer);

        repositoryClassesShouldBeInInfrastructureLayer.Check(Architecture);
    }

    [Fact]
    public void IDbContextInterfaceShouldBeInApplication()
    {
        IArchRule iDbContextInterfaceShouldBeInApplication =
            Interfaces().That().Are(_iDbContext)
                .Should().Be(_applicationLayer);

        iDbContextInterfaceShouldBeInApplication.Check(Architecture);
    }

    [Fact]
    public void DbContextClassesShouldBeInInfrastructureLayer()
    {
        IArchRule dbContextCLassesShouldBeInInfrastructureLayer =
            Classes().That().Are(_dbContextClasses)
                .Should().Be(_infrastructureLayer);

        dbContextCLassesShouldBeInInfrastructureLayer.Check(Architecture);
    }

    [Fact]
    public void DomainClassesShouldBeInDomainLayer()
    {
        IArchRule domainClassesShouldBeInDomainLayer =
            Classes().That().Are(_domainClasses)
                .Should().Be(_domainLayer);

        domainClassesShouldBeInDomainLayer.Check(Architecture);
    }

    [Fact]
    public void DomainLayerHasCorrectDependencies()
    {
        IArchRule correctDependenciesForDomainLayer =
            Types().That().Are(_domainLayer)
                .Should().NotDependOnAnyTypesThat().Are(_infrastructureLayer)
                .AndShould().NotDependOnAnyTypesThat().Are(_applicationLayer)
                .AndShould().NotDependOnAnyTypesThat().Are(_presentationLayer);

        correctDependenciesForDomainLayer.Check(Architecture);
    }

    [Fact]
    public void ApplicationLayerHasCorrectDependencies()
    {
        IArchRule correctDependenciesForApplicationLayer =
            Types().That().Are(_applicationLayer)
                .Should().NotDependOnAnyTypesThat().Are(_infrastructureLayer)
                .AndShould().NotDependOnAnyTypesThat().Are(_presentationLayer);

        correctDependenciesForApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void PresentationLayerHasCorrectDependencies()
    {
        IArchRule correctDependenciesForPresentationLayer =
            Types().That().Are(_presentationLayer)
                .Should().NotDependOnAnyTypesThat().Are(_domainLayer);

        correctDependenciesForPresentationLayer.Check(Architecture);
    }

    [Fact]
    public void InfrastructureLayerHasCorrectDependencies()
    {
        IArchRule correctDependenciesForInfrastructureLayer =
            Types().That().Are(_infrastructureLayer)
                .Should().NotDependOnAnyTypesThat().Are(_presentationLayer);

        correctDependenciesForInfrastructureLayer.Check(Architecture);
    }

    #endregion
}