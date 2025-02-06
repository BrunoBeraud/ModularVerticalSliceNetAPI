using FluentValidation;

namespace ComponentName.FunctionalDomainNameB.Features.CreateResourceB;

internal record CreateResourceBRequest(string SomeProperty);

internal class CreateResourceBRequestValidator : AbstractValidator<CreateResourceBRequest>
{
    public CreateResourceBRequestValidator()
    {
        RuleFor(x => x.SomeProperty).NotNull();
    }
}
