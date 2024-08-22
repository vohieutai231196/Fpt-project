using Application.Feature.Properties.Command;
using Application.Responsitories;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
    public class UpdatePropertyRequestValidator : AbstractValidator<UpdatePropertyRequest>
    {
        public UpdatePropertyRequestValidator(IPropertyRepon propertyRepo)
        {
            RuleFor(request => request.UpdateProperty)
                .SetValidator(new UpdatePropertyValidator(propertyRepo));
        }
    }
}
