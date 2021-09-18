using AWSSQSAPI.Models;
using AWSSQSAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AWSSQSAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AWSSQSController : ControllerBase
    {
        private readonly IAWSSQSService _aWSSQSService;

        public AWSSQSController(IAWSSQSService aWSSQSService)
        {
            _aWSSQSService = aWSSQSService;
        }

        [Route("postMenssagem")]
        [HttpPost]
        public async Task<IActionResult> PostMessageAsync([FromBody] Usuario usuario)
        {
            var result = await _aWSSQSService.PostMessageAsync(usuario);
            return Ok(new { EhSucesso = result });
        }

        [Route("recuperarMensagens")]
        [HttpGet]
        public async Task<IActionResult> RecuperarMensagensAsync()
        {
            return Ok(await _aWSSQSService.RecuperarMensagensAsync());
        }

        [Route("deletarMensagens")]
        [HttpDelete]
        public async Task<IActionResult> DeletarMensagensAsync(DeletarMensagem deletarMensagem)
        {
            var result = await _aWSSQSService.DeletarMensagensAsync(deletarMensagem);
            return Ok(new { EhSucesso = result });
        }
    }
}