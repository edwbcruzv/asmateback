using Application.Feautres.Kanban.Tickets.Commands.CreateTicket;
using Application.Feautres.Kanban.Tickets.Commands.DeleteTicket;
using Application.Feautres.Kanban.Tickets.Commands.SendTicket;
using Application.Feautres.Kanban.Tickets.Commands.UpdateTicket;
using Application.Feautres.Kanban.Tickets.Queries.GetAllTickets;
using Application.Feautres.Kanban.Tickets.Queries.GetTicketById;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.SendPagoReembolsoCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Kanban.Tickets
{
    [ApiVersion("1.0")]
    public class TicketController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateTicketCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTicketByIdQuery { Id = id }));
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyId(int id)
        {
            return Ok(await Mediator.Send(new GetAllTicketsByCompanyQuery { CompanyId = id }));
        }

        [HttpGet("company/{company_id}/employee-asignado/{employee_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllTicketsByCompanyIdAndEmployeeAsignadoId(int company_id,int employee_id)
        {
            return Ok(await Mediator.Send(new GetAllTicketsByCompanyIdAndEmployeeAsignadoIdQuery {CompanyId = company_id, EmployeeAsignadoId = employee_id }));
        }

        [HttpGet("company/{company_id}/employee-creador/{employee_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllTicketsByCompanyIdAndEmployeeCreadorId(int company_id, int employee_id)
        {
            return Ok(await Mediator.Send(new GetAllTicketsByCompanyIdAndEmployeeCreadorIdQuery { CompanyId = company_id, EmployeeCreadorId = employee_id}));
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult> Patch(int id, UpdateTicketCommand command)
        {
            if (id != command.Id) BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTicketCommand { Id = id }));
        }

        [HttpPost("enviarEmailTickets/{id}")]
        [Authorize]
        public async Task<IActionResult> enviarEmailTickets(int id)
        {
            return Ok(await Mediator.Send(new SendTicketCommand { Id = id }));
        }
    }
}
