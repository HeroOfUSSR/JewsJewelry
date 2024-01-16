using FluentAssertions;
using JewsJewelry.Context.Tests;
using JewsJewelry.Repositories.Contracts.Interface;
using JewsJewelry.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewsJewelry.Repositories.Tests.Tests
{
    public class WorkshopReadTest : JewsJewelryContextInMemory
    {
        private readonly IWorkshopReadRepository workshopReadRepository;

        public WorkshopReadTest()
        {
            workshopReadRepository = new WorkshopReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список мастерских
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await workshopReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }

        /// <summary>
        /// Возвращает список мастерских
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var target = TestDataGenerator.Workshop();

            await Context.Workshops.AddRangeAsync(target,
                TestDataGenerator.Workshop(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await workshopReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(1).And
                .ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение мастерской по айди возвращает NULL
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await workshopReadRepository.GetByIdAsync(id, cancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получение мастерской по айди возвращает что-то
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Workshop();
            await Context.Workshops.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await workshopReadRepository.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка мастерских по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            // Arrange
            var idFirst = Guid.NewGuid();
            var idSecond = Guid.NewGuid();
            var idThird = Guid.NewGuid();

            // Act
            var result = await workshopReadRepository.GetByIdsAsync(new[] { idFirst, idSecond, idThird }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }


        /// <summary>
        /// Получение списка мастерских по айдишникам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var targetFirst = TestDataGenerator.Workshop();
            var targetSecond = TestDataGenerator.Workshop(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var targetThird = TestDataGenerator.Workshop();
            var targetFourth = TestDataGenerator.Workshop();
            await Context.Workshops.AddRangeAsync(targetFirst, targetSecond, targetThird, targetFourth);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await workshopReadRepository.GetByIdsAsync(new[] { targetFirst.Id, targetSecond.Id, targetFourth.Id }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(2).And
                .ContainKey(targetFirst.Id).And
                .ContainKey(targetFourth.Id);
        }

        /// <summary>
        /// Поиск мастерских по айди = TRUE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target = TestDataGenerator.Workshop();
            await Context.Workshops.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await workshopReadRepository.IsNotNullAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Поиск мастерской по айди = FALSE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target = Guid.NewGuid();

            // Act
            var result = await workshopReadRepository.IsNotNullAsync(target, cancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Поиск удаленной мастерской по айди
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Workshop(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Workshops.AddAsync(target1);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await workshopReadRepository.IsNotNullAsync(target1.Id, cancellationToken);

            // Assert
            result.Should().BeFalse();
        }

    }
}
