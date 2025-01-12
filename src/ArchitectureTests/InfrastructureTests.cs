
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests;

public class InfrastructureTests
{
    private static readonly Architecture Architecture =
    new ArchLoader().LoadAssemblies(
        [..AssembliesRepository.FunctionalDomains,
        AssembliesRepository.Infrastructure]).Build();

    private readonly IObjectProvider<IType> _infrastructureLayer =
        Types().That().ResideInAssembly(AssembliesRepository.Infrastructure);

    private readonly IObjectProvider<IType> _functionalDomainsLayer =
        Types().That().ResideInAssembly(AssembliesRepository.FunctionalDomains[0],
            AssembliesRepository.FunctionalDomains.Skip(1).ToArray());


    [Fact]
    // Because purpose of Infrastructure is to share common infrastructure classes to functional domains
    // If a infrastructure class is used by only one functional domain, that class belongs to that functional domain
    public void InfrastructureTypesAreSharedByAtLeastTwoFunctionalDomain()
    {
        var infrastructureTypes = Types().That().Are(_infrastructureLayer).GetObjects(Architecture);
        var functionalDomainsTypes = Types().That().Are(_functionalDomainsLayer).GetObjects(Architecture);
        var functionalDomainsDependenciesByFunctionalDomain = functionalDomainsTypes
            .SelectMany(x => x.Dependencies)
            .GroupBy(x => x.Origin.Assembly);

        Assert.All(infrastructureTypes, infrastructureType =>
        {
            var functionalDomainUsageNumber = functionalDomainsDependenciesByFunctionalDomain
                   .Where(x => x.Any(y => y.Target.FullName == infrastructureType.FullName))
                   .Count();

            Assert.True(functionalDomainUsageNumber > 1,
                $"{infrastructureType.FullName} is used by only {functionalDomainUsageNumber} functional domain");
        });
    }
}