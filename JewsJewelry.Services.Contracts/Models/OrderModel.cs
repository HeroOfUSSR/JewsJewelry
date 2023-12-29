namespace JewsJewelry.Services.Contracts.Models
{
    public class OrderModel
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID изделия
        /// </summary>
        public JewelryModel Jewelry { get; set; }

        /// <summary>
        /// ID покупателя
        /// </summary>

        public CustomerModel Customer { get; set; }

        /// <summary>
        /// ID мастерской
        /// </summary>
        public WorkshopModel Workshop { get; set; }

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
