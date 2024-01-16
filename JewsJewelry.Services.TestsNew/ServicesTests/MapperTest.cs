using AutoMapper;
using JewsJewelry.Services.Automappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewsJewelry.Services.TestsNew.ServicesTests
{
    public class MapperTest
    {
        /// <summary>
        /// Тест конфигурации маппера
        /// </summary>
        [Fact]
        public void MapperConfigIsValid()
        {
            var config = new MapperConfiguration(c => c.AddProfile<ServiceProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
