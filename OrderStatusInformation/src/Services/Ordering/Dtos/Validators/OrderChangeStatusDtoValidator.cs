using FluentValidation;

namespace Ordering.Dtos.Validators
{
    public class OrderChangeStatusDtoValidator : AbstractValidator<OrderChangeStatusDto>
    {
        public OrderChangeStatusDtoValidator()
        {
            RuleFor(x => x.OrderNumber)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.CustomerId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.CustomerId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();
        }
    }
}
