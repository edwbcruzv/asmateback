using Application.Feautres.Catalogos.UsoCfdis.Queries.GetAllUsoCfdi;
using Application.Feautres.Catalogos.UsoCfdis.Queries.GetUsoCfdiById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class UsoCfdiController : BaseApiController
    {
        [HttpGet("UsoCfdi/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetUsoCfdiByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllUsoCfdiQuery { }));
        }


    }
}
