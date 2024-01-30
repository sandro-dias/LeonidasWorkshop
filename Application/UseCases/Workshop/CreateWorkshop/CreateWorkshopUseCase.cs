using Application.Data;
using Application.Data.Specification;
using Application.UseCases.CreateWorkshop.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.CreateWorkshop
{
    public class CreateWorkshopUseCase(IValidator<CreateWorkshopInput> validator, IUnitOfWork unitOfWork, ILogger<CreateWorkshopUseCase> logger) : ICreateWorkshopUseCase
    {
        private readonly IValidator<CreateWorkshopInput> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateWorkshopUseCase> _logger = logger;

        public async Task<Domain.Entities.Workshop> ExecuteAsync(CreateWorkshopInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                _logger.LogError("[{ClassName}] The input returned an error: {Errors}", nameof(CreateWorkshopUseCase), validationResult.Errors);
                return default;
            }

            var workshop = await _unitOfWork.WorkshopRepository.FirstOrDefaultAsync(new GetWorkshopByNameSpecification(input.Name));
            if (workshop != null)
            {
                _logger.LogWarning("[{ClassName}] The workshop already exists on database", nameof(CreateWorkshopUseCase));
                return workshop;
            }

            workshop = Domain.Entities.Workshop.CreateWorkshop(input.Name, input.Workload);
            workshop = await _unitOfWork.WorkshopRepository.AddAsync(workshop);
            await _unitOfWork.CommitAsync();
            return workshop;
        }
    }
}
