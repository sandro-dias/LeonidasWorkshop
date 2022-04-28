using Application.UseCases.PostWorkshop;
using Application.UseCases.PostWorkshop.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.PostWorkshop
{
    [ApiController]
    [Route("v1")]
    public class PostWorkshopController : ControllerBase
    {
        private readonly ILogger<PostWorkshopController> _logger;
        private readonly IPostWorkshopUseCase _postWorkshopUseCase;

        public PostWorkshopController(ILogger<PostWorkshopController> logger, IPostWorkshopUseCase postWorkshopUseCase)
        {
            _logger = logger;
            _postWorkshopUseCase = postWorkshopUseCase;
        }

        [HttpPost]
        [Route("api/post-workshop/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostWorkshop([FromBody] PostWorkshopInput input)
        {
            try
            {
                await _postWorkshopUseCase.ExecuteAsync(input);
                return Ok(input);
            }
            catch (Exception ex)
            {
                _logger.LogError("Não foi adicionar a oficina no banco de dados. A seguinte messagem foi retornada: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir oficina no banco de dados.");
            }
        }

        [HttpGet]
        [Route("api/get-workshop/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorkshop([FromBody] PostWorkshopInput input)
        {
            try
            {
                await _postWorkshopUseCase.ExecuteAsync(input);
                return Ok(input);
            }
            catch (Exception ex)
            {
                _logger.LogError("Não foi adicionar a oficina no banco de dados. A seguinte messagem foi retornada: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir oficina no banco de dados.");
            }
        }
    }
}
