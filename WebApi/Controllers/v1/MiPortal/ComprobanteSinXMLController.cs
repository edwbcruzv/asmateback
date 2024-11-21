using Application.Feautres.MiPortal.ComprobantesSinXML.Commands.CreateComprobanteSinXML;
using Application.Feautres.MiPortal.ComprobantesSinXML.Commands.DeleteComprobanteSinXML;
using Application.Feautres.MiPortal.ComprobantesSinXML.Commands.UpdateComprobanteSinXML;
using Application.Feautres.MiPortal.ComprobantesSinXML.Queries.GetAllComprobantesSinXML;
using Application.Feautres.MiPortal.ComprobantesSinXML.Queries.GetComprobanteSinXMLbyId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class ComprobanteSinXMLController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateComprobanteSinXMLCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetComprobanteSinXMLbyIdQuery { Id = id }));
        }

        [HttpGet("viatico/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByViaticoId(int id)
        {
            return Ok(await Mediator.Send(new GetComprobantesSinXMLByViaticoIdQuery { Id = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch([FromForm] UpdateComprobanteSinXMLCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteComprobanteSinXMLCommand { Id = id }));
        }

    }

}
