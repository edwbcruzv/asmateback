using Application.Feautres.MiPortal.Prestamos.Commands.CreatePrestamoCommand;
using Application.Feautres.MiPortal.Prestamos.Commands.DeletePrestamoCommand;
using Application.Feautres.MiPortal.Prestamos.Commands.UpdatePrestamoCommand;
using Application.Feautres.MiPortal.Prestamos.Commands.UpdatePrestamoFiles;
using Application.Feautres.MiPortal.Prestamos.Queries.GetAllPrestamos;
using Application.Feautres.MiPortal.Prestamos.Queries.GetPrestamoById;
using Application.Feautres.MiPortal.Prestamos.Queries.GetPrestamoFiles;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class PrestamoController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreatePrestamoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetPrestamoByIdQuery { Id = id }));
        }

        [HttpGet("{id}/acuse")]
        [Authorize]
        public async Task<IActionResult> GetAcusePDFById(int id)
        {
            return Ok(await Mediator.Send(new GetPrestamoAcusePDFByIdQuery { Id = id }));
        }

        [HttpGet("{id}/pagare")]
        [Authorize]
        public async Task<IActionResult> GetFormatoPDFById(int id)
        {
            return Ok(await Mediator.Send(new GetPrestamoPagarePDFByIdQuery { Id = id }));

        }
        [HttpGet("{id}/estado-cuenta")]
        [Authorize]
        public async Task<IActionResult> GetEstadoCuentaPDFById(int id)
        {
            return Ok(await Mediator.Send(new GetPrestamoEstadoCuentaPDFByIdQuery { Id = id }));

        }

        [HttpGet("employee/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByViaticoId(int id)
        {
            return Ok(await Mediator.Send(new GetAllPrestamosByEmployeeIdQuery { Id = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdatePrestamoCommand command)
        {
            //if (id != command.Id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("fecha-transferencia")]
        [Authorize]
        public async Task<ActionResult> PatchFechaTransefencia(UpdatePrestamoFechaTransferenciaCommand command)
        {
            //if (id != command.Id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("{id}/estatus/{estatus_id}")]
        [Authorize]
        public async Task<ActionResult> UpdateEstatus(int id, int estatus_id)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(new UpdatePrestamoEstatusCommand { Id = id ,Estatus = (EstatusOperacion)estatus_id}));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePrestamoCommand { Id = id }));
        }


        [HttpPatch("acuse-firmado")]
        [Authorize]
        public async Task<ActionResult> UpdateAcuseFirmado([FromForm] UpdatePrestamoAcuseCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("pagare")]
        [Authorize]
        public async Task<ActionResult> UpdatePagare([FromForm] UpdatePrestamoPagareCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("constancia-retiro")]
        [Authorize]
        public async Task<ActionResult> UpdateConstanciaRetiro([FromForm] UpdatePrestamoConstanciaRetiroCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        

        [HttpPatch("constancia-transferencia")]
        [Authorize]
        public async Task<ActionResult> UpdateSolicitudRetiro([FromForm] UpdatePrestamoContanciaTransferenciaCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}
