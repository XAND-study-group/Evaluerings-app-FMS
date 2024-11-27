using System.Reflection;

namespace Module.Feedback.Domain
{
    public class AssemblyReference
    {
        public static Assembly Assembly { get; set; } = typeof(AssemblyReference).Assembly;
    }
}
