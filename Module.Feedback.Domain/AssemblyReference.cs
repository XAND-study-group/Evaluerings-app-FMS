using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Module.Feedback.Domain.Test")]

namespace Module.Feedback.Domain;

public class AssemblyReference
{
    public static Assembly Assembly { get; set; } = typeof(AssemblyReference).Assembly;
}