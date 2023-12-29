using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Validation
{
    /// <summary>
    /// Сервис валидации
    /// </summary>
     public interface IServiceValidator
    {
        /// <summary>
        /// Валидация модели
        /// </summary>
        Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class;
    }
}
