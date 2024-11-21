using Application.Feautres.Administracion.Clientes.Queries.GetClienteByCompany;
using Application.Feautres.Facturacion.Nominas.Commands;
using Application.Feautres.Facturacion.Nominas.Commands.CalcularAguinaldoByEmployeeCommand;
using Application.Feautres.Facturacion.Nominas.Commands.CalcularAguinaldoCommand;
using Application.Feautres.Facturacion.Nominas.Commands.CancelarNominaCommnad;
using Application.Feautres.Facturacion.Nominas.Commands.GeneratePeriodoExtraordinarioCommand;
using Application.Feautres.Facturacion.Nominas.Commands.GetNominasByCompanyIdAndPeriodCommand;
using Application.Feautres.Facturacion.Nominas.Commands.PdfNominaCommand;
using Application.Feautres.Facturacion.Nominas.Commands.TimbrarNominaCommand;
using Application.Feautres.Facturacion.Nominas.Commands.GetNominasByUserIdCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Facturacion.Nominas.Commands.EnviarCorreoNominaCommand;
using Application.Feautres.Facturacion.Nominas.Commands.ReporteNominaByPeriodoCommand;


namespace WebApi.Controllers.v1.Facturacion
{
    [ApiVersion("1.0")]
    public class NominaController : BaseApiController
    {
        [HttpPost("generaNominaByPeriodo/{id}")]
        [Authorize]
        public async Task<ActionResult> Post(int id)
        {
            return Ok(await Mediator.Send(new GenerateNominaByPeriodoCommand { PeriodoId = id }));
        }

        [HttpPost("generaPeriodoExtraordinario")]
        [Authorize]
        public async Task<ActionResult> GeneratePeriodoExtraordinario(int companyId, int periodoID)
        {
            return Ok(await Mediator.Send(new GeneratePeriodoExtraordinarioCommand { CompanyId = companyId, PeriodoId = periodoID }));
        }

        [HttpPost("generaAguinaldoPorPeriodo/{id}")]
        [Authorize]
        public async Task <ActionResult> GenerateAguinaldoByPeriodo(int id)
        {
            return Ok(await Mediator.Send(new CalcularAguinaldoCommand { PeriodoId = id }));
        }

        [HttpPost("generaAguinaldoPorEmpleado")]
        [Authorize]

        public async Task <ActionResult> GenerateAguinaldoByEmployee(int employeeID,int periodoID)
        {
            return Ok(await Mediator.Send(new CalcularAguinaldoByEmployeeCommand { EmployeeID = employeeID, PeriodoID = periodoID }));
        }

        [HttpPost("pdfNomina/{Id}")]
        [Authorize]
        public async Task<ActionResult> PDF(int Id)
        {
            return Ok(await Mediator.Send(new PdfNominaCommand { Id = Id }));
        }

        [HttpPost("timbrarNomina/{Id}")]
        [Authorize]
        public async Task<ActionResult> TimbrarNoimina(int Id)
        {
            return Ok(await Mediator.Send(new TimbrarNominaCommand { Id = Id }));
        }

        [HttpGet("company/{CId}/periodo/{PId}")]
        [Authorize]
        public async Task<IActionResult> GetNominasByCompanyIdAndPeriod(int CId, int PId)
        {
            return Ok(await Mediator.Send(new GetNominasByCompanyIdAndPeriodCommand { CompanyId = CId, PeriodoId = PId})); ;
        }

        [HttpGet("user/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetNominasByUserId(int Id)
        {
            return Ok(await Mediator.Send(new GetNominasByUserIdCommand { UserId = Id })); ;
        }


        [HttpPost("cancelarNomina/{Id}")]
        [Authorize]
        public async Task<ActionResult> CancelarNoimina(int Id)
        {
            return Ok(await Mediator.Send(new CancelarNominaCommand { Id = Id }));
        }

        [HttpPost("enviarCorreoNomina/{id}")]
        [Authorize]
        public async Task<ActionResult> EnviarCorreoNomina(int id)
        {
            return Ok(await Mediator.Send(new EnviarCorreoNominaCommand { Id = id }));
        }

        [HttpPost("reporteNominaByPeriodo/{id}")]
        [Authorize]
        public async Task<ActionResult> ReporteNominaByPeriodo(int id)
        {
            return Ok(await Mediator.Send(new ReporteNominaByPeriodoCommand { Id = id }));
        }
    }
}
