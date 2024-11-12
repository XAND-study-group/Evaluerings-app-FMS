using System.Reflection;
using System.Runtime.CompilerServices;

namespace Module.User;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}