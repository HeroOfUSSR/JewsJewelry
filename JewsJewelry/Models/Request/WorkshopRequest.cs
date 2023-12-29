using JewsJewelry.API.Models.CreateRequest;

namespace JewsJewelry.API.Models.Request
{
    public class WorkshopRequest : CreateWorkshopReq
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
