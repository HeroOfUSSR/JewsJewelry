using JewsJewelry.API.Models.CreateRequest;

namespace JewsJewelry.API.Models.Request
{
    public class CustomerRequest : CreateCustomerReq
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
