using System.Reflection;

namespace Module.Feedback.Infrastructure;

public class AssemblyReference
{
    public static Assembly Assembly { get; set; } = typeof(AssemblyReference).Assembly;
}