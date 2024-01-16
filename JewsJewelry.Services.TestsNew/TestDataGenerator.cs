using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Contracts.ModelsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.TestsNew
{
    public static class TestDataGenerator
    {
        static public Craftsman Craftsman(Action<Craftsman>? settings = null)
        {
            var result = new Craftsman
            {
                Name = $"{Guid.NewGuid():N}",
                Surname = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                PhoneNumber = $"{Guid.NewGuid():N}",
                Age = 24
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static public Customer Customer(Action<Customer>? settings = null)
        {
            var result = new Customer
            {
                Name = $"{Guid.NewGuid():N}",
                Surname = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                PhoneNumber = $"{Guid.NewGuid():N}",
                Email = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static public Jewelry Jewelry(Action<Jewelry>? settings = null)
        {
            var result = new Jewelry
            {
                Name = $"{Guid.NewGuid():N}",
                Cost = 25000,
                Weight = 222,
                Description = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static public Material Material(Action<Material>? settings = null)
        {
            var result = new Material
            {
                Name = $"{Guid.NewGuid():N}",
                Color = $"{Guid.NewGuid():N}",
                Sample = 880,
                Amount = 500
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static public Order Order(Action<Order>? settings = null)
        {
            var result = new Order
            {
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                OrderDate = DateTimeOffset.Now,
                DoneDate = DateTimeOffset.Now.AddDays(7)
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static public Workshop Workshop(Action<Workshop>? settings = null)
        {
            var result = new Workshop
            {
                Name = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}",
                Speciality = $"{Guid.NewGuid():N}",
                Workplaces = 55
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static public CraftsmanModel CraftsmanModel(Action<CraftsmanModel>? settings = null)
        {
            var result = new CraftsmanModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Surname = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                PhoneNumber = $"{Guid.NewGuid():N}",
                Age = 24
            };

            settings?.Invoke(result);
            return result;

        }

        static public CustomerModel CustomerModel(Action<CustomerModel>? settings = null)
        {
            var result = new CustomerModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Surname = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                PhoneNumber = $"{Guid.NewGuid():N}",
                Email = "aboba@gmail.com"
            };

            settings?.Invoke(result);
            return result;

        }

        static public JewelryModel JewelryModel(Action<JewelryModel>? settings = null)
        {
            var result = new JewelryModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Cost = 25000,
                Weight = 222,
                Description = $"{Guid.NewGuid():N}"
            };
            
            settings?.Invoke(result);
            return result;

        }

        static public MaterialModel MaterialModel(Action<MaterialModel>? settings = null)
        {
            var result = new MaterialModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Color = $"{Guid.NewGuid():N}",
                Sample = 880,
                Amount = 500
            };
           
            settings?.Invoke(result);
            return result;

        }

        static public OrderRequestModel OrderRequestModel(Action<OrderRequestModel>? settings = null)
        {
            var result = new OrderRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                OrderDate = DateTimeOffset.Now,
                DoneDate = DateTimeOffset.Now.AddDays(7)
            };
            
            settings?.Invoke(result);
            return result;

        }

        static public WorkshopModel WorkshopModel(Action<WorkshopModel>? settings = null)
        {
            var result = new WorkshopModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}",
                Speciality = $"{Guid.NewGuid():N}",
                Workplaces = 55
            };
            
            settings?.Invoke(result);
            return result;

        }
    }
}

