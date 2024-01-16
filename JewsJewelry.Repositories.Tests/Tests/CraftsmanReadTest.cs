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
    public class CraftsmanReadTest : JewsJewelryContextInMemory
    {
        private readonly ICraftsmanReadRepository craftsmanReadRepository;

        public CraftsmanReadTest()
        {
            craftsmanReadRepository = new CraftsmanReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список мастеров
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await craftsmanReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }

        /// <summary>
        /// Возвращает список мастеров
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var target = TestDataGenerator.Craftsman();

            await Context.Craftsmen.AddRangeAsync(target,
                TestDataGenerator.Craftsman(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await craftsmanReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(1).And
                .ContainSingle(x => x.Id == target.Id);
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
            var result = await craftsmanReadRepository.GetByIdAsync(id, cancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получение мастеров по айди возвращает что-то
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Craftsman();
            await Context.Craftsmen.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await craftsmanReadRepository.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка мастеров по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            // Arrange
            var idFirst = Guid.NewGuid();
            var idSecond = Guid.NewGuid();
            var idThird = Guid.NewGuid();

            // Act
            var result = await craftsmanReadRepository.GetByIdsAsync(new[] { idFirst, idSecond, idThird }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }


        /// <summary>
        /// Получение списка мастеров по айдишникам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var targetFirst = TestDataGenerator.Craftsman();
            var targetSecond = TestDataGenerator.Craftsman(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var targetThird = TestDataGenerator.Craftsman();
            var targetFourth = TestDataGenerator.Craftsman();
            await Context.Craftsmen.AddRangeAsync(targetFirst, targetSecond, targetThird, targetFourth);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await craftsmanReadRepository.GetByIdsAsync(new[] { targetFirst.Id, targetSecond.Id, targetFourth.Id }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(2).And
                .ContainKey(targetFirst.Id).And
                .ContainKey(targetFourth.Id);
        }

        /// <summary>
        /// Поиск мастеров по айди = TRUE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target = TestDataGenerator.Craftsman();
            await Context.Craftsmen.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await craftsmanReadRepository.IsNotNullAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Поиск мастеров по айди = FALSE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target = Guid.NewGuid();

            // Act
            var result = await craftsmanReadRepository.IsNotNullAsync(target, cancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Поиск удаленного мастера по айди
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Craftsman(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Craftsmen.AddAsync(target1);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await craftsmanReadRepository.IsNotNullAsync(target1.Id, cancellationToken);

            // Assert
            result.Should().BeFalse();
        }

    }
}
