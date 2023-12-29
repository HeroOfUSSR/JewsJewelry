namespace JewsJewelry.API.Models.CreateRequest
{
    public class CreateJewelryReq
    {
        /// <summary>
        /// ID материала
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость изделия
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Масса изделия
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Описание изделия
        /// </summary>
        public string? Description { get; set; }
    }
}
