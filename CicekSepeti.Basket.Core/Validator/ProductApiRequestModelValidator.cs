using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Core.Validator
{
    public class ProductApiRequestModelValidator : AbstractValidator<ProductApiRequestModel>
    {
        public ProductApiRequestModelValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage(ValidationMessages.NotEmpty("Name"));

            RuleFor(s => s.ProductId).NotEmpty().WithMessage(ValidationMessages.NotEmpty("ProductId"));

            RuleFor(s => s.Quantity).NotEmpty().WithMessage(ValidationMessages.NotEmpty("Quantity"));

            RuleFor(s => s.ProductId).NotEmpty().GreaterThan(0).WithMessage(ValidationMessages.MustBeGreater("ProductId", 0));

            RuleFor(s => s.Quantity).NotEmpty().GreaterThan(0).WithMessage(ValidationMessages.MustBeGreater("Quantity", 0));
        }
    }
}
