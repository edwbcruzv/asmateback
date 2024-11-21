
using Application.Feautres.Catalogos.TipoImpuestos.Queries.GetTipoImpuestoById;
using Application.Feautres.Catalogos.TipoImpuestos.Queries.GetAllTipoImpuestos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoImpuestoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoImpuestoByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoImpuestosQuery { }));
        }


    }
}
