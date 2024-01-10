using Application.Database.Repository;
using Application.UseCases.PostWorkshop.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.UseCases.PostWorkshop
{
    public class PostWorkshopUseCase : IPostWorkshopUseCase
    {
        private readonly IValidator<PostWorkshopInput> _validator;
        private readonly IWorkshopRepository _workshopRepository;
        private readonly ILogger<PostWorkshopUseCase> _logger;

        public PostWorkshopUseCase(IValidator<PostWorkshopInput> validator, IWorkshopRepository productRepository, ILogger<PostWorkshopUseCase> logger)
        {
            _validator = validator;
            _workshopRepository = productRepository;
            _logger = logger;
        }

        public async Task<Workshop> ExecuteAsync(PostWorkshopInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                _logger.LogError("[{ClassName}] The input returned an error: {Errors}", nameof(PostWorkshopUseCase), validationResult.Errors);
                return null;
            }

            var workshop = Workshop.CreateWorkshop(input.WorkshopName, input.Workload);
            return await _workshopRepository.InsertWorkshop(workshop);
        }
    }
}
