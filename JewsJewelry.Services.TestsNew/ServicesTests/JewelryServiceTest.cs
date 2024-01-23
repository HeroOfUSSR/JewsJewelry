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
    public class JewelryServiceTest : JewsJewelryContextInMemory
    {
        private readonly IJewelryService jewelryService;
        private readonly JewelryReadRepository jewelryReadRepository;

        public JewelryServiceTest()
        {
            var config = new MapperConfiguration(c => {
                c.AddProfile(new ServiceProfile());
            });

            jewelryReadRepository = new JewelryReadRepository(Reader);

            jewelryService = new JewelryService(jewelryReadRepository,
                new JewelryWriteRepository(WriterContext),
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
            Func<Task> result = () => jewelryService.GetByIdAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Jewelry>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение мастеров по айди возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Jewelry();
            await Context.Jewelries.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await jewelryService.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Cost,
                    target.Weight,
                    target.Description
                });
        }

        /// <summary>
        /// Получение мастеров по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await jewelryService.GetAllAsync(cancellationToken);

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
            var target = TestDataGenerator.Jewelry();
            await Context.Jewelries.AddRangeAsync(target,
                TestDataGenerator.Jewelry(c => c.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await jewelryService.GetAllAsync(cancellationToken);

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
            Func<Task> result = () => jewelryService.DeleteAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Jewelry>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного мастера
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            // Arrange
            var model = TestDataGenerator.Jewelry(c => c.DeletedAt = DateTime.UtcNow);
            await Context.Jewelries.AddAsync(model);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> result = () => jewelryService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Jewelry>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление мастеров
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Jewelry();
            await Context.Jewelries.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> act = () => jewelryService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Jewelries.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.JewelryModel();

            // Act
            Func<Task> act = () => jewelryService.AddAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Jewelries.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.JewelryModel(c => c.Name = "A");

            // Act
            Func<Task> act = () => jewelryService.AddAsync(model, cancellationToken);

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
            var model = TestDataGenerator.JewelryModel();

            // Act
            Func<Task> act = () => jewelryService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<EntityNtFoundException<Jewelry>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого мастера
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            // Arrange
            var model = TestDataGenerator.JewelryModel(x => x.Name = "A");

            // Act
            Func<Task> act = () => jewelryService.EditAsync(model, cancellationToken);

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
            var model = TestDataGenerator.JewelryModel();
            var jewelry = TestDataGenerator.Jewelry(x => x.Id = model.Id);
            await Context.Jewelries.AddAsync(jewelry);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            //Act
            Func<Task> act = () => jewelryService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Jewelries.Single(x => x.Id == jewelry.Id);
            entity.Should()
            .NotBeNull().And
            .BeEquivalentTo(new
            {
                entity.Id,
                entity.Name,
                entity.Cost,
                entity.Weight,
                entity.Description
            });
        }

    }
}
