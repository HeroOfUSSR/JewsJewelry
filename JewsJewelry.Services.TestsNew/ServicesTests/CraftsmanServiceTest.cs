using AutoMapper;
using JewsJewelry.Context.Tests;
using JewsJewelry.Repositories.Implementations;
using JewsJewelry.Repositories.WriteRepository;
using JewsJewelry.Services.Automappers;
using JewsJewelry.Services.Contracts.Interface;
using JewsJewelry.Services.Implementations;
using JewsJewelry.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.TestsNew.ServicesTests
{
    public class CraftsmanServiceTest : JewsJewelryContextInMemory
    {
        private readonly ICraftsmanService craftsmanService;
        private readonly CraftsmanReadRepository craftsmanReadRepository;

        public CraftsmanServiceTest()
        {
            var config = new MapperConfiguration(c => { c.AddProfile(new ServiceProfile()); 
            });

            craftsmanReadRepository = new CraftsmanReadRepository(Reader);

            craftsmanService = new CraftsmanService(craftsmanReadRepository, config.CreateMapper(),
                new CraftsmanWriteRepository(WriterContext), 
                UnitOfWork,
                new ServiceValidator(craftsmanReadRepository, new JewelryReadRepository(Reader),
                new CustomerReadRepository(Reader), new WorkshopReadRepository(Reader)));
        }

        
    }
}
