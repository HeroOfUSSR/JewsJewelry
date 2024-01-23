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
    public class CraftsmanModelValidator : AbstractValidator<CraftsmanModel>
    {

        public CraftsmanModelValidator()
        {
            RuleFor(bruh => bruh.Name)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 50).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Surname)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(5, 100).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Patronymic)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 100).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.PhoneNumber)
               .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
               .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
               .Length(9, 12).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Age)
               .InclusiveBetween(10, 150).WithMessage(ValidatorMessage.InclusiveBetweenMessage);

        }
    }
}
