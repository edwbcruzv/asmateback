using Application.Feautres.Catalogos.CveProductos.Queries.GetCveProductoByEstatus;
using Application.Feautres.Administracion.Puestos.Queries.GetAllPuestos;
using Application.Feautres.Administracion.Puestos.Queries.GetPuestoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Administracion.Puestos.Commands.DeletePuesto;
using Application.Feautres.Administracion.Puestos.Commands.CreatePuesto;
using Application.Feautres.Administracion.Puestos.Commands.UpdatePuesto;

namespace WebApi.Controllers.v1.Administracion
{
    public class PuestoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetPuestoByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post( CreatePuestoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("departamento/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByDepartamento(int id)
        {
            return Ok(await Mediator.Send(new GetAllPuestosByDepartamentoQuery { id = id }));
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompany(int id)
        {
            return Ok(await Mediator.Send(new GetAllPuestosByCompanyQuery { CompanyId = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdatePuestoCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePuestoCommand { Id = id }));
        }




    }
}
