using Application.Feautres.Catalogos.FormaPagos.Queries.GetAllFormaPago;
using Application.Feautres.Catalogos.FormaPagos.Queries.GetFormaPagoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class FormaPagoController : BaseApiController
    {
        [HttpGet("formaPago/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetFormaPagoByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllFormaPagoQuery { }));
        }


    }
}
