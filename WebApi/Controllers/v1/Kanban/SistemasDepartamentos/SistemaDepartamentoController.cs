using Application.Feautres.Kanban.SistemasDepartamentos.Commands.Create;
using Application.Feautres.Kanban.SistemasDepartamentos.Commands.Delete;
using Application.Feautres.Kanban.SistemasDepartamentos.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Kanban.SistemasDepartamentos
{
    [ApiVersion("1.0")]
    public class SistemaDepartamentoController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateSistemaDepartamentoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        

        [HttpGet("departamento/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByDepartamentoId(int id)
        {
            return Ok(await Mediator.Send(new GetAllSistemasDepartamentosByDepartamentoIdQuery { DepartamentoId = id }));
        }

        [HttpGet("sistema/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllBySistemaId(int id)
        {
            return Ok(await Mediator.Send(new GetAllSistemasDepartamentosBySistemaIdQuery { SistemaId = id }));
        }



        [HttpDelete("sistema/{sistema_id}/departamento/{departamento_id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int sistema_id, int departamento_id)
        {
            return Ok(await Mediator.Send(new DeleteSistemaDepartamentoCommand { SistemaId = sistema_id, DepartamentoId = departamento_id }));
        }


    }
}
