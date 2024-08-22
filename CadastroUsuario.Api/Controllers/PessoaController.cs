using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace CadastroUsuario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        private readonly IAppPessoa _appPessoa;
        private readonly ILogger<PessoaController> _logger;

        public PessoaController(IAppPessoa appPessoa, ILogger<PessoaController> logger)
        {
            _appPessoa = appPessoa;
            _logger = logger;
        }

        [Authorize]
        [EnableCors]
        [HttpPost("Cadastrar")]
        [DisableRequestSizeLimit]
        public ActionResult Cadastrar([FromBody] PessoaRequest request)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            try
            {
                _appPessoa.Cadastrar(request);


                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }
    }
}
