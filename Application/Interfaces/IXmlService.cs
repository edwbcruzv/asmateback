using Application.DTOs.Administracion;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteCommand;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolso;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IXmlService
    {
        Task<Comprobante> GetComprobanteByXML(string file_xml, CreateComprobanteCommand request);
        public Task<MovimientoReembolso> GetMovimientoReembolsoByXML(string xml_path, CreateMovimientoReembolsoFacturaCommand request);
        
    }
}
