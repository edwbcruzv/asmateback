using Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.CreateMovimientoAhorroVoluntario;
using Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.DeleteMovimientoAhorroVoluntario;
using Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.UpdateMovimientoAhorroVoluntario;
using Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Queries.GetAllMovimientosAhorroVoluntario;
using Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Queries.GetMovimientoAhorroVoluntario;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class MovimientoAhorroVoluntarioController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateMovimientoAhorroVoluntarioCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdateMovimientoAhorroVoluntarioCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("company/{company_id}/employee/{employee_id}/ahorro/{ahorro_id}/movimiento/{movimiento_id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int company_id, int employee_id, int ahorro_id, int movimiento_id)
        {
            return Ok(await Mediator.Send(new DeleteMovimientoAhorroVoluntarioCommand { CompanyId = company_id, EmployeeId = employee_id, AhorroVoluntarioId = ahorro_id, MovimientoId = movimiento_id }));
        }

        // == GETS LOCOS BY CRUZ :) ====

        [HttpGet("company/{company_id}/employee/{employee_id}/ahorro/{ahorro_id}/movimiento/{movimiento_id}")]
        [Authorize]
        public async Task<IActionResult> GetByIds(int company_id, int employee_id, int ahorro_id, int movimiento_id)
        {
            return Ok(await Mediator.Send(new GetMovimientoAhorroVoluntarioByIdQuery { CompanyId = company_id, EmployeeId = employee_id, AhorroVoluntarioId = ahorro_id, MovimientoId = movimiento_id }));
        }

        [HttpGet("company/{company_id}/employee/{employee_id}/ahorro/{ahorro_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyIdAndEmployeeIdAndAhorroVoluntarioId(int company_id, int employee_id, int ahorro_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdQuery { CompanyId = company_id, EmployeeId = employee_id,  AhorroVoluntarioId = ahorro_id }));
        }

        // Peticion con insunsistencias por no tener ahorro, pero si te sirve usala con cuidado :)

        //[HttpGet("company/{company_id}/employee/{employee_id}")]
        //[Authorize]
        //public async Task<IActionResult> GetAllByCompanyIdAndEmployeeId(int company_id, int employee_id)
        //{
        //    return Ok(await Mediator.Send(new GetAllMovimientosAhorroVoluntarioByCompanyIdAndEmployeeIdQuery { CompanyId = company_id, EmployeeId = employee_id }));
        //}

        [HttpGet("company/{company_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyId(int company_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosAhorroVoluntarioByCompanyIdQuery { CompanyId = company_id }));
        }

        [HttpGet("employee/{employee_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEmployeeId(int employee_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosAhorroVoluntarioByEmployeeIdQuery { EmployeeId = employee_id }));
        }

    }
}
