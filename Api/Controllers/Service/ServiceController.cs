using Application.UseCases.Service.CreateService;
using Application.UseCases.Service.CreateService.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers.Service
{
    [ApiController]
    [Route("v1")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly ICreateServiceUseCase _createServiceUseCase;

        public ServiceController(ILogger<ServiceController> logger, ICreateServiceUseCase createServiceUseCase)
        {
            _logger = logger;
            _createServiceUseCase = createServiceUseCase;
        }

        [HttpPost]
        [Route("api/create-service")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateService([Required][FromBody] CreateServiceInput input)
        {
            try
            {
                var result = await _createServiceUseCase.ExecuteAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the service. The message returned was: {@Message}", nameof(ServiceController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir serviço no banco de dados.");
            }
        }
    }
}
