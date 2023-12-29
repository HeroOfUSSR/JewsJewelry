namespace JewsJewelry.API.Models.Response
{
    public class OrderResponse
    {
        /// <summary>
        /// Сущность изделия
        /// </summary>
        public JewelryResponse? Jewelries { get; set; }

        /// <summary>
        /// Сущность покупателя
        /// </summary>
        public CustomerResponse? Customer { get; set; }

        /// <summary>
        /// Сущность мастерской
        /// </summary>
        public WorkshopResponse? Workshop { get; set; }

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
        public string OrderDate { get; set; } = string.Empty;

        /// <summary>
        /// Дата закрытия заказа
        /// </summary>
        public string DoneDate { get; set; } = string.Empty;
    }
}
