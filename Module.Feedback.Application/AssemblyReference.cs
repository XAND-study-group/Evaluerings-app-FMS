using System.Reflection;

namespace Module.Feedback.Application
{
    public class AssemblyReference
    {
        public static Assembly Assembly { get; set; } = typeof(AssemblyReference).Assembly;
    }
}
