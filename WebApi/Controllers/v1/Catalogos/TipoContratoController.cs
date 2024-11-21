using Application.Feautres.Catalogos.TipoContratos.Queries.GetAllTipoContrato;
using Application.Feautres.Catalogos.TipoContratos.Queries.GetTipoContratoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoContratoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoContratoByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoContratoQuery { }));
        }


    }
}
