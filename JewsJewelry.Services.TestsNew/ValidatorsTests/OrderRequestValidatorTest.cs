using FluentValidation.TestHelper;
using JewsJewelry.Context.Tests;
using JewsJewelry.Repositories.Implementations;
using JewsJewelry.Services.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewsJewelry.Services.TestsNew.ValidatorsTests
{
    public class OrderRequestValidatorTest : JewsJewelryContextInMemory
    {
        private OrderRequestValidator validator;

        public OrderRequestValidatorTest()
        {
            validator = new OrderRequestValidator(new JewelryReadRepository(Reader),
                new CustomerReadRepository(Reader), new WorkshopReadRepository(Reader));
        }

        /// <summary>
        /// Тест должен выдать ошибку валидации
        /// </summary>
        [Fact]
        public async void ValidatorShouldErrorAsync()
        {
            //Arrange
            var model = TestDataGenerator.OrderRequestModel(c =>
            {
                c.Name = "0";
                c.Description = "0";
                c.OrderDate = DateTimeOffset.Now;
                c.DoneDate = DateTimeOffset.Now;
            });
            model.JewelryId = Guid.NewGuid();
            model.CustomerId = Guid.NewGuid();
            model.WorkshopId = Guid.NewGuid();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Все чики пуки
        /// </summary>
        [Fact]
        async public void ValidatorShouldSuccess()
        {
            //Arrange
            var jewelry = TestDataGenerator.Jewelry();
            var workshop = TestDataGenerator.Workshop();
            var customer = TestDataGenerator.Customer();

            await Context.Jewelries.AddAsync(jewelry);
            await Context.Workshops.AddAsync(workshop);
            await Context.Customers.AddAsync(customer);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            var model = TestDataGenerator.OrderRequestModel();
            model.JewelryId = jewelry.Id;
            model.WorkshopId = workshop.Id;
            model.CustomerId = customer.Id;

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
