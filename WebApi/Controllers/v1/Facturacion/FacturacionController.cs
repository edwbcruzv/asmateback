using Application.Feautres.Facturacion.FacturaMovimientos.Commands.CreateFacturaMovimientoCommand;
using Application.Feautres.Facturacion.FacturaMovimientos.Commands.UpdateFacturaMovimientoCommand;
using Application.Feautres.Facturacion.Facturas.Commands.CreateFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Commands.UpdateFacturaCommand;
using Application.Feautres.Facturacion.FacturaMovimientos.Commands.DeleteFacturaMovimientoCommand;
using Application.Feautres.Facturacion.FacturaMovimientos.Queries.GetAllFacturaMovimientoByFactura;
using Application.Feautres.Facturacion.FacturaMovimientos.Queries.GetFacturaMovimientoById;
using Application.Feautres.Facturacion.Facturas.Commands.CancelarFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Commands.DeleteFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Commands.PdfFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Commands.Prueba;
using Application.Feautres.Facturacion.Facturas.Commands.TimbrarFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Queries.GetAllFacturaByCompany;
using Application.Feautres.Facturacion.Facturas.Queries.GetFacturaById;
using Application.Feautres.Facturacion.Facturas.Commands.ExcelFacturasCommand;
using Application.Feautres.Facturacion.Facturas.Commands.PagarFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Commands.CompressFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Commands.SendFacturaCommand;
using Application.Feautres.Facturacion.Facturas.Queries.GetAllFactura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Facturacion
{
    [ApiVersion("1.0")]
    public class FacturacionController : BaseApiController
    {
        [HttpPost("factura")]
        [Authorize]
        public async Task<ActionResult> Post(CreateFacturaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("factura/{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, UpdateFacturaCommand command)
        {
            if (id != command.Id) BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("factura/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteFacturaCommand { Id = id }));
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByCompany(int id)
        {
            return Ok(await Mediator.Send(new GetAllFacturaByCompanyQuery { CompanyId = id }));
        }

        [HttpGet("ComplementoPago/company/{CompanyId}/client/{ClientId}")]
        [Authorize]
        public async Task<ActionResult> GetByCompanyAndPPD(int CompanyId, int ClientId)
        {
            return Ok(await Mediator.Send(new GetFacturasByPPDQuery { CompanyId = CompanyId, ClientId = ClientId }));
        }

        [HttpGet("factura/{id}")]
        [Authorize]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetFacturaByIdQuery { Id = id }));
        }


        //Factura movimientos
        [HttpPost("facturaMovimiento")]
        [Authorize]
        public async Task<ActionResult> PostM(CreateFacturaMovimientoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("facturaMovimiento/{id}")]
        [Authorize]
        public async Task<ActionResult> PutM(int id, UpdateFacturaMovimientoCommand command)
        {
            if (id != command.Id) BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("facturaMovimiento/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteM(int id)
        {
            return Ok(await Mediator.Send(new DeleteFacturaMovimientoCommand { Id = id }));
        }

        [HttpGet("facturaMovimientos/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByFactura(int id)
        {
            return Ok(await Mediator.Send(new GetAllFacturaMovimientoByFacturaQuery { FacturaId = id }));
        }

        [HttpGet("facturaMovimiento/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByMId(int id)
        {
            return Ok(await Mediator.Send(new GetFacturaMovimientoByIdQuery { Id = id }));
        }

        [HttpPost("timbrarFactura/{Id}")]
        [Authorize]
        public async Task<ActionResult> Timbrar(int Id)
        {
            return Ok(await Mediator.Send(new TimbrarFacturaCommand { Id = Id }));
        }

        [HttpPost("cancelarFactura/{Id}")]
        [Authorize]
        public async Task<ActionResult> Cancelar(int Id)
        {
            return Ok(await Mediator.Send(new CancelarFacturaCommand { Id = Id }));
        }

        [HttpPost("pdfFactura/{Id}")]
        [Authorize]
        public async Task<ActionResult> PDF(int Id)
        {
            return Ok(await Mediator.Send(new PdfFacturaCommand { Id = Id }));
        }

        [HttpGet("excelFacturas/{CompanyId}")]
        [Authorize]
        public async Task<ActionResult> excelFacturas(int CompanyId)
        {
            return Ok(await Mediator.Send(new ExcelFacturasCommand { CompanyId  = CompanyId }));
        }

        [HttpPost("pagoPdfFactura/{Id}")]
        [Authorize]
        public async Task<ActionResult> PagoPDF(int Id, [FromForm] PagarFacturaCommand command)
        {
            if(command.Id != Id) return BadRequest(command.Id);
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("comprimirFacturas")]
        [Authorize]
        public async Task<ActionResult> rarFacturas(CompressFacturaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("enviarFactura/{id}")]
        [Authorize]
        public async Task<ActionResult> sendMailFactura(int id)
        {
            return Ok(await Mediator.Send(new SendFacturaCommand { Id = id }));
        }

        //[HttpPost("pruebas")]
        //public async Task<ActionResult> preuna()
        //{
        //    return Ok(await Mediator.Send(new PruebaCommand { }));
        //}



    }
}
