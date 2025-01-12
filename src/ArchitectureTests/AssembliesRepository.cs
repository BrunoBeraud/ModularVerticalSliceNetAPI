using System.Reflection;

namespace ArchitectureTests;

internal static class AssembliesRepository
{
    public static Assembly SharedKernel = Assembly.Load("SharedKernel");
    public static Assembly Infrastructure = Assembly.Load("Infrastructure");
    public static Assembly[] FunctionalDomains = [Assembly.Load("FunctionalDomainNameA"), Assembly.Load("FunctionalDomainNameB")];
}