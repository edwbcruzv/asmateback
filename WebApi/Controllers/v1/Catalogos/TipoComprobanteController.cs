using Application.Feautres.Catalogos.TipoComprobantes.Queries.GetTipoComprobanteById;
using Application.Feautres.Catalogos.TipoComprobantes.Queries.GetAllTipoComprobante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoComprobanteController : BaseApiController
    {
        [HttpGet("TipoComprobante/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoComprobanteByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoComprobanteQuery { }));
        }


    }
}
