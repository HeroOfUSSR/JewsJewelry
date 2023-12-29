namespace JewsJewelry.API.Models.CreateRequest
{
    public class CreateWorkshopReq
    {
        /// <summary>
        /// Название мастерской
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Адрес мастерской
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Направленность мастерской
        /// </summary>
        public string Speciality { get; set; } = string.Empty;

        /// <summary>
        /// Количество рабочих мест
        /// </summary>
        public int Workplaces { get; set; }
    }
}
