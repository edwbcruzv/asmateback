using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.CreateRetiroAhorroVoluntario;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.DeleteRetiroAhorroVoluntario;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.UpdateRetiroAhorroVoluntario;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.UpdateRetiroAhorroVoluntarioFiles;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetAllRetirosAhorroVoluntario;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetRetiroAhorroVoluntarioByEmployeeId;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetRetiroAhorroVoluntarioById;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetRetiroAhorroVoluntarioFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class RetiroAhorroVoluntarioController : BaseApiController
    {

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateRetiroAhorroVoluntarioCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("ahorro-voluntario/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByAhorroId(int id)
        {
            return Ok(await Mediator.Send(new GetAllRetirosAhorroVoluntatioByAhorroVoluntarioIdQuery { Id = id }));
        }

        [HttpGet("{retiro_id}/ahorro/{ahorro_id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int retiro_id, int ahorro_id)
        {
            return Ok(await Mediator.Send(new GetRetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdQuery { Id = retiro_id, AhorroVoluntarioId = ahorro_id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdateRetiroAhorroVoluntarioCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{retiro_id}/ahorro/{ahorro_id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int retiro_id, int ahorro_id)
        {
            return Ok(await Mediator.Send(new DeleteRetiroAhorroVoluntarioCommand { Id = retiro_id, AhorroVoluntarioId = ahorro_id }));
        }

        [HttpGet("{retiro_id}/ahorro/{ahorro_id}/solicitud-retiro")]
        [Authorize]
        public async Task<IActionResult> GetSolicitudRetiroPDFById(int retiro_id, int ahorro_id)
        {
            return Ok(await Mediator.Send(new GetRetiroAhorroVoluntarioSolicitudPDFByIdAndAhorroVoluntarioIdQuery { Id = retiro_id, AhorroVoluntarioId = ahorro_id }));
        }

        [HttpGet("{retiro_id}/ahorro/{ahorro_id}/constancia-transferencia")]
        [Authorize]
        public async Task<IActionResult> GetConstanciaTransferenciaPDFById(int retiro_id, int ahorro_id)
        {
            return Ok(await Mediator.Send(new GetRetiroAhorroVoluntarioConstanciaTransferenciaPDFByIdAndAhorroVoluntarioIdQuery { Id = retiro_id, AhorroVoluntarioId = ahorro_id }));
        }

        [HttpPatch("solicitud-firmada")]
        [Authorize]
        public async Task<ActionResult> UpdateCartaFirma([FromForm] UpdateRetiroAhorroVoluntarioSolicitudFirmadoCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("constancia-transferencia")]
        [Authorize]
        public async Task<ActionResult> UpdateConstanciaTransferencia([FromForm] UpdateRetiroAhorroVoluntarioConstanciaTransferenciaCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpGet("employee/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEmployeeId(int id)
        {
            return Ok(await Mediator.Send(new GetRetiroAhorroVoluntarioByEmployeeIdQuery { Id = id }));
        }

        [HttpPatch("constancia-Pago")]
        [Authorize]
        public async Task<ActionResult> UpdateConstanciaPago([FromForm] UpdateRetiroAhorroVoluntarioConstanciaPagoCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}
