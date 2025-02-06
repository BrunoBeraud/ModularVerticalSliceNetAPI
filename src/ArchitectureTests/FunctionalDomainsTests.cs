using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnitV3;
using ComponentName.FunctionalDomainNameA;
using ComponentName.FunctionalDomainNameB;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ComponentName.ArchitectureTests;

public class FunctionalDomainsTests
{
    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            [
                .. AssembliesRepository.FunctionalDomains,
                AssembliesRepository.SharedKernel,
                AssembliesRepository.Infrastructure,
            ]
        )
        .Build();

    private readonly IObjectProvider<IType> _coreLayer = Types()
        .That()
        .ResideInNamespace(@".*\.Core.*", useRegularExpressions: true);

    private readonly IObjectProvider<Class> _featuresLayer = Classes()
        .That()
        .ResideInNamespace(@"\..*Features\.", useRegularExpressions: true);

    private readonly IObjectProvider<Class> _useCasesLayer = Classes()
        .That()
        .HaveNameEndingWith("UseCase");

    private readonly IObjectProvider<Class> _endpointsLayer = Classes()
        .That()
        .HaveNameEndingWith("Endpoint");
    private readonly IObjectProvider<IType> _dtosLayer = Types()
        .That()
        .HaveNameEndingWith("Request")
        .Or()
        .HaveNameEndingWith("Response")
        .Or()
        .HaveNameEndingWith("Error")
        .Or()
        .HaveNameEndingWith("Event");

    [Fact]
    public void CoreOnlyDependsOnDomainRelatedTypes()
    {
        Types()
            .That()
            .Are(_coreLayer)
            .Should()
            .OnlyDependOn(
                Types()
                    .That()
                    .Are(_coreLayer)
                    .Or()
                    .ResideInAssembly(AssembliesRepository.SharedKernel)
            )
            .Because(
                "Core, as a guarantor of business logic, must not depend on infrastructure details"
            )
            .Check(Architecture);
    }

    [Fact]
    public void UseCaseOnlyDependsOnDomainRelatedTypesAndInfrastructureAbstraction()
    {
        Types()
            .That()
            .Are(_useCasesLayer)
            .Should()
            .OnlyDependOn(
                Types()
                    .That()
                    .Are(_coreLayer)
                    .Or()
                    .ResideInAssembly(AssembliesRepository.SharedKernel)
            )
            .Because(
                "As the bearer of business application logic, UseCase must not depend on infrastructure details"
            )
            .Check(Architecture);
    }

    [Fact]
    /// Because a feature must cover just one use case
    public void EachFeatureHaveOnlyOneUseCase()
    {
        var classesByFeature = Classes()
            .That()
            .Are(_featuresLayer)
            .GetObjects(Architecture)
            .GroupBy(classesInFeaturesLayer => classesInFeaturesLayer.Namespace.FullName);

        Assert.All(
            classesByFeature,
            featureClasses =>
                Assert.Single(
                    collection: featureClasses,
                    predicate: featureClass => featureClass.FullName.EndsWith("UseCase")
                )
        );
    }

    [Fact]
    // Because a feature must have atan entry point to be triggered (could be many)
    public void EachFeatureHaveAtLeastOneEndpoint()
    {
        var classesByFeature = Classes()
            .That()
            .Are(_featuresLayer)
            .GetObjects(Architecture)
            .GroupBy(classesInFeaturesLayer => classesInFeaturesLayer.Namespace.FullName);

        Assert.All(
            classesByFeature,
            featureClasses =>
                Assert.True(
                    featureClasses
                        .Where(featureClass => featureClass.FullName.EndsWith("Endpoint"))
                        .Any(),
                    $"{featureClasses.Key} Feature has no endpoint class"
                )
        );
    }

    [Fact]
    // Since an endpoint represents only one feature, and a feature is covered by only one use case, an endpoint must refer to only one use case
    public void EachEndpointReferenceOnlyOneUseCase()
    {
        var endpoints = Classes().That().Are(_endpointsLayer).GetObjects(Architecture);
        var endpointsByNumberOfUseCaseReferenced = endpoints.Select(endpoint =>
            (
                endpoint,
                UseCases: endpoint
                    .Dependencies.Select(x => x.Target.FullName)
                    .Where(x => x.EndsWith("UseCase"))
                    .Distinct()
            )
        );

        Assert.All(
            endpointsByNumberOfUseCaseReferenced,
            endpointClassWithNumberUseCaseReferenced =>
                Assert.True(
                    endpointClassWithNumberUseCaseReferenced.UseCases.Count() == 1,
                    @$"{endpointClassWithNumberUseCaseReferenced.endpoint.FullName} endpoint not referencing only one use case
                    ({endpointClassWithNumberUseCaseReferenced.UseCases.Count()} referenced) {Environment.NewLine}
                        {string.Join(Environment.NewLine, endpointClassWithNumberUseCaseReferenced.UseCases)}"
                )
        );
    }

    [Fact]
    // Because record, with their immutability and compairaon by values, are the most suitable for representing a dto
    public void DtosAreRecords()
    {
        var dtosTypes = Classes().That().Are(_dtosLayer).GetObjects(Architecture);

        Assert.All(
            dtosTypes,
            dtoType => Assert.True(dtoType.IsRecord, $"{dtoType.FullName} Dto is not a record")
        );
    }

    [Fact]
    public void AllTypesAreInternals()
    {
        Types()
            .That()
            .ResideInAssembly(
                AssembliesRepository.FunctionalDomains[0],
                AssembliesRepository.FunctionalDomains.Skip(1).ToArray()
            )
            .And()
            .AreNot(
                typeof(FunctionalDomainNameARegistration),
                typeof(FunctionalDomainNameBRegistration)
            )
            .Should()
            .BeInternal()
            .Because(
                "functional domain must be considered as an isolated module that could be its own independent component"
            )
            .Check(Architecture);
    }
}
