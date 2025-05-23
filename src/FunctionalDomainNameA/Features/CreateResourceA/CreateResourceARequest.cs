using FluentValidation;

namespace ComponentName.FunctionalDomainNameA.Features.CreateResourceA;

internal record CreateResourceARequest(string SomeProperty);

internal class CreateResourceARequestValidator : AbstractValidator<CreateResourceARequest>
{
    public CreateResourceARequestValidator()
    {
        RuleFor(x => x.SomeProperty).NotNull();
    }
}
