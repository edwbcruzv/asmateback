using Application.Feautres.Nif;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers.v1.Nif
{
    [ApiVersion("1.0")]
    public class NifController : BaseApiController
    {
        [HttpGet("CrearExcelBase")]
        [Authorize]
        public async Task<ActionResult> CrearExcelBase()
        {
            return Ok(await Mediator.Send(new CreaArchivoBaseCommand { }));
        }
        [HttpPost("CalculoNif")]
        [Authorize]
        public async Task <ActionResult> CalculoNif([FromForm] GeneraNifCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
