using JewsJewelry.General;

namespace JewsJewelry.API.Exceptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class APIExcValidation
    {
        public IEnumerable<InvalidateItemModel> Errors { get; set; } 
            = Array.Empty<InvalidateItemModel>();
    }
}
