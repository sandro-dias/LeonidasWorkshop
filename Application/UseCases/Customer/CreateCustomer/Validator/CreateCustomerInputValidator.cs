using Application.UseCases.Customer.CreateCustomer.Input;
using FluentValidation;

namespace Application.UseCases.Customer.CreateCustomer.Validator
{
    public class CreateCustomerInputValidator : AbstractValidator<CreateCustomerInput>
    {
        private const int CPFLenght = 11;
        public CreateCustomerInputValidator()
        {
            RuleFor(x => x.CPF)
                .NotEmpty()
                .NotNull()
                .Length(CPFLenght);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
