using Application.Models;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
    public class NewPropertyValidator : AbstractValidator<NewProperty>
    {
        public NewPropertyValidator() 
        {
            RuleFor(np => np.Name)
                .NotEmpty().WithMessage("Property name is required.")
                .MaximumLength(15).WithMessage("Name should not exceed 15 charactors.");
        }
    }
}
