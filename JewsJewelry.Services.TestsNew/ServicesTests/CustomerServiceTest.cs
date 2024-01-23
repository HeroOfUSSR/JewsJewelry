using AutoMapper;
using FluentAssertions;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Context.Tests;
using JewsJewelry.Repositories.Implementations;
using JewsJewelry.Repositories.WriteRepository;
using JewsJewelry.Services.Automappers;
using JewsJewelry.Services.Contracts.Exceptions;
using JewsJewelry.Services.Contracts.Interface;
using JewsJewelry.Services.Implementations;
using JewsJewelry.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewsJewelry.Services.TestsNew.ServicesTests
{
    public class CustomerServiceTest : JewsJewelryContextInMemory
    {
        private readonly ICustomerService customerService;
        private readonly CustomerReadRepository customerReadRepository;

        public CustomerServiceTest()
        {
            var config = new MapperConfiguration(c => {
                c.AddProfile(new ServiceProfile());
            });

            customerReadRepository = new CustomerReadRepository(Reader);

            customerService = new CustomerService(customerReadRepository,
                new CustomerWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper(),
                new ServiceValidator(new JewelryReadRepository(Reader),
                new CustomerReadRepository(Reader), new WorkshopReadRepository(Reader)));
        }

        /// <summary>
        /// Получение мастеров по айди возвращает NULL
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => customerService.GetByIdAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Customer>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение мастеров по айди возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Customer();
            await Context.Customers.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await customerService.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Surname,
                    target.Patronymic,
                    target.PhoneNumber,
                    target.Email
                });
        }

        /// <summary>
        /// Получение мастеров по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await customerService.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }

        /// <summary>
        /// Получение мастеров по айди возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var target = TestDataGenerator.Customer();
            await Context.Customers.AddRangeAsync(target,
                TestDataGenerator.Customer(c => c.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await customerService.GetAllAsync(cancellationToken);

            result.Should()
                .NotBeNull().And
                .HaveCount(1).And
                .ContainSingle(c => c.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего мастера
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => customerService.DeleteAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Customer>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного мастера
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            // Arrange
            var model = TestDataGenerator.Customer(c => c.DeletedAt = DateTime.UtcNow);
            await Context.Customers.AddAsync(model);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> result = () => customerService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Customer>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление мастеров
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Customer();
            await Context.Customers.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> act = () => customerService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Customers.Single(c => c.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление мастера
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.CustomerModel();

            // Act
            Func<Task> act = () => customerService.AddAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Customers.Single(c => c.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление невалидируемого мастера
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            // Arrange
            var model = TestDataGenerator.CustomerModel(c => c.Name = "A");

            // Act
            Func<Task> act = () => customerService.AddAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidsException>();
        }

        /// <summary>
        /// Изменение несуществующего мастера
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            // Arrange
            var model = TestDataGenerator.CustomerModel();

            // Act
            Func<Task> act = () => customerService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<EntityNtFoundException<Customer>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого мастера
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            // Arrange
            var model = TestDataGenerator.CustomerModel(x => x.Name = "A");

            // Act
            Func<Task> act = () => customerService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidsException>();
        }

        /// <summary>
        /// Изменение мастера
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.CustomerModel();
            var customer = TestDataGenerator.Customer(x => x.Id = model.Id);
            await Context.Customers.AddAsync(customer);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            //Act
            Func<Task> act = () => customerService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Customers.Single(x => x.Id == customer.Id);
            entity.Should()
            .NotBeNull().And
            .BeEquivalentTo(new
            {
                entity.Id,
                entity.Name,
                entity.Surname,
                entity.Patronymic,
                entity.PhoneNumber,
                entity.Email
            });
        }

    }
}
