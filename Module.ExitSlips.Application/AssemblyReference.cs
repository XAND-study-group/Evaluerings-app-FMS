using System.Reflection;

namespace Module.ExitSlip.Application;

public class AssemblyReference
{
    public static Assembly Assembly { get; set; } = typeof(AssemblyReference).Assembly;
}