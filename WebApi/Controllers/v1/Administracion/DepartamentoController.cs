using Application.Feautres.Catalogos.CveProductos.Queries.GetCveProductoByEstatus;
using Application.Feautres.Administracion.Departamentos.Queries.GetAllDepartamentos;
using Application.Feautres.Administracion.Departamentos.Queries.GetDepartamentoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Administracion.Departamentos.Commands.CreateDepartamento;
using Application.Feautres.Administracion.Departamentos.Commands.UpdateDepartamento;
using Application.Feautres.Administracion.Departamentos.Commands.DeleteDepartamento;
using Application.Feautres.Administracion.Departamentos.Commands.GetDespartamento;

namespace WebApi.Controllers.v1.Administracion
{
    public class DepartamentoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetDepartamentoByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetDepartamentoCommand { }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateDepartamentoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await Mediator.Send(new GetAllDepartamentosByCompanyQuery { id = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdateDepartamentoCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteDepartamentoCommand { Id = id }));
        }

    }
}
