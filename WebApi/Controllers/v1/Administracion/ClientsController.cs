using Application.Feautres.Administracion.Clientes.Commands.DeleteClienteCommand;
using Application.Feautres.Administracion.Clientes.Queries.GetClienteByCompany;
using Application.Feautres.Administracion.Clientes.Queries.GetClienteById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Administracion.Clientes.Commands.CreateClienteCommand;
using Application.Feautres.Administracion.Clientes.Commands.UpdateClienteCommand;

namespace WebApi.Controllers.v1.Administracion
{
    [ApiVersion("1.0")]

    public class ClientsController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateClientCommand command)
        {
            if (command.Id != id)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {

            return Ok(await Mediator.Send(new DeleteClientCommand { Id = id })); ;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(await Mediator.Send(new GetCompaniaByIdQuery { Id = id })); ;
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByCompanyId(int id)
        {

            return Ok(await Mediator.Send(new GetAllClientByCompanyQuery { Id = id })); ;
        }
    }
}
