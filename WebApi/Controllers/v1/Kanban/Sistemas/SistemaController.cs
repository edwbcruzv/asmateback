
using Application.Feautres.Kanban.Sistemas.Commands.CreateSistema;
using Application.Feautres.Kanban.Sistemas.Commands.DeleteSistema;
using Application.Feautres.Kanban.Sistemas.Commands.GetSistema;
using Application.Feautres.Kanban.Sistemas.Commands.UpdateSistema;
using Application.Feautres.Kanban.Sistemas.Queries.GetAllSistemas;
using Application.Feautres.Kanban.Sistemas.Queries.GetSistemaById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Kanban.Sistemas
{
    [ApiVersion("1.0")]
    public class SistemaController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post( CreateSistemaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetSistemaCommand { }));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetSistemaByIdQuery { Id = id }));
        }

        [HttpGet("estado/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEstadoId(int id)
        {
            return Ok(await Mediator.Send(new GetAllSistemasByEstadoIdQuery { EstadoId = id }));
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult> Patch(int id, UpdateSistemaCommand command)
        {
            if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSistemaCommand { Id = id }));
        }


    }
}
