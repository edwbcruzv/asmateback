using Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.CreateMovimientoAhorroWise;
using Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.DeleteMovimientoAhorroWise;
using Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.UpdateMovimientoAhorroWise;
using Application.Feautres.MiPortal.MovimientosAhorroWise.Queries.GetAllMovimientosAhorroWise;
using Application.Feautres.MiPortal.MovimientosAhorroWise.Queries.GetMovimientoAhorroWiseById;
using Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.EnviarCorreoEstadoDeCuentaWiseCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Facturacion.MiPortal.Commands.PdfEstadoDeCuentaWiseCommand;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class MovimientoAhorroWiseController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateMovimientoAhorroWiseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdateMovimientoAhorroWiseCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("company/{company_id}/employee/{employee_id}/ahorro/{ahorro_id}/movimiento/{movimiento_id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int company_id, int employee_id, int ahorro_id, int movimiento_id)
        {
            return Ok(await Mediator.Send(new DeleteMovimientoAhorroWiseCommand { CompanyId = company_id, EmployeeId = employee_id, AhorroWiseId = ahorro_id, MovimientoId = movimiento_id }));
        }

        // == GETS LOCOS BY CRUZ :) ====

        [HttpGet("company/{company_id}/employee/{employee_id}/ahorro/{ahorro_id}/movimiento/{movimiento_id}")]
        [Authorize]
        public async Task<IActionResult> GetByIds(int company_id, int employee_id, int ahorro_id, int movimiento_id)
        {
            return Ok(await Mediator.Send(new GetMovimientoAhorroWiseByIdQuery { CompanyId = company_id, EmployeeId = employee_id, AhorroWiseId = ahorro_id, MovimientoId = movimiento_id }));
        }

        [HttpGet("company/{company_id}/employee/{employee_id}/ahorro/{ahorro_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyIdAndEmployeeIdAndAhorroWiseId(int company_id, int employee_id, int ahorro_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdQuery { CompanyId = company_id, EmployeeId = employee_id, AhorroWiseId = ahorro_id }));
        }

        [HttpGet("company/{company_id}/employee/{employee_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyIdAndEmployeeId(int company_id, int employee_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosAhorroWiseByCompanyIdAndEmployeeIdQuery { CompanyId = company_id, EmployeeId = employee_id }));
        }

        [HttpGet("company/{company_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByCompanyId(int company_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosAhorroWiseByCompanyIdQuery { CompanyId = company_id }));
        }

        [HttpGet("employee/{employee_id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEmployeeId(int employee_id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosAhorroWiseByEmployeeIdQuery { EmployeeId = employee_id }));
        }

        [HttpPost("enviarCorreoEstadoDeCuenta")]
        [Authorize]
        public async Task<ActionResult> EnviarCorreoEstadoDeCuenta(EnviarCorreoEstadoDeCuentaWiseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("pdfEstadoDeCuentaWise")]
        [Authorize]
        public async Task<ActionResult> PDF(PdfEstadoDeCuentaWiseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
