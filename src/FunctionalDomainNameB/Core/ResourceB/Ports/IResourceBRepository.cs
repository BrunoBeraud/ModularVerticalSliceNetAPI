using FunctionalDomainNameB.Core.ResourceB;

using SharedKernel;

namespace FunctionalDomainNameB.Core.ResourceB.Ports;

internal interface IResourceBRepository : IRepository<ResourceBEntity, ResourceBId> { }