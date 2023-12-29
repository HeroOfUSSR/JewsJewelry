using FluentValidation;
using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Validation.Validators
{
    public class MaterialModelValidator : AbstractValidator<MaterialModel>
    {
        public MaterialModelValidator()
        {
            RuleFor(bruh => bruh.Name)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 50).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Color)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 50).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Sample)
                 .InclusiveBetween(0, 1000)
                 .WithMessage(ValidatorMessage.InclusiveBetweenMessage);

            RuleFor(bruh => bruh.Amount)
                .InclusiveBetween(0, 100)
                .WithMessage(ValidatorMessage.InclusiveBetweenMessage);
        }
    }
}
