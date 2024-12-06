using System.Reflection;

namespace Module.ExitSlip.Infrastructure;

public class AssemblyReference
{
    public static Assembly Assembly { get; set; } = typeof(AssemblyReference).Assembly;
}