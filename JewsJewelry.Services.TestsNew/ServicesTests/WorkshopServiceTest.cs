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
    public class WorkshopServiceTest : JewsJewelryContextInMemory
    {
        private readonly IWorkshopService workshopService;
        private readonly WorkshopReadRepository workshopReadRepository;

        public WorkshopServiceTest()
        {
            var config = new MapperConfiguration(c => {
                c.AddProfile(new ServiceProfile());
            });

            workshopReadRepository = new WorkshopReadRepository(Reader);

            workshopService = new WorkshopService(workshopReadRepository,
                new WorkshopWriteRepository(WriterContext),
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
            Func<Task> result = () => workshopService.GetByIdAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Workshop>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение мастеров по айди возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Workshop();
            await Context.Workshops.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await workshopService.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Address,
                    target.Speciality,
                    target.Workplaces
                });
        }

        /// <summary>
        /// Получение мастеров по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await workshopService.GetAllAsync(cancellationToken);

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
            var target = TestDataGenerator.Workshop();
            await Context.Workshops.AddRangeAsync(target,
                TestDataGenerator.Workshop(c => c.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await workshopService.GetAllAsync(cancellationToken);

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
            Func<Task> result = () => workshopService.DeleteAsync(id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Workshop>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного мастера
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            // Arrange
            var model = TestDataGenerator.Workshop(c => c.DeletedAt = DateTime.UtcNow);
            await Context.Workshops.AddAsync(model);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> result = () => workshopService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await result.Should().ThrowAsync<EntityNtFoundException<Workshop>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление мастеров
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Workshop();
            await Context.Workshops.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Act
            Func<Task> act = () => workshopService.DeleteAsync(model.Id, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Workshops.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.WorkshopModel();

            // Act
            Func<Task> act = () => workshopService.AddAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Workshops.Single(c => c.Id == model.Id);
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
            var model = TestDataGenerator.WorkshopModel(c => c.Name = "A");

            // Act
            Func<Task> act = () => workshopService.AddAsync(model, cancellationToken);

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
            var model = TestDataGenerator.WorkshopModel();

            // Act
            Func<Task> act = () => workshopService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<EntityNtFoundException<Workshop>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого мастера
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            // Arrange
            var model = TestDataGenerator.WorkshopModel(x => x.Name = "A");

            // Act
            Func<Task> act = () => workshopService.EditAsync(model, cancellationToken);

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
            var model = TestDataGenerator.WorkshopModel();
            var workshop = TestDataGenerator.Workshop(x => x.Id = model.Id);
            await Context.Workshops.AddAsync(workshop);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            //Act
            Func<Task> act = () => workshopService.EditAsync(model, cancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Workshops.Single(x => x.Id == workshop.Id);
            entity.Should()
            .NotBeNull().And
            .BeEquivalentTo(new
            {
                entity.Id,
                entity.Name,
                entity.Address,
                entity.Speciality,
                entity.Workplaces
            });
        }

    }
}
