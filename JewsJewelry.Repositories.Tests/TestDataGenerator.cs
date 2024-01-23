using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Tests
{
    internal static class TestDataGenerator
    {
        static internal Craftsman Craftsman(Action<Craftsman>? settings = null) 
        {
            var result = new Craftsman
            {
                Name = $"{Guid.NewGuid():N}",
                Surname = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                PhoneNumber = $"{Guid.NewGuid():N}",
                Age = 31
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static internal Customer Customer(Action<Customer>? settings = null)
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

        static internal Jewelry Jewelry(Action<Jewelry>? settings = null)
        {
            var result = new Jewelry
            {
                //MaterialId = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Cost = 10000,
                Weight = 100,
                Description = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static internal Material Material(Action<Material>? settings = null)
        {
            var result = new Material
            {
                Name = $"{Guid.NewGuid():N}",
                Color = $"{Guid.NewGuid():N}",
                Sample = 100,
                Amount = 1000
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

        static internal Order Order(Action<Order>? settings = null)
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

        static internal Workshop Workshop(Action<Workshop>? settings = null)
        {
            var result = new Workshop
            {
                Name = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}",
                Speciality = $"{Guid.NewGuid():N}",
                Workplaces = 12
            };
            result.BaseAuditParameters();

            settings?.Invoke(result);
            return result;

        }

    }
}
