using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Validation.Validators
{
    /// <summary>
    /// Сообщения для валидатора
    /// </summary>
    internal static class ValidatorMessage
    {
        public static string DefaultMessage = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

        /// <summary>
        /// Сообщение для ошибок длинны строк
        /// </summary>
        public static string LengthMessage = "Длина поля {PropertyName} должна быть от {MinLength} до {MaxLength}. Текущая длина: {TotalLength}";

        /// <summary>
        /// Сообщение для ошибок диапазона
        /// </summary>
        public static string InclusiveBetweenMessage = "Значение поля {PropertyName} должно находится в " +
            "диапозоне от {From} до {To} включительно. Тукущее значение равно {PropertyValue}";

        public static string NotFoundGuidMessage = "Вы ввели несуществующий Guid в поле {PropertyName}";
    }
}
