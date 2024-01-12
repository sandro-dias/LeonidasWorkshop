using Application.Data;
using Application.UseCases.PostWorkshop.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.PostWorkshop
{
    public class PostWorkshopUseCase : IPostWorkshopUseCase
    {
        private readonly IValidator<PostWorkshopInput> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostWorkshopUseCase> _logger;

        public PostWorkshopUseCase(IValidator<PostWorkshopInput> validator, IUnitOfWork unitOfWork, ILogger<PostWorkshopUseCase> logger)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Domain.Entities.Workshop> ExecuteAsync(PostWorkshopInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                _logger.LogError("[{ClassName}] The input returned an error: {Errors}", nameof(PostWorkshopUseCase), validationResult.Errors);
                return null;
            }

            var workshop = Domain.Entities.Workshop.CreateWorkshop(input.WorkshopName, input.Workload);
            await _unitOfWork.WorkshopRepository.AddAsync(workshop);
            return workshop;
        }
    }
}
