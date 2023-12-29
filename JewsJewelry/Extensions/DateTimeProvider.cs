using JewsJewelry.Common.Entity;

namespace JewsJewelry.API.Extensions
{
    /// <summary>
    /// Реализация <see cref="IDateTimeProvider"/>
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.CurrentTime => DateTimeOffset.UtcNow;
    }
}
