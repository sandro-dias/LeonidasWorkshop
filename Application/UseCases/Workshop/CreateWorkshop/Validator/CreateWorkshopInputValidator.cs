using Application.UseCases.CreateWorkshop.Input;
using FluentValidation;

namespace Application.UseCases.CreateWorkshop.Validator
{
    public class CreateWorkshopInputValidator : AbstractValidator<CreateWorkshopInput>
    {
        private const int ZeroWorkload = 0;
        private const int CNPJLenght = 14;

        public CreateWorkshopInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Workload)
                .NotEmpty()
                .NotNull()
                .GreaterThan(ZeroWorkload);

            RuleFor(x => x.CNPJ)
                .NotEmpty()
                .NotNull()                
                .Length(CNPJLenght)
                .Custom((x, context) =>
                {
                    if ((!long.TryParse(x, out long value)))
                    {
                        context.AddFailure($"{x} is not a valid number or less than 0");
                    }
                }); ;

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
