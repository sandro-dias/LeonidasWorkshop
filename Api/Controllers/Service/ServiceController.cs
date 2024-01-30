using Application.UseCases.Service.CreateService;
using Application.UseCases.Service.CreateService.Input;
using Application.UseCases.Service.DeleteService;
using Application.UseCases.Service.GetServices;
using Domain.Entities;
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
    public class ServiceController(ILogger<ServiceController> logger, ICreateServiceUseCase createServiceUseCase, IGetServicesUseCase getServicesUseCase, IDeleteServiceUseCase deleteServiceUseCase) : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger = logger;
        private readonly ICreateServiceUseCase _createServiceUseCase = createServiceUseCase;
        private readonly IGetServicesUseCase _getServicesUseCase = getServicesUseCase;
        private readonly IDeleteServiceUseCase _deleteServiceUseCase = deleteServiceUseCase;

        [HttpPost]
        [Route("api/create-service/{workShopId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateService([FromRoute, Required] long workShopId, [FromRoute, Required] long customerId, [FromQuery, Required] DateTime date, [FromHeader, Required] ServiceWorkload serviceWorkload)
        {
            try
            {
                var input = new CreateServiceInput(workShopId, customerId, date, serviceWorkload);
                var result = await _createServiceUseCase.ExecuteAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the service. The message returned was: {@Message}", nameof(ServiceController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir serviço no banco de dados.");
            }
        }

        [HttpGet]
        [Route("api/get-services/{workShopId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServices([FromRoute, Required] long workShopId)
        {
            try
            {
                var servicesList = await _getServicesUseCase.ExecuteAsync(workShopId);
                return Ok(servicesList);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to get the services for the day. The message returned was: {@Message}", nameof(ServiceController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar os serviços do dia atual da oficina no banco de dados.");
            }
        }

        [HttpDelete]
        [Route("api/delete-service/{serviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService([FromRoute, Required] long serviceId)
        {
            try
            {
                await _deleteServiceUseCase.ExecuteAsync(serviceId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to delete the service for the day. The message returned was: {@Message}", nameof(ServiceController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar o serviço no banco de dados.");
            }
        }
    }
}
