using JewsJewelry.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace JewsJewelry.Context.Contracts
{
    public interface IJewelryContext
    {
        /// <summary> Список <inheritdoc cref="Craftsman"></summary>
        DbSet<Craftsman> Craftsmen { get; }

        /// <summary> Список <inheritdoc cref="Customer"></summary>
        DbSet<Customer> Customers { get; }

        /// <summary> Список <inheritdoc cref="Jewelry"></summary>
        DbSet<Jewelry> Jewelries { get; }

        /// <summary> Список <inheritdoc cref="Material"></summary>
        DbSet<Material> Materials { get; }

        /// <summary> Список <inheritdoc cref="Order"></summary>
        DbSet<Order> Orders { get; }

        /// <summary> Список <inheritdoc cref="Workshop"></summary>
        DbSet<Workshop> Workshops { get; }



    }
}