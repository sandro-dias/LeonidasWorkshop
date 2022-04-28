﻿using Application.Database.Entities;
using Application.Database.Repository;
using Application.UseCases.PostWorkshop.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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

        public async Task ExecuteAsync(PostWorkshopInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                _logger.LogError("A validação do input retornou falha com o seguinte erro: {Errors}", validationResult.Errors);
                return;
            }

            var workshop = new Workshop(input.Id, input.CNPJ, input.Workload, input.Password);
            await _workshopRepository.InsertWorkshop(workshop);
            _logger.LogInformation("A oficina de Id {id} foi inserida com sucesso", input.Id);
        }
    }
}
