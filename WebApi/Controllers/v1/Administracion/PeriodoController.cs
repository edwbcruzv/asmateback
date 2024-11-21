using Application.Feautres.Administracion.Periodos.Commands.CargaExcelAsistenciasPorPeriodosCommand;
using Application.Feautres.Administracion.Periodos.Commands.CreatePeriodosCommand;
using Application.Feautres.Administracion.Periodos.Commands.ExcelAsistenciasPorPeriodosCommand;
using Application.Feautres.Administracion.Periodos.Commands.UpdatePeriodoCommand;
using Application.Feautres.Administracion.Periodos.Queries.GetAllPeriodosQuery;
using Application.Feautres.Administracion.Periodos.Queries.GetPeriodoByIdQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Administracion
{
    public class PeriodoController : BaseApiController
    {
        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> Post(CreatePeriodoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetPeriodoByIdQuery { Id = id }));
        }

        [HttpGet("compania/{company_id}/periocidad/{tipo}")]
        [Authorize]
        public async Task<IActionResult> GetByCompanyAndTipo(int company_id, int tipo)
        {
            return Ok(await Mediator.Send(new GetPeriodosByCompanyAndTipoQuery { CompanyId = company_id, Tipo = tipo }));
        }

        [HttpGet("excelAsistenciaPorPeriodo/{id}")]
        [Authorize]
        public async Task<IActionResult> ExcelAsistenciaPorPeriodo(int id)
        {
            return Ok(await Mediator.Send(new ExcelAsistenciasPorPeriodosCommand { Id = id }));
        }

        [HttpPost("cargaExcelAsistenciaPorPeriodo")]
        [Authorize]
        public async Task<IActionResult> CargaExcelAsistenciaPorPeriodo([FromForm] CargaExcelAsistenciasPorPeriodosCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> UpdatePeriodo(UpdatePeriodoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
