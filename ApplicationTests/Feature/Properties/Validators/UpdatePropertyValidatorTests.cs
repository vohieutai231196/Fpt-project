using Application.Models;
using Application.Responsitories;
using Domain;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Xunit;

namespace Application.Features.Properties.Validators.Tests
{
    public class UpdatePropertyValidatorTests
    {
        private readonly Mock<IPropertyRepon> _propertyRepon;
        public UpdatePropertyValidatorTests() 
        { 
            _propertyRepon = new Mock<IPropertyRepon>();
        }
        [Fact()]
        public async Task UpdatePropertyValidatorTest_UpdatePropertyWithHaveId()
        {
            
            var command = new UpdateProperty()
            {
                Id = 1,
                Name = "Hihihaha",
                Description = "Description",
                Type = "aa",
                ErfSize = "aaa",
                FloorSize = "aaa",
                Price = 3,
                Levies = 4,
                Rates = 5,
                Address = "Tran Hung Dao",
                PetsAllowed = true,
                Bedrooms = 2,
                Bathrooms = 1,
                Kitchens = 3,
                Louge = 4,
                Dining = 5
            };

            var property = new Domain.Property()
            {
                Id = 1,
                Name = "Hihihaha",
                Description = "Description",
                Type = "aa",
                ErfSize = "aaa",
                FloorSize = "aaa",
                Price = 3,
                Levies = 4,
                Rates = 5,
                Address = "Tran Hung Dao",
                PetsAllowed = true,
                Bedrooms = 2,
                Bathrooms = 1,
                Kitchens = 3,
                Louge = 4,
                Dining = 5,
                ListDate = DateTime.Now,
            };

            _propertyRepon.Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync(property);

            var validator = new UpdatePropertyValidator(_propertyRepon.Object);
          
            //act
            var result = await validator.TestValidateAsync(command);

            //assert

             result.ShouldNotHaveAnyValidationErrors();
        }
    }
}