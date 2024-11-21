using Application.Feautres.MiPortal.Incidencias.Commands.CreateIncidenciaCommand;
using Application.Feautres.MiPortal.Incidencias.Commands.UpdateIncidenciaCommmand;
using Application.Feautres.MiPortal.Incidencias.Queries.GenerarIncidenciaPDFCommand;
using Application.Feautres.MiPortal.Incidencias.Queries.GetDiasIncidenciaByEmployeeCommand;
using Application.Feautres.MiPortal.Incidencias.Queries.GetIncidenciaByIdCommand;
using Application.Feautres.MiPortal.Incidencias.Queries.GetIncidenciasByCompanyCommand;
using Application.Feautres.MiPortal.Incidencias.Queries.GetIncidenciasByEmployeeCommand;
using Application.Feautres.MiPortal.Incidencias.Queries.ObtenerArchivoIncidenciaCommand;
using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pkcs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class IncidenciasController : BaseApiController
    {
        [HttpPost("CrearIncidenciaVacaciones/{empleadoId}")]
        [Authorize]
        public async Task<ActionResult> CrearIncidenciaVacaciones(CreateIncidenciaVacacionesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CrearIncidenciaIncapacidad/{empleadoId}")]
        [Authorize]
        public async Task<ActionResult> CrearIncidenciaIncapacidad(CreateIncidenciaIncapacidadCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpPost("CrearIncidenciaCualquiera/{empleadoId}")]
        [Authorize]
        public async Task<ActionResult> CrearIncidenciaCualquiera(CreateIncidenciaCualquieraCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("CambiarEstatusIncidencia/{id}")]
        [Authorize]

        public async Task<ActionResult> CambiarEstatusIncidencia(CambiarEstatusIncidenciaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("SubirArchivoIncidencia")]
        [Authorize]

        public async Task<ActionResult> SubirArchivoIncidencia([FromForm] SubirArchivoIncidenciaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("ActualizarMotivoIncidencia/{id}")]
        [Authorize]

        public async Task<ActionResult> ActualizarMotivoIncidencia(ModificarMotivoIncidenciaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("ObtenerArchivoIncidencia/{id}")]
        [Authorize]
        public async Task<ActionResult> ObtenerArchivoIncidencia(int id)
        {
            return Ok(await Mediator.Send(new ObtenerArchivoIncidenciaCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult> GetIncidenciaById(int id)
        {
            return Ok (await Mediator.Send(new GetIncidenciaByIdCommand { Id = id }));
        }

        [HttpGet("employee/{id}")]
        [Authorize]

        public async Task<ActionResult> IncidenciasPorEmpleado(int id)
        {
            return Ok(await Mediator.Send(new GetIncidenciasByEmployeeCommand { EmployeeId = id}));
        }

        [HttpGet("company/{id}")]
        [Authorize]

        public async Task<ActionResult> IncidenciasPorCompany(int id)
        {
            return Ok (await Mediator.Send(new GetIncidenciasByCompanyCommand { CompanyId = id }));
        }

        [HttpGet("DiasIncidenciaPorEmpleado/{employeeId}")]
        [Authorize]
        public async Task<ActionResult> DiasIncidenciaPorEmpleado(int employeeId)
        {
            return Ok (await Mediator.Send(new GetDiasIncidenciaByEmployeeCommand { EmpleadoId =  employeeId }));
        }

        [HttpGet("GenerarIncidenciaPDF/{id}")]
        [Authorize]
        
        public async Task<ActionResult> GenerarIncidenciaPDF(int id)
        {
            return Ok(await Mediator.Send(new GenerarIncidenciaPDFCommand { Id = id }));
        }

    }
}
