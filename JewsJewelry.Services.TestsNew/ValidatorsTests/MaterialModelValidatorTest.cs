using FluentValidation.TestHelper;
using JewsJewelry.Services.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewsJewelry.Services.TestsNew.ValidatorsTests
{
    public class MaterialModelValidatorTest
    {
        private MaterialModelValidator validator;

        public MaterialModelValidatorTest()
        {
            validator = new MaterialModelValidator();
        }

        /// <summary>
        /// Тест должен выдать ошибку валидации
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.MaterialModel(c =>
            {
                c.Name = "0";
                c.Amount = 0;
                c.Sample = 0;
                c.Color = "0";
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Все чики пуки
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.MaterialModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
