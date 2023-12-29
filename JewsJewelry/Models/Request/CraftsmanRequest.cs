using JewsJewelry.API.Models.CreateRequest;

namespace JewsJewelry.API.Models.Request
{
    public class CraftsmanRequest : CreateCraftsmanReq
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
