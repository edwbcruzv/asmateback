using Application.Feautres.Catalogos.ObjetoImpuestos.Queries.GetAllObjetoImpuesto;
using Application.Feautres.Catalogos.ObjetoImpuestos.Queries.GetObjetoImpuestoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class ObjetoImpuestoController : BaseApiController
    {
        [HttpGet("ObjetoImpuesto/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetObjetoImpuestoByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllObjetoImpuestoQuery { }));
        }


    }
}
