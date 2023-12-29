using FluentValidation;
using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Validation.Validators
{
    public class WorkshopModelValidator : AbstractValidator<WorkshopModel>
    {
        public WorkshopModelValidator()
        {
            RuleFor(bruh => bruh.Name)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 50).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Address)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(5, 100).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Speciality)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 200).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Workplaces)
                .InclusiveBetween(2, 100)
                .WithMessage(ValidatorMessage.InclusiveBetweenMessage);
        }
    }
}
