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
    public class CraftsmanWriteRepository : BasedWriteRepository<Craftsman>, ICraftsmanWriteRepository, IRepositoryMarker
    {
        public CraftsmanWriteRepository(IDBWriterContext writerContext)
            : base(writerContext) { }
    }
}
