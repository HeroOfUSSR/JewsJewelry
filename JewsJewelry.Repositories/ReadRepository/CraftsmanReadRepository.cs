using JewsJewelry.Context.Contracts;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Repositories.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Implementations
{
    public class CraftsmanReadRepository : ICraftsmanReadRepository
    {

        private IJewelryContext context;

        public CraftsmanReadRepository(IJewelryContext context) 
        { 
            this.context = context;
        }

        Task<List<Craftsman>>
    }
}
