namespace JewsJewelry.API.Models.Response
{
    public class MaterialResponse
    {
        /// <summary>
        /// Название материала
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цвет материала
        /// </summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// Проба металла
        /// </summary>
        public int Sample { get; set; }

        /// <summary>
        /// Количество материала (В граммах)
        /// </summary>
        public int Amount { get; set; }
    }
}
