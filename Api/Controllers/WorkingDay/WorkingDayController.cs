using Application.UseCases.Workshop.CreateWorkingDay;
using Application.UseCases.Workshop.CreateWorkingDay.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers.WorkingDay
{
    [ApiController]
    [Route("v1")]
    public class WorkingDayController : ControllerBase
    {
        private readonly ILogger<WorkingDayController> _logger;
        private readonly ICreateWorkingDayUseCase _createWorkingDayUseCase;

        public WorkingDayController(ILogger<WorkingDayController> logger, ICreateWorkingDayUseCase createWorkingDayUseCase)
        {
            _logger = logger;
            _createWorkingDayUseCase = createWorkingDayUseCase;
        }

        [HttpPost]
        [Route("api/create-working-day/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWorkingDay([FromBody, Required] CreateWorkingDayInput input)
        {
            try
            {
                var result = await _createWorkingDayUseCase.ExecuteAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the working day. The message returned was: {@Message}", nameof(WorkingDayController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir dia de trabalho no banco de dados.");
            }
        }
    }
}
