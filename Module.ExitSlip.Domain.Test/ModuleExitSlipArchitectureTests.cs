using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;
using SharedKernel.Interfaces;

namespace Module.ExitSlip.Domain.Test
{
    public class ModuleExitSlipArchitectureTests
    {

        private static readonly Architecture Architecture = new ArchLoader()
            .LoadAssemblies(ExitSlip.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Infrastructure.AssemblyReference.Assembly,
            ExitSlip.Domain.AssemblyReference.Assembly)
            .Build();

        #region Layer Setup

        private readonly IObjectProvider<IType> _presentationLayer =
            Types().That().ResideInAssembly(ExitSlip.AssemblyReference.Assembly).As("Presentation Layer");
        private readonly IObjectProvider<IType> _applicationLayer =
            Types().That().ResideInAssembly(Application.AssemblyReference.Assembly).As("Application Layer");
        private readonly IObjectProvider<IType> _domainLayer =
            Types().That().ResideInAssembly(ExitSlip.Domain.AssemblyReference.Assembly).As("Domain Layer");
        private readonly IObjectProvider<IType> _infrastructureLayer =
            Types().That().ResideInAssembly(Infrastructure.AssemblyReference.Assembly).As("Infrastructure Layer");

        #endregion


        #region Classes & Interfaces Setup

        private readonly IObjectProvider<Class> _endpointClasses =
            Classes().That().ImplementInterface(typeof(IEndpoint)).As("Endpoint Classes");




        #endregion

    }
}
