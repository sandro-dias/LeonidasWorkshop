using Application.Data;
using Application.UseCases.CreateWorkshop.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.CreateWorkshop
{
    public class CreateWorkshopUseCase : ICreateWorkshopUseCase
    {
        private readonly IValidator<CreateWorkshopInput> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateWorkshopUseCase> _logger;

        public CreateWorkshopUseCase(IValidator<CreateWorkshopInput> validator, IUnitOfWork unitOfWork, ILogger<CreateWorkshopUseCase> logger)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Domain.Entities.Workshop> ExecuteAsync(CreateWorkshopInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                _logger.LogError("[{ClassName}] The input returned an error: {Errors}", nameof(CreateWorkshopUseCase), validationResult.Errors);
                return null;
            }

            var workshop = Domain.Entities.Workshop.CreateWorkshop(input.WorkshopName, input.Workload);
            workshop = await _unitOfWork.WorkshopRepository.AddAsync(workshop);
            return workshop;
        }
    }
}
