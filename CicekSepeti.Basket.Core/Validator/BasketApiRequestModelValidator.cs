using CicekSepeti.BasketCore;
using FluentValidation;

namespace CicekSepeti.Basket.Core.Validator
{
    public class BasketApiRequestModelValidator : AbstractValidator<BasketApiRequestModel>
    {
        public BasketApiRequestModelValidator()
        {
            RuleFor(s => s.ProductId).NotEmpty().WithMessage(ValidationMessages.NotEmpty("ProductId"));

            RuleFor(s => s.Quantity).NotEmpty().WithMessage(ValidationMessages.NotEmpty("Quantity"));

            RuleFor(s => s.ProductId).NotEmpty().GreaterThan(0).WithMessage(ValidationMessages.MustBeGreater("ProductId", 0));

            RuleFor(s => s.Quantity).NotEmpty().GreaterThan(0).WithMessage(ValidationMessages.MustBeGreater("Quantity", 0));
        }
    }
}
