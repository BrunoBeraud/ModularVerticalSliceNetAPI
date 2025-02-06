using System.Reflection;

namespace ComponentName.ArchitectureTests;

internal static class AssembliesRepository
{
    public static Assembly SharedKernel = Assembly.Load("ComponentName.SharedKernel");
    public static Assembly Infrastructure = Assembly.Load("ComponentName.Infrastructure");
    public static Assembly[] FunctionalDomains =
    [
        Assembly.Load("ComponentName.FunctionalDomainNameA"),
        Assembly.Load("ComponentName.FunctionalDomainNameB"),
    ];
}
