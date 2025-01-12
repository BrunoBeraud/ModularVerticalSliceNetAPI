using FunctionalDomainNameA.Core.ResourceA;

using SharedKernel;

namespace FunctionalDomainNameA.Core.ResourceA.Ports;

internal interface IResourceARepository : IRepository<ResourceAEntity, ResourceAId> { }