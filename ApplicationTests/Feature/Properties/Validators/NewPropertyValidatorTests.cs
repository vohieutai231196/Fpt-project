using Xunit;

using Application.Models;
using FluentValidation.TestHelper;

namespace Application.Features.Properties.Validators.Tests
{
    public class NewPropertyValidatorTests
    {
        [Fact()]
        public void NewPropertyValidatorTest_WhenNameIsEmpty()
        {
            var command = new NewProperty()
            {
                Name = "",
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
            var validator = new NewPropertyValidator();

            // act
            var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
        [Fact()]
        public void NewPropertyValidatorTest_WhenNameIsNotEmpty()
        {
            var command = new NewProperty()
            {
                Name = "Bla",
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
            var validator = new NewPropertyValidator();

            // act
            var result = validator.TestValidate(command);

            //assert

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}