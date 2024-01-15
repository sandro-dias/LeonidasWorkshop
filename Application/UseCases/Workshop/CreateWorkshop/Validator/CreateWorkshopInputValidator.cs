using Application.UseCases.CreateWorkshop.Input;
using FluentValidation;

namespace Application.UseCases.CreateWorkshop.Validator
{
    public class CreateWorkshopInputValidator : AbstractValidator<CreateWorkshopInput>
    {
        private const int ZeroWorkload = 0;
        public CreateWorkshopInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Workload)
                .NotEmpty()
                .NotNull()
                .GreaterThan(ZeroWorkload);
        }
    }
}
