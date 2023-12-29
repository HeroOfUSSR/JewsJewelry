using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Context.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context
{
    /// <summary>
    /// Метода расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsContext
    {
        /// <summary>
        /// Регистрация всего, что связано с контекстом
        /// </summary>
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<IJewelryContext>(provider => provider.GetRequiredService<JewelryContext>());
            service.TryAddScoped<IDBRead>(provider => provider.GetRequiredService<JewelryContext>());
            service.TryAddScoped<IDBWriter>(provider => provider.GetRequiredService<JewelryContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<JewelryContext>());

        }
    }
}
