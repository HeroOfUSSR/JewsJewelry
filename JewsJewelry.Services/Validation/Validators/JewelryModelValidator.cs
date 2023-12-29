using FluentValidation;
using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Validation.Validators
{
    public class JewelryModelValidator : AbstractValidator<JewelryModel>
    {
        public JewelryModelValidator()
        {
            RuleFor(bruh => bruh.Name)
                .NotEmpty().WithMessage(ValidatorMessage.DefaultMessage)
                .NotNull().WithMessage(ValidatorMessage.DefaultMessage)
                .Length(2, 100).WithMessage(ValidatorMessage.LengthMessage);

            RuleFor(bruh => bruh.Cost)
                .InclusiveBetween(500, 5000000)
                .WithMessage(ValidatorMessage.InclusiveBetweenMessage);

            RuleFor(bruh => bruh.Weight)
                .InclusiveBetween(2, 2000)
                .WithMessage(ValidatorMessage.InclusiveBetweenMessage);
        }
    }
}
