﻿using Application.Models;
using Application.Repositories;
using Application.Responsitories;
using Domain;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
    public class UpdatePropertyValidator : AbstractValidator<UpdateProperty>
    {
        public UpdatePropertyValidator(IPropertyRepon propertyRepo)
        {
            RuleFor(updateProperty => updateProperty.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(updateProperty => updateProperty.Id)
                .MustAsync(async (id, ct) => 
                    await propertyRepo.GetByIdAsync(id) is Property existingProperty && 
                        existingProperty.Id == id)
                .WithMessage("Property does not exist.");
        }
    }
}
