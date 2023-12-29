using JewsJewelry.API.Models.CreateRequest;

namespace JewsJewelry.API.Models.Request
{
    public class OrderRequest : CreateWorkshopReq
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
