using JewsJewelry.API.Models.CreateRequest;

namespace JewsJewelry.API.Models.Request
{
    public class MaterialRequest : CreateMaterialReq
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
