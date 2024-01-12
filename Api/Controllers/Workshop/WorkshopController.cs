using Application.UseCases.CreateWorkshop;
using Application.UseCases.CreateWorkshop.Input;
using Application.UseCases.GetWorkshopWorkload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers.PostWorkshop
{
    [ApiController]
    [Route("v1")]
    public class WorkshopController : ControllerBase
    {
        private readonly ILogger<WorkshopController> _logger;
        private readonly ICreateWorkshopUseCase _postWorkshopUseCase;
        private readonly IGetWorkshopWorkloadUseCase _getWorkshopWorkload;

        public WorkshopController(ILogger<WorkshopController> logger, ICreateWorkshopUseCase postWorkshopUseCase, IGetWorkshopWorkloadUseCase getWorkshopWorkload)
        {
            _logger = logger;
            _postWorkshopUseCase = postWorkshopUseCase;
            _getWorkshopWorkload = getWorkshopWorkload;
        }

        [HttpPost]
        [Route("api/create-workshop/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostWorkshop([FromBody, Required] CreateWorkshopInput input)
        {
            try
            {
                await _postWorkshopUseCase.ExecuteAsync(input);
                return Ok(input);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the workshop. The message returned was: {@Message}", nameof(WorkshopController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir oficina no banco de dados.");
            }
        }

        [HttpGet]
        [Route("api/get-workshop-workload/{workShopId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorkshopWorkload([FromRoute, Required] int workShopId)
        {
            try
            {
                var workloadList = await _getWorkshopWorkload.ExecuteAsync(workShopId);
                return Ok(workloadList);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to get the workshop workload. The message returned was: {@Message}", nameof(WorkshopController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar a carga disponível dos próximos dias da oficina no banco de dados.");
            }
        }
    }
}
