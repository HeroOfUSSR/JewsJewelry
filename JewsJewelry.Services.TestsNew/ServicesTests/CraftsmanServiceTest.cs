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
    public class CraftsmanServiceTest : JewsJewelryContextInMemory
    {
        private readonly ICraftsmanService craftsmanService;
        private readonly CraftsmanReadRepository craftsmanReadRepository;

        public CraftsmanServiceTest()
        {
            var config = new MapperConfiguration(c => { c.AddProfile(new ServiceProfile()); 
            });

            craftsmanReadRepository = new CraftsmanReadRepository(Reader);

            craftsmanService = new CraftsmanService(craftsmanReadRepository, 
                new CraftsmanWriteRepository(WriterContext),
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
            Func<Task> result = () => craftsmanService.GetByIdAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Craftsman>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение мастеров по айди возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Craftsman();
            await Context.Craftsmen.AddAsync( target );
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await craftsmanService.GetByIdAsync(target.Id, cancellationToken);
            
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
                    target.Age
                });
        }

        /// <summary>
        /// Получение мастеров по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await craftsmanService.GetAllAsync(cancellationToken);

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
            var target = TestDataGenerator.Craftsman();
            await Context.Craftsmen.AddRangeAsync(target,
                TestDataGenerator.Craftsman(c => c.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await craftsmanService.GetAllAsync(cancellationToken);

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
            Func<Task> result = () => craftsmanService.DeleteAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Craftsman>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного мастера
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            // Arrange
            var model = TestDataGenerator.Craftsman(c => c.DeletedAt = DateTime.UtcNow);
            await Context.Craftsmen.AddAsync(model);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> result = () => craftsmanService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Craftsman>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление мастеров
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Craftsman();
            await Context.Craftsmen.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> act = () => craftsmanService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Craftsmen.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.CraftsmanModel();

            // Act
            Func<Task> act = () => craftsmanService.AddAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Craftsmen.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.CraftsmanModel(c => c.Name = "A");

            // Act
            Func<Task> act = () => craftsmanService.AddAsync(model, cancellationToken);

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
            var model = TestDataGenerator.CraftsmanModel();

            // Act
            Func<Task> act = () => craftsmanService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<EntityNtFoundException<Craftsman>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого мастера
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            // Arrange
            var model = TestDataGenerator.CraftsmanModel(x => x.Name = "A");

            // Act
            Func<Task> act = () => craftsmanService.EditAsync(model, cancellationToken);

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
            var model = TestDataGenerator.CraftsmanModel();
            var craftsman = TestDataGenerator.Craftsman(x => x.Id = model.Id);
            await Context.Craftsmen.AddAsync(craftsman);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            //Act
            Func<Task> act = () => craftsmanService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Craftsmen.Single(x => x.Id == craftsman.Id);
                entity.Should()
                .NotBeNull().And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Name,
                    model.Surname,
                    model.Patronymic,
                    model.PhoneNumber,
                    model.Age
                });
        }

    }
}
