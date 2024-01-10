using Application.UseCases.PostWorkshop.Input;
using FluentValidation;

namespace Application.UseCases.PostWorkshop.Validator
{
    public class PostWorkshopInputValidator : AbstractValidator<PostWorkshopInput>
    {
        private const int ZeroWorkload = 0;
        public PostWorkshopInputValidator()
        {
            RuleFor(x => x.WorkshopName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Workload)
                .NotEmpty()
                .NotNull()
                .GreaterThan(ZeroWorkload);
        }
    }
}
