using Application.UseCases.PostWorkshop.Input;
using FluentValidation;

namespace Application.UseCases.PostWorkshop.Validator
{
    public class PostWorkshopInputValidator : AbstractValidator<PostWorkshopInput>
    {
        public PostWorkshopInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
