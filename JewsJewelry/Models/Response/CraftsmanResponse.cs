namespace JewsJewelry.API.Models.Response
{
    public class CraftsmanResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя мастера
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия мастера
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Отчество мастера
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Возраст мастера
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Сущность мастерской
        /// </summary>
        public WorkshopResponse? Workshops { get; set; }
    }
}
