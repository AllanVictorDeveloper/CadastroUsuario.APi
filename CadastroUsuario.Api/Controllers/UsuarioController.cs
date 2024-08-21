using CadastroUsuario.Api.Controllers.Shared;
using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.DTOs.Response;
using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace CadastroUsuario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private IIdentityService _identityService;

        public UsuarioController(ILogger<UsuarioController> logger, IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioCadastroResponse>> Login(UsuarioLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var resultado = await _identityService.Login(request);
            if (resultado.Sucesso)
                return Ok(resultado);

            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.Unauthorized, Request, errors: resultado.Erros);
                return Unauthorized(problemDetails);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);


        }

        [HttpPost("cadastrar")]
        [Authorize(Roles = Roles.Administrador)]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Cadastrar(UsuarioCadastroRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.CadastrarUsuario(request);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return BadRequest(problemDetails);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }



        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _identityService.Logout();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, "Error do sistema, tente novamente mais tarde.");
            }
        }
    }
}