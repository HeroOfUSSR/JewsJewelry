namespace JewsJewelry.API.Models.CreateRequest
{
    public class CreateOrderReq
    {
        /// <summary>
        /// ID изделия
        /// </summary>
        public Guid JewelryId { get; set; }

        /// <summary>
        /// ID покупателя
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// ID мастерской
        /// </summary>
        public Guid WorkshopId { get; set; }

        /// <summary>
        /// Наименование заказа
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание заказа
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Дата закрытия заказа
        /// </summary>
        public DateTimeOffset DoneDate { get; set; }
    }
}
