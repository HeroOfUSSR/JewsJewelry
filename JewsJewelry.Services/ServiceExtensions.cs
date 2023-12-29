using JewsJewelry.General;
using JewsJewelry.Services.Markers;
using JewsJewelry.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensions
    {
        public static void RegistrationService(this IServiceCollection service) 
        {
            service.RegistrationOnInterface<IServiceMarker>(ServiceLifetime.Scoped);
            service.AddTransient<IServiceValidator, ServiceValidator>();
        }
    }
}
