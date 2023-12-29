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

                var filePath = Path.Combine(AppContext.BaseDirectory, "TicketSelling.API.xml");
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
                x.SwaggerEndpoint("Cinema/swagger.json", "Кинотеатры");
                x.SwaggerEndpoint("Client/swagger.json", "Клиенты");
                x.SwaggerEndpoint("Film/swagger.json", "Фильмы");
                x.SwaggerEndpoint("Hall/swagger.json", "Залы");
                x.SwaggerEndpoint("Staff/swagger.json", "Работники");
                x.SwaggerEndpoint("Ticket/swagger.json", "Билеты");
            });
        }
    }
}
