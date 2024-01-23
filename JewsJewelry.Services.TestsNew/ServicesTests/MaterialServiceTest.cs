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
using JewsMaterial.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewsJewelry.Services.TestsNew.ServicesTests
{
    public class MaterialServiceTest : JewsJewelryContextInMemory
    {
        private readonly IMaterialService materialService;
        private readonly MaterialReadRepository materialReadRepository;

        public MaterialServiceTest()
        {
            var config = new MapperConfiguration(c => {
                c.AddProfile(new ServiceProfile());
            });

            materialReadRepository = new MaterialReadRepository(Reader);

            materialService = new MaterialService(materialReadRepository,
                new MaterialWriteRepository(WriterContext),
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
            Func<Task> result = () => materialService.GetByIdAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Material>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение мастеров по айди возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Material();
            await Context.Materials.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await materialService.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(new
                {
                    result.Id,
                    result.Name,
                    result.Color,
                    result.Sample,
                    result.Amount
                });
        }

        /// <summary>
        /// Получение мастеров по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await materialService.GetAllAsync(cancellationToken);

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
            var target = TestDataGenerator.Material();
            await Context.Materials.AddRangeAsync(target,
                TestDataGenerator.Material(c => c.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await materialService.GetAllAsync(cancellationToken);

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
            Func<Task> result = () => materialService.DeleteAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Material>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного мастера
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            // Arrange
            var model = TestDataGenerator.Material(c => c.DeletedAt = DateTime.UtcNow);
            await Context.Materials.AddAsync(model);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> result = () => materialService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Material>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление мастеров
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Material();
            await Context.Materials.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> act = () => materialService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Materials.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.MaterialModel();

            // Act
            Func<Task> act = () => materialService.AddAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Materials.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.MaterialModel(c => c.Name = "A");

            // Act
            Func<Task> act = () => materialService.AddAsync(model, cancellationToken);

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
            var model = TestDataGenerator.MaterialModel();

            // Act
            Func<Task> act = () => materialService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<EntityNtFoundException<Material>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого мастера
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            // Arrange
            var model = TestDataGenerator.MaterialModel(x => x.Name = "A");

            // Act
            Func<Task> act = () => materialService.EditAsync(model, cancellationToken);

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
            var model = TestDataGenerator.MaterialModel();
            var material = TestDataGenerator.Material(x => x.Id = model.Id);
            await Context.Materials.AddAsync(material);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            //Act
            Func<Task> act = () => materialService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Materials.Single(x => x.Id == material.Id);
            entity.Should()
            .NotBeNull().And
            .BeEquivalentTo(new
            {
                model.Id,
                model.Name,
                model.Color,
                model.Sample,
                model.Amount
            });
        }

    }
}
