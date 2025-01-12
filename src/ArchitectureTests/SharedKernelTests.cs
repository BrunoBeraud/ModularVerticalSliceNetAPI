using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests;

public class SharedKernelTests
{
    private static readonly Architecture Architecture =
new ArchLoader().LoadAssemblies(
    [..AssembliesRepository.FunctionalDomains,
        AssembliesRepository.SharedKernel]).Build();

    private readonly IObjectProvider<IType> _sharedKernel =
        Types().That().ResideInAssembly(AssembliesRepository.SharedKernel);

    private readonly IObjectProvider<IType> _functionalDomainsLayer =
        Types().That().ResideInAssembly(AssembliesRepository.FunctionalDomains[0],
            AssembliesRepository.FunctionalDomains.Skip(1).ToArray());


    [Fact]
    // Because purpose of Shared Kernel is to share common domain logic classes to functional domains
    // If a Shared Kernel  class is used by only one functional domain, that class belongs to that functional domain
    public void SharedKernelTypesAreSharedByAtLeastTwoFunctionalDomain()
    {
        var sharedKernelTypes = Types().That().Are(_sharedKernel).GetObjects(Architecture);
        var functionalDomainsTypes = Types().That().Are(_functionalDomainsLayer).GetObjects(Architecture);
        var functionalDomainsDependenciesByFunctionalDomain = functionalDomainsTypes
            .SelectMany(x => x.Dependencies)
            .GroupBy(x => x.Origin.Assembly);

        Assert.All(sharedKernelTypes, sharedKernelType =>
        {
            var functionalDomainUsageNumber = functionalDomainsDependenciesByFunctionalDomain
                   .Where(x => x.Any(y => y.Target.FullName == sharedKernelType.FullName))
                   .Count();

            Assert.True(functionalDomainUsageNumber > 1,
                $"{sharedKernelType.FullName} is used by only {functionalDomainUsageNumber} functional domain");
        });
    }
}