using Application.Feautres.MiPortal.AhorrosVoluntario.Commands.CreateAhorroVoluntarioCommand;
using Application.Feautres.MiPortal.AhorrosVoluntario.Commands.DeleteAhorroVoluntarioCommand;
using Application.Feautres.MiPortal.AhorrosVoluntario.Commands.UpdateAhorroVoluntarioCartaFirmadaCommand;
using Application.Feautres.MiPortal.AhorrosVoluntario.Commands.UpdateAhorroVoluntarioCommand;
using Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetAhorroVoluntarioById;
using Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetAllAhorrosVoluntario;
using Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetOthers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class AhorroVoluntarioController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateAhorroVoluntarioCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetAhorroVoluntarioByIdQuery { Id = id }));
        }

        [HttpGet("employee/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEmployeeId(int id)
        {
            return Ok(await Mediator.Send(new GetAllAhorrosVoluntarioByEmployeeIdQuery { Id = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch([FromForm] UpdateAhorroVoluntarioCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("carta-firmada")]
        [Authorize]
        public async Task<ActionResult> UpdateCartaFirma([FromForm] UpdateAhorroVoluntarioCartaFirmadaCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAhorroVoluntarioCommand { Id = id }));
        }

        [HttpGet("{id}/carta")]
        [Authorize]
        public async Task<IActionResult> GetCartaPDFById(int id)
        {
            return Ok(await Mediator.Send(new GetAhorroVoluntarioCartaPDFByIdQuery { Id = id }));

            //// Verifica si se obtuvo el archivo PDF correctamente
            //if (archivoPDF == null)
            //{
            //    return NotFound(); // Devuelve un error 404 si el archivo no pudo ser generado
            //}

            //// Devuelve el archivo PDF como respuesta
            //return File(archivoPDF, "application/pdf", "acuse.pdf");
        }

        [HttpGet("{id}/estado-cuenta")]
        [Authorize]
        public async Task<IActionResult> GetEstadoCuentaPDFById(int id)
        {
            return Ok(await Mediator.Send(new GetAhorroVoluntarioEstadoCuentaPDFByIdQuery { Id = id }));

        }

        [HttpPost("solicitud-retiro")]
        [Authorize]
        public async Task<IActionResult> GetSolicitudRetiroPDFById(GetAhorroVoluntarioSolicitudRetiroPDFByIdQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("employee/{id}/has-pendiente")]
        [Authorize]
        public async Task<IActionResult> GetIsPendienteByEmployeeId(int id)
        {
            return Ok(await Mediator.Send(new GetAhorroVoluntarioIsPendienteByEmployeeIdQuery { EmployeeId = id }));
        }

        [HttpGet("employee/{id}/deduccion")]
        [Authorize]
        public async Task<IActionResult> GetDeduccionByEmployeeId(int id)
        {
            return Ok(await Mediator.Send(new GetAhorrosVoluntarioDeduccionByEmployeeIdQuery { Id = id }));
        }
    }
}
