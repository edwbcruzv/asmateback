using Application.Feautres.Catalogos.TipoMonedas.Queries.GetAllTipoMoneda;
using Application.Feautres.Catalogos.TipoMonedas.Queries.GetTipoMonedaById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoMonedaController : BaseApiController
    {
        [HttpGet("TipoMoneda/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoMonedaByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoMonedaQuery { }));
        }


    }
}
