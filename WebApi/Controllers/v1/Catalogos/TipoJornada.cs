using Application.Feautres.Catalogos.TipoJornadas.Queries.GetAllTipoJornada;
using Application.Feautres.Catalogos.TipoJornadas.Queries.GetTipoJornadaById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoJornadaController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoJornadaByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoJornadaQuery { }));
        }


    }
}
