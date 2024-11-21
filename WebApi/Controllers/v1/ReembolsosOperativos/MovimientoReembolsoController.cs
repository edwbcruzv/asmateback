using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolso;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaExtrangera;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaSinXML;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByPagoImpuestos;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByValeAzul;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.DeleteMovimientoReembolso;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Queries.GetAllMovimientosReembolso;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Queries.GetMovimientoReembolso;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.CreateReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetAllReembolsos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.ReembolsosOperativos
{
    public class MovimientoReembolsoController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetMovimientoReembolsoById { Id = id }));
        }


        [HttpGet("reembolsoid/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByReembolsoId(int id)
        {
            return Ok(await Mediator.Send(new GetAllMovimientosReembolsoByReembolsoId { ReembolsoId = id }));
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateMovimientoReembolsoFacturaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("sinxml")]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateMovimientoReembolsoByFacturaSinXMLCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("pagoimpuestos")]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateMovimientoReembolsoByPagoImpuestosCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("valeazul")]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateMovimientoReembolsoByValeAzulCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("FacturaExtrangera")]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateMovimientoReembolsoByFacturaExtranjeraCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteMovimientoReembolsoCommand { Id = id }));
        }
    }
}
