using FluentValidation;
using JewsJewelry.Repositories.Contracts.Interface;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Contracts.ModelsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Validation.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequestModel>
    {
        private readonly IJewelryReadRepository jewelryReadRepository;
        private readonly ICustomerReadRepository customerReadRepository;
        private readonly IWorkshopReadRepository workshopReadRepository;

        public OrderRequestValidator(IJewelryReadRepository jewelryReadRepository,
            ICustomerReadRepository customerReadRepository, IWorkshopReadRepository workshopReadRepository)
        {

            this.jewelryReadRepository = jewelryReadRepository;
            this.customerReadRepository = customerReadRepository;
            this.workshopReadRepository = workshopReadRepository;

            RuleFor(bruh => bruh.JewelryId)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .MustAsync(async (bruh, cancellationToken) => await this.jewelryReadRepository.IsNotNullAsync(bruh, cancellationToken))
                .WithMessage(ValidatorMessage.NotFoundGuidMessage);

            RuleFor(bruh => bruh.CustomerId)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .MustAsync(async (bruh, cancellationToken) => await this.customerReadRepository.IsNotNullAsync(bruh, cancellationToken))
                .WithMessage(ValidatorMessage.NotFoundGuidMessage);

            RuleFor(bruh => bruh.WorkshopId)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .MustAsync(async (bruh, cancellationToken) => await this.workshopReadRepository.IsNotNullAsync(bruh, cancellationToken))
                .WithMessage(ValidatorMessage.NotFoundGuidMessage);

            RuleFor(bruh => bruh.Name)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 100).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.DoneDate)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .GreaterThan(DateTimeOffset.Now.AddMinutes(1)).WithMessage(ValidatorMessage.InclusiveBetweenMessage);

            RuleFor(bruh => bruh.OrderDate)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .LessThan(DateTimeOffset.Now.AddMinutes(5)).WithMessage(ValidatorMessage.InclusiveBetweenMessage);

        }
    }
}
