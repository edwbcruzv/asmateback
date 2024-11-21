using Application.Feautres.Catalogos.TipoPeriocidadPagos.Queries.GetAllTipoPeriocidadPago;
using Application.Feautres.Catalogos.TipoPeriocidadPagos.Queries.GetTipoPeriocidadPagoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoPeriocidadPagoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoPeriocidadPagoByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoPeriocidadPagoQuery { }));
        }


    }
}
