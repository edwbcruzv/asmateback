using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteCommand;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteSinXML;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteValeAzulCommand;
using Application.Feautres.MiPortal.Comprobantes.Commands.DeleteComprobanteCommand;
using Application.Feautres.MiPortal.Comprobantes.Commands.UpdateComprobanteCommand;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteFacturaExtrangera;
using Application.Feautres.MiPortal.Comprobantes.Queries.GetAllComprobantes;
using Application.Feautres.MiPortal.Comprobantes.Queries.GetComprobanteById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobantePagoImpuestos;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class ComprobanteController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateComprobanteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("valeAzul")]
        [Authorize]
        public async Task<IActionResult> PostValeAzul(CreateComprobanteValeAzulCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("sinXML")]
        [Authorize]
        public async Task<IActionResult> PostSinXML([FromForm] CreateComprobanteSinXMLCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("facturaExtranjera")]
        [Authorize]
        public async Task<IActionResult> PostFacturaExtranjera([FromForm] CreateComprobanteFacturaExtranjeraCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("pagoImpuestos")]
        [Authorize]
        public async Task<IActionResult> PostPagoImpuestos([FromForm] CreateComprobantePagoImpuestosCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetComprobanteByIdQuery { Id = id }));
        }

        [HttpGet("viatico/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByViaticoId(int id)
        {
            return Ok(await Mediator.Send(new GetComprobantesByViaticoQuery { Id = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch([FromForm] UpdateComprobanteCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteComprobanteCommand { Id = id }));
        }

    }

}
