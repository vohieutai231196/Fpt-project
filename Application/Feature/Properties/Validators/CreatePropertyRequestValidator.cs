using Application.Feature.Properties.Command;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
    public class CreatePropertyRequestValidator : AbstractValidator<CreatePropertyRequest>
    {
        public CreatePropertyRequestValidator()
        {
            RuleFor(request => request.PropertyRequest)
                .SetValidator(new NewPropertyValidator());
        }
    }
}
