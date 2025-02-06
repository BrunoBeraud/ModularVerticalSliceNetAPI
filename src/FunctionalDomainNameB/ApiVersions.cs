using Asp.Versioning;

namespace ComponentName.FunctionalDomainNameB;

internal static class ApiVersions
{
    public static readonly ApiVersion V1 = new(1);
    public static readonly ApiVersion V2 = new(2);
    public static readonly ApiVersion[] All = [V1, V2];
}
