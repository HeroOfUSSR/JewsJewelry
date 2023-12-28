using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Contracts.WriteReposInterface
{
    /// <summary>
    /// Интерфейс для работы с БД
    /// </summary>
    /// <typeparam name="TEntity"> Сущность из БД</typeparam>
    public interface IRepositoryWriter<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавление записи
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Обновление записи
        /// </summary>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Удаление записи
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}
