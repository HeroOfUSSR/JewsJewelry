using JewsJewelry.Common.Entity;
using JewsJewelry.Common.Entity.DBInterface;

namespace JewsJewelry.API.Extensions
{
    /// <summary>
    /// Реализация <see cref="IDBWriterContext"/>
    /// </summary>
    public class DbWriterContext : IDBWriterContext
    {
        public IDBWriter Writer { get; }

        public IUnitOfWork UnitOfWork { get; }

        public IDateTimeProvider TimeProvider { get; }

        public string User { get; } = "Чикибамбино";

        public DbWriterContext(IDBWriter writer, IUnitOfWork unitOfWork, IDateTimeProvider timeProvider)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            TimeProvider = timeProvider;
        }
    }
}
