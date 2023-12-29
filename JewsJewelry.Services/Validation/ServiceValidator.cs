using FluentValidation;
using JewsJewelry.General;
using JewsJewelry.Repositories.Contracts.Interface;
using JewsJewelry.Services.Contracts.Exceptions;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Contracts.ModelsRequest;
using JewsJewelry.Services.Validation.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Validation
{
    public sealed class ServiceValidator : IServiceValidator
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ServiceValidator(IJewelryReadRepository jewelryReadRepository,
            ICustomerReadRepository customerReadRepository, IWorkshopReadRepository workshopReadRepository)
        {
            validators.Add(typeof(CraftsmanModel), new CraftsmanModelValidator());
            validators.Add(typeof(CustomerModel), new CustomerModelValidator());
            validators.Add(typeof(JewelryModel), new JewelryModelValidator());
            validators.Add(typeof(MaterialModel), new MaterialModelValidator());
            validators.Add(typeof(WorkshopModel), new WorkshopModelValidator());
            validators.Add(typeof(OrderRequestModel), new OrderRequestValidator(jewelryReadRepository,
                customerReadRepository, workshopReadRepository));
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new ValidsException(result.Errors.Select(bruh =>
                InvalidateItemModel.New(bruh.PropertyName, bruh.ErrorMessage)));
            }
        }
    }
}

