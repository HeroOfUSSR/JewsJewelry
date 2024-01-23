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
    public class JewelryModelValidatorTest
    {
        private JewelryModelValidator validator;

        public JewelryModelValidatorTest()
        {
            validator = new JewelryModelValidator();
        }

        /// <summary>
        /// Тест должен выдать ошибку валидации
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.JewelryModel(c =>
            {
                c.Name = "0";
                c.Cost = 0;
                c.Weight = 0;
                c.Description = "0";
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
            var model = TestDataGenerator.JewelryModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
