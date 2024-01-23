using FluentValidation.TestHelper;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Services.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewsJewelry.Services.TestsNew.ValidatorsTests
{
    public class CraftsmanModelValidatorTest
    {
        private CraftsmanModelValidator validator;

        public CraftsmanModelValidatorTest()
        {
            validator = new CraftsmanModelValidator();
        }

        /// <summary>
        /// Тест должен выдать ошибку валидации
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.CraftsmanModel(c => 
            {   
                c.Name = "0"; 
                c.Surname = "0";
                c.Patronymic = "0";
                c.PhoneNumber = "0";
                c.Age = 0;
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
            var model = TestDataGenerator.CraftsmanModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
