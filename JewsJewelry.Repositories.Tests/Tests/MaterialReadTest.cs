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
    public class MaterialReadTest : JewsJewelryContextInMemory
    {
        private readonly IMaterialReadRepository materialReadRepository;

        public MaterialReadTest()
        {
            materialReadRepository = new MaterialReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список материалов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await materialReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }

        /// <summary>
        /// Возвращает список материалов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var target = TestDataGenerator.Material();

            await Context.Materials.AddRangeAsync(target,
                TestDataGenerator.Material(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await materialReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(1).And
                .ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение материалов по айди возвращает NULL
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await materialReadRepository.GetByIdAsync(id, cancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получение материалов по айди возвращает что-то
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Material();
            await Context.Materials.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await materialReadRepository.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка материалов по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            // Arrange
            var idFirst = Guid.NewGuid();
            var idSecond = Guid.NewGuid();
            var idThird = Guid.NewGuid();

            // Act
            var result = await materialReadRepository.GetByIdsAsync(new[] { idFirst, idSecond, idThird }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }


        /// <summary>
        /// Получение списка материалов по айдишникам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var targetFirst = TestDataGenerator.Material();
            var targetSecond = TestDataGenerator.Material(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var targetThird = TestDataGenerator.Material();
            var targetFourth = TestDataGenerator.Material();
            await Context.Materials.AddRangeAsync(targetFirst, targetSecond, targetThird, targetFourth);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await materialReadRepository.GetByIdsAsync(new[] { targetFirst.Id, targetSecond.Id, targetFourth.Id }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(2).And
                .ContainKey(targetFirst.Id).And
                .ContainKey(targetFourth.Id);
        }

        /// <summary>
        /// Поиск материалов по айди = TRUE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target = TestDataGenerator.Material();
            await Context.Materials.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await materialReadRepository.IsNotNullAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Поиск материалов по айди = FALSE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target = Guid.NewGuid();

            // Act
            var result = await materialReadRepository.IsNotNullAsync(target, cancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Поиск удаленных материалов по айди
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Material(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Materials.AddAsync(target1);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await materialReadRepository.IsNotNullAsync(target1.Id, cancellationToken);

            // Assert
            result.Should().BeFalse();
        }

    }
}
