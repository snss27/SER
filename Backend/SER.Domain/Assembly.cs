using System.Reflection;

namespace PMS.Domain;

public static class DomainAssembly
{
    public static Assembly Itself => typeof(DomainAssembly).Assembly;
}