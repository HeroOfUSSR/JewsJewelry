using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context
{
    public class SampleContext : IDesignTimeDbContextFactory<JewelryContext>
    {
        public JewelryContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<JewelryContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new JewelryContext(options);
        }
        
    }
}
