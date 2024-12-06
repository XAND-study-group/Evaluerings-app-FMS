using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Module.ExitSlip.Domain.Test")]

namespace Module.ExitSlip.Domain;

public class AssemblyReference
{
    public static Assembly Assembly { get; set; } = typeof(AssemblyReference).Assembly;
}