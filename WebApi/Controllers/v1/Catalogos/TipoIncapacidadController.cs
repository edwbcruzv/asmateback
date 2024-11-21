using Application.Feautres.Catalogos.TipoIncapacidads.Queries.GetAllTipoIncapacidad;
using Application.Feautres.Catalogos.TipoIncapacidads.Queries.GetTipoIncapacidadById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoIncapacidadController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoIncapacidadByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoIncapacidadQuery { }));
        }


    }
}
