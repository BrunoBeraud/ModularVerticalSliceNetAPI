using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit3;

namespace ComponentName.TestsHelpers;

public class AutoDomainDataAttribute : AutoDataAttribute
{
    public AutoDomainDataAttribute()
        : base(() => new Fixture().Customize(new AutoMoqCustomization())) { }
}
