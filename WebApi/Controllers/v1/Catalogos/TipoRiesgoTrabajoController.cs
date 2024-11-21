using Application.Feautres.Catalogos.TipoRiesgoTrabajos.Queries.GetAllTipoRiesgoTrabajos;
using Application.Feautres.Catalogos.TipoRiesgoTrabajos.Queries.GetTipoRiesgoTrabajoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoRiesgoTrabajoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoRiesgoTrabajoByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoRiesgoTrabajosQuery { }));
        }


    }
}
