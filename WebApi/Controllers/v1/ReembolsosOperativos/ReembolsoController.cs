using Application.Feautres.Administracion.Employees.Commands.CreateEmployeeCommand;
using Application.Feautres.Catalogos.Bancos.Queries.GetBancoById;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.DeleteComplementoPagoFacturaCommand;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.UpdateComplementoPagoFacturaCommand;
using Application.Feautres.Kanban.Tickets.Queries.GetTicketById;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.CreateReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.DeleteReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.PagarReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.SendPagoReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.SendReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.UpdateReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Others;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetAllReembolsos;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetReembolsoByUsername;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetReembolsosById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers.v1.ReembolsosOperativos
{
    public class ReembolsoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetReembolsoByIdQuery { Id = id }));
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompany(int id)
        {
            return Ok(await Mediator.Send(new GetAllReembolsosByCompanyQuery { CompanyId = id }));
        }

        [HttpGet("userid/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByUserId(int id)
        {
            return Ok(await Mediator.Send(new GetAllReembolsosByUsernameQuery { UserId = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateReembolsoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, UpdateReembolsoCommand command)
        {
            if (id != command.Id) BadRequest();

            return Ok(await Mediator.Send(command));
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteReembolsoCommand { Id = id }));
        }

        [HttpGet("GeneraExcelMovimientosReembolso/{id}")]
        [Authorize]
        public async Task<ActionResult> generaExcelMovimientosReembolso(int id)
        {
            return Ok(await Mediator.Send(new ExcelMovimientoReembolsoCommand { Id = id }));
        }


        [HttpGet("DescargaMasivaReembolsos")]
        [Authorize]
        public async Task<ActionResult> DescargaMasivaReembolsos(DescargaMasivaReembolsosCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("ObtenerTotalesReembolso/{id}")]
        [Authorize]
        public async Task<ActionResult> ObtenerTotalesReembolso(int id)
        {
            return Ok(await Mediator.Send(new ObtenerTotalesReembolsoCommand { ReembolsoId = id }));
        }


        [HttpPost("enviarNotificacionReembolso/{id}")]
        [Authorize]
        public async Task<ActionResult> sendMailReembolsos(int id)
        {
            return Ok(await Mediator.Send(new SendReembolsoCommand { Id = id }));
        }

        [HttpPost("enviarPagoReembolso/{id}")]
        [Authorize]
        public async Task<ActionResult> enviarPagoReembolso(int id)
        {
            return Ok(await Mediator.Send(new SendPagoReembolsoCommand { Id = id }));
        }


        [HttpPut("PagarReembolso/{id}")]
        [Authorize]
        
        public async Task<ActionResult> pagarReembolso([FromForm] PagarReembolsoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
