using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Application.Features.Room.Command;
using Module.Feedback.Endpoints.Room;
using Module.Feedback.Infrastructure.Repositories;
using SharedKernel.Interfaces;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Module.Feedback.Domain.Test;

public class ModuleFeedbackArchitectureTests
{
    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(typeof(CreateRoom).Assembly,
            typeof(CreateRoomCommand).Assembly,
            typeof(Room).Assembly,
            typeof(RoomRepository).Assembly)
        .Build();

    #region Layer Setup

    private readonly IObjectProvider<IType> _presentationLayer =
        Types().That().ResideInAssembly(typeof(CreateRoom).Assembly).As("Presentation Layer");

    private readonly IObjectProvider<IType> _applicationLayer =
        Types().That().ResideInAssembly(typeof(CreateRoomCommand).Assembly).As("Application Layer");

    private readonly IObjectProvider<IType> _domainLayer =
        Types().That().ResideInAssembly(typeof(Room).Assembly).As("Domain Layer");

    private readonly IObjectProvider<IType> _infrastructureLayer =
        Types().That().ResideInAssembly(typeof(RoomRepository).Assembly).As("Infrastructure Layer");

    #endregion Layer Setup

    #region Classes & Interfaces Setup

    private readonly IObjectProvider<Class> _endpointClasses =
        Classes().That().ImplementInterface(typeof(IEndpoint)).As("Endpoint Classes");

    private readonly IObjectProvider<Class> _transactionalClasses =
        Classes().That().ImplementInterface(typeof(ITransactionalCommand)).As("Application Classes");

    private readonly IObjectProvider<Class> _commandRecords =
        Classes().That().HaveNameEndingWith("Command").And().AreRecord().As("Command Records");

    private readonly IObjectProvider<Class> _iRequestRecords =
        Classes().That().ImplementInterface(typeof(IRequest<>)).And().AreRecord().As("IRequest Records");

    private readonly IObjectProvider<Class> _commandHandlerClasses =
        Classes().That().HaveNameEndingWith("CommandHandler").As("CommandHandler Classes");

    private readonly IObjectProvider<Class> _queryRecords =
        Classes().That().HaveNameEndingWith("Query").And().AreRecord().As("Query Records");

    private readonly IObjectProvider<Class> _queryHandlerClasses =
        Classes().That().HaveNameEndingWith("QueryHandler").As("QueryHandler Classes");

    private readonly IObjectProvider<Class> _iRequestHandlerClasses =
        Classes().That().ImplementInterface(typeof(IRequestHandler<,>)).As("IRequestHandler Classes");

    private readonly IObjectProvider<Class> _repositoryInterfaces =
        Classes().That().ImplementInterface(typeof(IRoomRepository))
            .Or().ImplementInterface(typeof(IFeedbackRepository))
            .Or().ImplementInterface(typeof(ICommentRepository))
            .Or().ImplementInterface(typeof(IVoteRepository)).As("Repository Interfaces");

    private readonly IObjectProvider<Class> _domainClasses =
        Classes().That().AreAssignableTo(typeof(Entity)).As("Domain Classes");
    
    private readonly IObjectProvider<Class> _dbContextClasses =
        Classes().That().AreAssignableTo(typeof(DbContext)).As("DbContext Classes");

    #endregion Classes & Interfaces Setup

    [Fact]
    public void EndpointClassesShouldBeInPresentationLayer()
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
            Classes().That().Are(_commandRecords).Should().Be(_applicationLayer);
        IArchRule commandRecordsShouldBeInImplementIRequest =
            Classes().That().Are(_commandRecords).Should().ImplementInterface(typeof(IRequest<>));

        var combineRules =
            commandRecordsShouldBeInApplicationLayer.And(commandRecordsShouldBeInImplementIRequest);

        combineRules.Check(Architecture);
    }

    [Fact]
    public void IRequestRecordsShouldBeInApplicationLayer()
    {
        IArchRule iRequestRecordsShouldBeInApplicationLayer =
            Classes().That().Are(_iRequestRecords).Should().Be(_applicationLayer);

        iRequestRecordsShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void CommandHandlerClassesShouldBeInApplicationLayer()
    {
        IArchRule commandHandlerClassesShouldBeInApplicationLayer =
            Classes().That().Are(_commandHandlerClasses).Should().Be(_applicationLayer);
        IArchRule commandHandlerClassesShouldImplementIRequestHandler =
            Classes().That().Are(_commandHandlerClasses).Should().ImplementInterface(typeof(IRequestHandler<,>));

        var combineRules =
            commandHandlerClassesShouldBeInApplicationLayer.And(commandHandlerClassesShouldImplementIRequestHandler);

        combineRules.Check(Architecture);
    }

    [Fact]
    public void QueryRecordsShouldBeInApplicationLayer()
    {
        IArchRule queryRecordsShouldBeInApplicationLayer =
            Classes().That().Are(_queryRecords).Should().Be(_applicationLayer);
        IArchRule queryRecordsShouldBeInImplementIRequest =
            Classes().That().Are(_queryRecords).Should().ImplementInterface(typeof(IRequest<>));

        var combineRules = queryRecordsShouldBeInApplicationLayer.And(queryRecordsShouldBeInImplementIRequest);

        combineRules.Check(Architecture);
    }

    [Fact]
    public void QueryHandlerClassesShouldBeInInfrastructureLayer()
    {
        IArchRule queryHandlerClassesShouldBeInApplicationLayer =
            Classes().That().Are(_queryHandlerClasses).Should().Be(_infrastructureLayer);
        IArchRule queryHandlerClassesShouldImplementIRequestHandler =
            Classes().That().Are(_queryHandlerClasses).Should().ImplementInterface(typeof(IRequestHandler<,>));

        var combineRules =
            queryHandlerClassesShouldBeInApplicationLayer.And(queryHandlerClassesShouldImplementIRequestHandler);

        combineRules.Check(Architecture);
    }

    [Fact]
    public void RepositoryInterfacesShouldBeInInfrastructureLayer()
    {
        IArchRule repositoryInterfacesShouldBeInApplicationLayer =
            Classes().That().Are(_repositoryInterfaces).Should().Be(_infrastructureLayer);

        repositoryInterfacesShouldBeInApplicationLayer.Check(Architecture);
    }

    [Fact]
    public void DomainClassesShouldBeInDomainLayer()
    {
        IArchRule domainClassesShouldBeInDomainLayer =
            Classes().That().Are(_domainClasses).Should().Be(_domainLayer);

        domainClassesShouldBeInDomainLayer.Check(Architecture);
    }

    [Fact]
    public void DbContextClassesShouldBeInInfrastructureLayer()
    {
        IArchRule dbContextClassesShouldBeInInfrastructureLayer =
            Classes().That().Are(_dbContextClasses).Should().Be(_infrastructureLayer);
        
        dbContextClassesShouldBeInInfrastructureLayer.Check(Architecture);
    }

    [Fact]
    public void IRequestHandlerClassesShouldBeInApplicationOrInfrastructureLayer()
    {
        IArchRule iRequestHandlerClassesShouldBeInApplicationOrInfrastructureLayer =
            Classes().That().Are(_iRequestHandlerClasses).Should().Be(_applicationLayer).OrShould().Be(_infrastructureLayer);
        
        iRequestHandlerClassesShouldBeInApplicationOrInfrastructureLayer.Check(Architecture);
    }
}