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
    public class CustomerModelValidatorTest
    {
        private CustomerModelValidator validator;

        public CustomerModelValidatorTest()
        {
            validator = new CustomerModelValidator();
        }

        /// <summary>
        /// Тест должен выдать ошибку валидации
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.CustomerModel(c =>
            {
                c.Name = "0";
                c.Surname = "0";
                c.Patronymic = "0";
                c.PhoneNumber = "0";
                c.Email = "0";
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
            var model = TestDataGenerator.CustomerModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
