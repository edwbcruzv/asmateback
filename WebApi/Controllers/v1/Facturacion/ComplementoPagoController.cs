using Application.Feautres.Facturacion.ComplementoPagos.Commands.CreateComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.UpdateComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.DeleteComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.PdfComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.CompressComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.SendComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.ExcelComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.TimbrarComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.CancelarComplementoPagoCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Queries.GetComplementoPagoByIdQuery;
using Application.Feautres.Facturacion.ComplementoPagos.Queries.GetAllComplementoPagoByCompany;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.CreateComplementoPagoFacturaCommand;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.UpdateComplementoPagoFacturaCommand;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.DeleteComplementoPagoFacturaCommand;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Queries.GetAllComplementoPagoFacturaByComplementoPago;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Queries.GetComplementoPagoFacturaById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers.v1.Facturacion
{
    [ApiVersion("1.0")]
    public class ComplementoPagoController : BaseApiController
    {
        [HttpPost()]
        [Authorize]
        public async Task<ActionResult> Post([FromForm] CreateComplementoPagoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateComplementoPagoCommand command)
        {
            if (id != command.Id) BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteComplementoPagoCommand { Id = id }));
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByCompany(int id)
        {
            return Ok(await Mediator.Send(new GetAllComplementoPagoByCompanyQuery { CompanyId = id }));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetComplementoPagoByIdQuery { Id = id }));
        }


        //Factura movimientos
        [HttpPost("AsociarFactura")]
        [Authorize]
        public async Task<ActionResult> PostM(CreateComplementoPagoFacturaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("AsociarFactura/{id}")]
        [Authorize]
        public async Task<ActionResult> PutM(int id, UpdateComplementoPagoFacturaCommand command)
        {
            if (id != command.Id) BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("AsociarFactura/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteM(int id)
        {
            return Ok(await Mediator.Send(new DeleteComplementoPagoFacturaCommand { Id = id }));
        }

        [HttpGet("AsociarFactura/ComplementoPago/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByComplementoPago(int id)
        {
            return Ok(await Mediator.Send(new GetAllComplementoPagoFacturaByComplementoPagoQuery { Id = id }));
        }

        [HttpGet("AsociarFactura/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByMId(int id)
        {
            return Ok(await Mediator.Send(new GetComplementoPagoFacturaByIdQuery { Id = id }));
        }

        [HttpPost("timbrarComplemento/{Id}")]
        [Authorize]
        public async Task<ActionResult> Timbrar(int Id)
        {
            return Ok(await Mediator.Send(new TimbrarComplementoPagoCommand { Id = Id }));
        }

        [HttpPost("cancelarComplemento/{Id}")]
        [Authorize]
        public async Task<ActionResult> Cancelar(int Id)
        {
            return Ok(await Mediator.Send(new CancelarComplementoPagoCommand { Id = Id }));
        }

        [HttpPost("pdf/{Id}")]
        [Authorize]
        public async Task<ActionResult> PDF(int Id)
        {
            return Ok(await Mediator.Send(new PdfComplementoPagoCommand { Id = Id }));
        }

        [HttpGet("excelComplemento/{CompanyId}")]
        [Authorize]
        public async Task<ActionResult> excelFacturas(int CompanyId)
        {
            return Ok(await Mediator.Send(new ExcelComplementoPagoCommand { CompanyId = CompanyId }));
        }

        [HttpPost("comprimirComplemento")]
        [Authorize]
        public async Task<ActionResult> rarFacturas(CompressComplementoPagoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }












        [HttpPost("enviarComplemento/{id}")]
        [Authorize]
        public async Task<ActionResult> sendMailFactura(int id)
        {
            return Ok(await Mediator.Send(new SendComplementoPagoCommand { Id = id }));
        }


















    }
}
