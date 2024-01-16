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
    public class JewelryReadTest : JewsJewelryContextInMemory
    {
        private readonly IJewelryReadRepository jewelryReadRepository;

        public JewelryReadTest()
        {
            jewelryReadRepository = new JewelryReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список изделий
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await jewelryReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }

        /// <summary>
        /// Возвращает список изделий
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var target = TestDataGenerator.Jewelry();

            await Context.Jewelries.AddRangeAsync(target,
                TestDataGenerator.Jewelry(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await jewelryReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(1).And
                .ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение изделий по айди возвращает NULL
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await jewelryReadRepository.GetByIdAsync(id, cancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получение изделий по айди возвращает что-то
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Jewelry();
            await Context.Jewelries.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await jewelryReadRepository.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка изделий по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            // Arrange
            var idFirst = Guid.NewGuid();
            var idSecond = Guid.NewGuid();
            var idThird = Guid.NewGuid();

            // Act
            var result = await jewelryReadRepository.GetByIdsAsync(new[] { idFirst, idSecond, idThird }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }


        /// <summary>
        /// Получение списка изделий по айдишникам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var targetFirst = TestDataGenerator.Jewelry();
            var targetSecond = TestDataGenerator.Jewelry(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var targetThird = TestDataGenerator.Jewelry();
            var targetFourth = TestDataGenerator.Jewelry();
            await Context.Jewelries.AddRangeAsync(targetFirst, targetSecond, targetThird, targetFourth);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await jewelryReadRepository.GetByIdsAsync(new[] { targetFirst.Id, targetSecond.Id, targetFourth.Id }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(2).And
                .ContainKey(targetFirst.Id).And
                .ContainKey(targetFourth.Id);
        }

        /// <summary>
        /// Поиск изделий по айди = TRUE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target = TestDataGenerator.Jewelry();
            await Context.Jewelries.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await jewelryReadRepository.IsNotNullAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Поиск изделий по айди = FALSE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target = Guid.NewGuid();

            // Act
            var result = await jewelryReadRepository.IsNotNullAsync(target, cancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Поиск удаленного изделия по айди
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Jewelry(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Jewelries.AddAsync(target1);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await jewelryReadRepository.IsNotNullAsync(target1.Id, cancellationToken);

            // Assert
            result.Should().BeFalse();
        }

    }
}
