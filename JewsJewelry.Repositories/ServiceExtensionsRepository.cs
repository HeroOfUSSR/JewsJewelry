using JewsJewelry.General;
using JewsJewelry.Repositories.Marker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories
{
    public static class ServiceExtensionsRepository
    {
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryMarker>(ServiceLifetime.Scoped);
        }
    }
}
