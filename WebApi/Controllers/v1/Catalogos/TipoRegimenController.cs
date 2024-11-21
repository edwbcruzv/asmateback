using Application.Feautres.Catalogos.TipoRegimens.Queries.GetAllTipoRegimens;
using Application.Feautres.Catalogos.TipoRegimens.Queries.GetTipoRegimenById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class TipoRegimenController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoRegimenByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTipoRegimensQuery { }));
        }


    }
}
