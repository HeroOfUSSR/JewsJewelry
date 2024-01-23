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
    public class OrderServiceTest : JewsJewelryContextInMemory
    {
        private readonly IOrderService orderService;

        public OrderServiceTest()
        {
            var config = new MapperConfiguration(c => {
                c.AddProfile(new ServiceProfile());
            });

            orderService = new OrderService(new OrderReadRepository(Reader),
                new OrderWriteRepository(WriterContext),
                new JewelryReadRepository(Reader),
                new CustomerReadRepository(Reader),
                new WorkshopReadRepository(Reader),
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
            Func<Task> result = () => orderService.GetByIdAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Order>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение мастеров по айди возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Order();
            await Context.Orders.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await orderService.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description,
                    target.OrderDate,
                    target.DoneDate
                });
        }

        /// <summary>
        /// Получение мастеров по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await orderService.GetAllAsync(cancellationToken);

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
            var target = TestDataGenerator.Order();
            await Context.Orders.AddRangeAsync(target,
                TestDataGenerator.Order(c => c.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await orderService.GetAllAsync(cancellationToken);

            result.Should()
                .NotBeNull().And
                .HaveCount(0);
                
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
            Func<Task> result = () => orderService.DeleteAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Order>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного мастера
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            // Arrange
            var model = TestDataGenerator.Order(c => c.DeletedAt = DateTime.UtcNow);
            await Context.Orders.AddAsync(model);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> result = () => orderService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Order>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление мастеров
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Order();
            await Context.Orders.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> act = () => orderService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Orders.Single(c => c.Id == model.Id);
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
            var jewelry = TestDataGenerator.Jewelry();
            var customer = TestDataGenerator.Customer();
            var workshop = TestDataGenerator.Workshop();

            await Context.Jewelries.AddAsync(jewelry);
            await Context.Customers.AddAsync(customer);
            await Context.Workshops.AddAsync(workshop);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            var model = TestDataGenerator.OrderRequestModel();
            model.JewelryId = jewelry.Id;
            model.CustomerId = customer.Id;
            model.WorkshopId = workshop.Id;

            // Act
            Func<Task> act = () => orderService.AddAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Orders.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.OrderRequestModel();

            // Act
            Func<Task> act = () => orderService.AddAsync(model, cancellationToken);

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
            var jewelry = TestDataGenerator.Jewelry();
            var customer = TestDataGenerator.Customer();
            var workshop = TestDataGenerator.Workshop();

            await Context.Jewelries.AddAsync(jewelry);
            await Context.Customers.AddAsync(customer);
            await Context.Workshops.AddAsync(workshop);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            var model = TestDataGenerator.OrderRequestModel();
            model.JewelryId = jewelry.Id;
            model.CustomerId = customer.Id;
            model.WorkshopId = workshop.Id;


            // Act
            Func<Task> act = () => orderService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<EntityNtFoundException<Order>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого мастера
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            // Arrange
            var model = TestDataGenerator.OrderRequestModel();

            // Act
            Func<Task> act = () => orderService.EditAsync(model, cancellationToken);

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
            var jewelry = TestDataGenerator.Jewelry();
            var customer = TestDataGenerator.Customer();
            var workshop = TestDataGenerator.Workshop();

            await Context.Jewelries.AddAsync(jewelry);
            await Context.Customers.AddAsync(customer);
            await Context.Workshops.AddAsync(workshop);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            var order = TestDataGenerator.Order();
            order.JewelryId = jewelry.Id;
            order.CustomerId = customer.Id;
            order.WorkshopId = workshop.Id;

            var model = TestDataGenerator.OrderRequestModel();
            model.Id = order.Id;
            model.JewelryId = jewelry.Id;
            model.CustomerId = customer.Id;
            model.WorkshopId = workshop.Id;

            await Context.Orders.AddAsync(order);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            //Act
            Func<Task> act = () => orderService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Orders.Single(x => x.Id == order.Id);
            entity.Should()
            .NotBeNull().And
            .BeEquivalentTo(new
            {
                entity.Id,
                entity.Name,
                entity.Description,
                entity.OrderDate,
                entity.DoneDate
            });
        }

    }
}
