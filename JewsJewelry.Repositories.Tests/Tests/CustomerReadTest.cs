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

    public class CustomerReadTest : JewsJewelryContextInMemory
    {
        private readonly ICustomerReadRepository customerReadRepository;

        public CustomerReadTest()
        {
            customerReadRepository = new CustomerReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список заказчиков
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await customerReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }

        /// <summary>
        /// Возвращает список заказчиков
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var target = TestDataGenerator.Customer();

            await Context.Customers.AddRangeAsync(target,
                TestDataGenerator.Customer(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await customerReadRepository.GetAllAsync(cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(1).And
                .ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение заказчиков по айди возвращает NULL
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await customerReadRepository.GetByIdAsync(id, cancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получение заказчиков по айди возвращает что-то
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var target = TestDataGenerator.Customer();
            await Context.Customers.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await customerReadRepository.GetByIdAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка заказчиков по айди возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            // Arrange
            var idFirst = Guid.NewGuid();
            var idSecond = Guid.NewGuid();
            var idThird = Guid.NewGuid();

            // Act
            var result = await customerReadRepository.GetByIdsAsync(new[] { idFirst, idSecond, idThird }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .BeEmpty();
        }


        /// <summary>
        /// Получение списка заказчиков по айдишникам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var targetFirst = TestDataGenerator.Customer();
            var targetSecond = TestDataGenerator.Customer(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var targetThird = TestDataGenerator.Customer();
            var targetFourth = TestDataGenerator.Customer();
            await Context.Customers.AddRangeAsync(targetFirst, targetSecond, targetThird, targetFourth);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await customerReadRepository.GetByIdsAsync(new[] { targetFirst.Id, targetSecond.Id, targetFourth.Id }, cancellationToken);

            // Assert
            result.Should()
                .NotBeNull().And
                .HaveCount(2).And
                .ContainKey(targetFirst.Id).And
                .ContainKey(targetFourth.Id);
        }

        /// <summary>
        /// Поиск заказчиков по айди = TRUE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target = TestDataGenerator.Customer();
            await Context.Customers.AddAsync(target);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await customerReadRepository.IsNotNullAsync(target.Id, cancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Поиск заказчиков по айди = FALSE
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target = Guid.NewGuid();

            // Act
            var result = await customerReadRepository.IsNotNullAsync(target, cancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Поиск удаленного заказчика по айди
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Customer(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Customers.AddAsync(target1);
            await Context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await customerReadRepository.IsNotNullAsync(target1.Id, cancellationToken);

            // Assert
            result.Should().BeFalse();
        }

    }
}
