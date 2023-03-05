using FluentValidation;

namespace Ordering.Dtos.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.MaterialName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(400).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(x => x.MaterialCode)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.DestinationAddress)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(500).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(x => x.OrderNumber)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.CustomerId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.Quantity)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(x => x.QuantityUnit)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(x => x.Weight)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(x => x.WeightUnit)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
        }
    }
}
