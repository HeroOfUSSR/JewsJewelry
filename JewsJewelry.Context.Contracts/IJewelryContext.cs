using JewsJewelry.Context.Contracts.Models;

namespace JewsJewelry.Context.Contracts
{
    public interface IJewelryContext
    {
        /// <summary>
        /// Мастера
        /// </summary>
        IEnumerable<Craftsman> Craftsmen { get; }
        /// <summary>
        /// Заказчики
        /// </summary>
        IEnumerable<Customer> Customers { get; }
        /// <summary>
        /// Ювелирные изделия
        /// </summary>
        IEnumerable<Jewelries> Jewelries { get; }
        /// <summary>
        /// Материалы
        /// </summary>
        IEnumerable<Materials> Materials { get; }
        /// <summary>
        /// Заказы
        /// </summary>
        IEnumerable<Orders> Orders { get; }
        /// <summary>
        /// Мастерские
        /// </summary>
        IEnumerable<Workshop> Workshops { get; }



    }
}