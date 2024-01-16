using JewsJewelry.Common.Entity;
using JewsJewelry.Common.Entity.DBInterface;
using Moq;

namespace JewsJewelry.Context.Tests
{
    internal class TestWriterContext : IDBWriterContext
    {
        public IDBWriter Writer { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IDateTimeProvider TimeProvider { get; }
        public string User { get; }

        public TestWriterContext(IDBWriter writer, IUnitOfWork unitOfWork)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.CurrentTime).Returns(DateTimeOffset.UtcNow);
            TimeProvider = dateTimeProviderMock.Object;
            User = "TestUser";
        }  
    }
}