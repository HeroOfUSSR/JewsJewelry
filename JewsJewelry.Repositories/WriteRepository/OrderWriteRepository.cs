using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Repositories.Contracts.WriteReposInterface;
using JewsJewelry.Repositories.Marker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.WriteRepository
{
    public class OrderWriteRepository : BasedWriteRepository<Order>, IOrderWriteRepository, IRepositoryMarker
    {
        public OrderWriteRepository(IDBWriterContext writerContext)
            : base(writerContext) { }
    }
}
