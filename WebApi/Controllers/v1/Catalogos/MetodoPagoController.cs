
using Application.Feautres.Catalogos.MetodoPagos.Queries.GetAllMetodoPago;
using Application.Feautres.Catalogos.MetodoPagos.Queries.GetMetodoPagoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class MetodoPagoController : BaseApiController
    {
        [HttpGet("MetodoPago/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetMetodoPagoByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllMetodoPagoQuery { }));
        }


    }
}
