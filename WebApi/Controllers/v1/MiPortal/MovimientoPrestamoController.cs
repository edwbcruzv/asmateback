using Application.Feautres.MiPortal.MovimientosPrestamo.Commands.CreateMovimientoPrestamo;
using Application.Feautres.MiPortal.MovimientosPrestamo.Commands.DeleteMovimientoPrestamo;
using Application.Feautres.MiPortal.MovimientosPrestamo.Commands.UpdateMovimientoPrestamo;
using Application.Feautres.MiPortal.MovimientosPrestamo.Queries.GetAllMovimientosPrestamo;
using Application.Feautres.MiPortal.MovimientosPrestamo.Queries.GetMovimientoPrestamoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.MiPortal
{
    public class MovimientoPrestamoController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateMovimientoPrestamoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdateMovimientoPrestamoCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("company/{company_id}/employee/{employee_id}/prestamo/{prestamo_id}/movimiento/{movimiento_id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int company_id, int employee_id, int prestamo_id, int movimiento_id)
        {
            return Ok(await Mediator.Send(new DeleteMovimientoPrestamoCommand { CompanyId = company_id, EmployeeId = employee_id, PrestamoId = prestamo_id, MovimientoId = movimiento_id }));
        }


        // == GETS LOCOS BY CRUZ :) ====

        [HttpGet("company/{company_id}/employee/{employee_id}/ahorro/{prestamo_id}/movimiento/{movimiento_id}")]
        [Authorize]
        public async Task<IActionResult> GetByIds(int company_id, int employee_id, int prestamo_id, int movimiento_id)
        {
            return Ok(await Mediator.Send(new GetMovimientoPrestamoByIdQuery { CompanyId = company_id, EmployeeId = employee_id, PrestamoId = prestamo_id, MovimientoId = movimiento_id }));
        }

        [HttpGet("company/{company_id}/employee/{employee_id}/ahorro/{prestamo_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyIdAndEmployeeIdAndPrestamoId(int company_id, int employee_id, int prestamo_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdQuery { CompanyId = company_id, EmployeeId = employee_id, PrestamoId = prestamo_id }));
        }

        [HttpGet("company/{company_id}/employee/{employee_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyIdAndEmployeeId(int company_id, int employee_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosPrestamoByCompanyIdAndEmployeeIdQuery { CompanyId = company_id, EmployeeId = employee_id }));
        }

        [HttpGet("company/{company_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyId(int company_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosPrestamoByCompanyIdQuery { CompanyId = company_id }));
        }

        [HttpGet("employee/{employee_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEmployeeId(int employee_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosPrestamoByEmployeeIdQuery { EmployeeId = employee_id }));
        }
    }
}
