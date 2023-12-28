using JewsJewelry.Context.Contracts;
using JewsJewelry.Context.Contracts.Models;

namespace JewsJewelry.Context
{
    public class JewelryContext : IJewelryContext
    {
        private readonly List<Craftsman> craftsmen;
        private readonly List<Customer> customers;
        private readonly List<Jewelries> jewelries;
        private readonly List<Materials> materials;
        private readonly List<Orders> orders;
        private readonly List<Workshop> workshops;
       

        public JewerlyContext()
        {
            craftsmen = new List<Craftsman> 
            {
                new Craftsman
                {

                }
            };
        }

    }
}