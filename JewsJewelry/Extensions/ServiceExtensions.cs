using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Common.Entity;
using JewsJewelry.Services;
using JewsJewelry.Repositories;
using JewsJewelry.Context;
using JewsJewelry.API.Automapper;
using JewsJewelry.Services.Automappers;
using Newtonsoft.Json.Converters;
using Microsoft.OpenApi.Models;

namespace JewsJewelry.API.Extensions
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Регистрирует все необходимое для контекста
        /// </summary>
        public static void RegistrationSRC(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDBWriterContext, DbWriterContext>();
            services.RegistrationService();
            services.RegistrationRepository();
            services.RegistrationContext();
            services.AddAutoMapper(typeof(AutoMapperrino), typeof(ServiceProfile));
        }

        public static void RegistrationControllers(this IServiceCollection service)
        {
            service.AddControllers(f =>
            {
                f.Filters.Add<OrderExceptionFilter>();
            })
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        CamelCaseText = false
                    });
                })
                .AddControllersAsServices();
        }

        /// <summary>
        /// Параметры сваггера
        /// </summary>
        public static void RegistrationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Craftsman", new OpenApiInfo { Title = "Мастера", Version = "v1" });
                c.SwaggerDoc("Customer", new OpenApiInfo { Title = "Заказчики", Version = "v1" });
                c.SwaggerDoc("Jewelry", new OpenApiInfo { Title = "Ювелирные изделия", Version = "v1" });
                c.SwaggerDoc("Material", new OpenApiInfo { Title = "Материалы", Version = "v1" });
                c.SwaggerDoc("Order", new OpenApiInfo { Title = "Заказы", Version = "v1" });
                c.SwaggerDoc("Workshop", new OpenApiInfo { Title = "Мастерские", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "JewsJewelry.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void CustomizeSwaggerUI(this WebApplication web)
        {
            web.UseSwagger();
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Craftsman/swagger.json", "Мастера");
                x.SwaggerEndpoint("Customer/swagger.json", "Заказчики");
                x.SwaggerEndpoint("Jewelry/swagger.json", "Ювелирные изделия");
                x.SwaggerEndpoint("Material/swagger.json", "Материалы");
                x.SwaggerEndpoint("Order/swagger.json", "Заказы");
                x.SwaggerEndpoint("Workshop/swagger.json", "Мастерские");
            });
        }
    }
}
