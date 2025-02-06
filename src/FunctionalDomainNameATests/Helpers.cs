using ComponentName.TestsHelpers;

namespace ComponentName.FunctionalDomainNameATests;

// XUnit limitation: collection definitions must be in the same assembly as the test that uses them.
[CollectionDefinition(nameof(BaseFunctionalTestsCollectionFixture))]
public class WebApplicationFactoryCollection
    : ICollectionFixture<FunctionalTestsApplicationFactory> { }
