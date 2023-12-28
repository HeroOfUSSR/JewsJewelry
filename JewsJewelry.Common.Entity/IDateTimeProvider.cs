namespace JewsJewelry.Common.Entity
{
    /// <summary>
    /// Интерфейс получения текущего время
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Текущее время
        /// </summary>
        DateTimeOffset CurrentTime { get; }
    }
}