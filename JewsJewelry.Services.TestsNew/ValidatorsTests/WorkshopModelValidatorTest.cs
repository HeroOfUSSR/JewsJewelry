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
    public class WorkshopModelValidatorTest
    {
        private WorkshopModelValidator validator;

        public WorkshopModelValidatorTest()
        {
            validator = new WorkshopModelValidator();
        }

        /// <summary>
        /// Тест должен выдать ошибку валидации
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.WorkshopModel(c =>
            {
                c.Name = "0";
                c.Address = "0";
                c.Workplaces = 0;
                c.Speciality = "0";
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
            var model = TestDataGenerator.WorkshopModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
