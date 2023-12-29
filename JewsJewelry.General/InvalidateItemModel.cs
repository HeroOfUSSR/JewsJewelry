using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.General
{
    /// <summary>
    /// Модель инвалидации запросов
    /// </summary>
    public class InvalidateItemModel
    {
        /// <summary>
        /// Создаёт <see cref="InvalidateItemModel"/>
        /// </summary>
        public static InvalidateItemModel New(string field, string message)
            => new InvalidateItemModel(field, message);

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="InvalidateItemModel"/>
        /// </summary>
        private InvalidateItemModel(string field, string message)
        {
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Имя поля
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
    }
}
