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
    public class OrderReadTest : JewsJewelryContextInMemory
    {
        private readonly IOrderReadRepository orderReadRepository;

        public OrderReadTest()
        {
            orderReadRepository = new OrderReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список заказов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await orderReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }

        /// <summary>
        /// Возвращает список заказов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var target = TestDataGenerator.Order();

            await Context.Orders.AddRangeAsync(target,
                TestDataGenerator.Order(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await orderReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(1).And
                .ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение заказов по айди возвращает NULL
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await orderReadRepository.GetByIdAsync(id, cancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получение заказов по айди возвращает что-то
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Order();
            await Context.Orders.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await orderReadRepository.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(target);
        }


        /// <summary>
        /// Поиск заказов по айди = TRUE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target = TestDataGenerator.Order();
            await Context.Orders.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await orderReadRepository.IsNotNullAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Поиск заказов по айди = FALSE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target = Guid.NewGuid();

            // Act
            var result = await orderReadRepository.IsNotNullAsync(target, cancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Поиск удаленных заказов по айди
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Order(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Orders.AddAsync(target1);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await orderReadRepository.IsNotNullAsync(target1.Id, cancellationToken);

            // Assert
            result.Should().BeFalse();
        }

    }
}
