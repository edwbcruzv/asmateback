using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteCommand;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolso;
using Application.Interfaces;
using Application.Specifications.Catalogos;
using AutoMapper;
using Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shared.Services
{
    public class XmlService : IXmlService
    {
        
        private readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
        private readonly IRepositoryAsync<TipoComprobante> _repositoryAsyncTipoComprobante;
        private readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;
        private readonly IMapper _mapper;

        public XmlService(
                       IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal,
                       IRepositoryAsync<TipoComprobante> repositoryAsyncTipoComprobante,
                       IRepositoryAsync<FormaPago> repositoryAsyncFormaPago,
                       IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago
,
                       IMapper mapper)
        {

            _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;
            _repositoryAsyncTipoComprobante = repositoryAsyncTipoComprobante;
            _repositoryAsyncFormaPago = repositoryAsyncFormaPago;
            _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;
            _mapper = mapper;
        }
        public async Task<MovimientoReembolso> GetMovimientoReembolsoByXML(string xml_path, CreateMovimientoReembolsoFacturaCommand request)
        {
            var mov_reembolso = _mapper.Map<MovimientoReembolso>(request);


            // XML string
            string xmlString = @"C:\" + xml_path;

            // Navegar por los elementos usando LINQ
            XElement comprobanteElement = XElement.Load(xmlString);
            //XNamespace cfdiNamespace = "http://www.sat.gob.mx/cfd/4";
            XNamespace cfdiNamespace = comprobanteElement.GetNamespaceOfPrefix("cfdi");
            XNamespace implocalNamespace = "http://www.sat.gob.mx/implocal";

            // Obtener los atributos del comprobante
            string certificado = comprobanteElement.Attribute("Certificado").Value;
            string lugarExpedicion = comprobanteElement.Attribute("LugarExpedicion").Value;
            string tipoComprobante = comprobanteElement.Attribute("TipoDeComprobante").Value;
            string metodoPago = comprobanteElement.Attribute("MetodoPago").Value;
            string formaPago = comprobanteElement.Attribute("FormaPago").Value;
            string subTotal = comprobanteElement.Attribute("SubTotal").Value;
            string total = comprobanteElement.Attribute("Total").Value;
            

            mov_reembolso.LugarExpedicion = lugarExpedicion;
            var tipo_comprobante_elem = await _repositoryAsyncTipoComprobante.FirstOrDefaultAsync(new TipoComprobanteByClaveSpecification(tipoComprobante));
            mov_reembolso.TipoComprobanteId = tipo_comprobante_elem.Id;
            var metodo_pago_elem = await _repositoryAsyncMetodoPago.FirstOrDefaultAsync(new MetodoPagoByClaveSpecification(metodoPago));
            mov_reembolso.MetodoPagoId = metodo_pago_elem.Id;
            var forma_pago_elem = await _repositoryAsyncFormaPago.FirstOrDefaultAsync(new FormaPagoByClaveSpecification(formaPago));
            mov_reembolso.FormaPagoId = forma_pago_elem.Id;
            mov_reembolso.Subtotal = Double.Parse(subTotal);
            mov_reembolso.Total = Double.Parse(total);

            mov_reembolso.IVATrasladados = 0.0;
            mov_reembolso.IVARetenidos = 0.0;
            mov_reembolso.ISR = 0.0;
            mov_reembolso.IEPS = 0.0;
            mov_reembolso.ISH = 0.0;

            // Obtener los elementos Emisor y Receptor
            XElement emisorElement = comprobanteElement.Element(cfdiNamespace + "Emisor");
            XElement receptorElement = comprobanteElement.Element(cfdiNamespace + "Receptor");

            // Obtener atributos de Emisor
            string emisorNombre = emisorElement.Attribute("Nombre").Value;
            string emisorRfc = emisorElement.Attribute("Rfc").Value;
            string emisorRegimenFiscal = emisorElement.Attribute("RegimenFiscal").Value;

            mov_reembolso.EmisorNombre = emisorNombre;
            mov_reembolso.EmisorRFC = emisorRfc;
            var regimen_fiscal_elem = await _repositoryAsyncRegimenFiscal.FirstOrDefaultAsync(new RegimenFicalByClaveSpecification(emisorRegimenFiscal));
            mov_reembolso.RegimenFiscalId = regimen_fiscal_elem.Id;


            // Obtener atributos de Receptor
            string receptorNombre = receptorElement.Attribute("Nombre").Value;
            string receptorRfc = receptorElement.Attribute("Rfc").Value;

            mov_reembolso.ReceptorNombre = receptorNombre;
            mov_reembolso.ReceptorRFC = receptorRfc;

            bool bandera_gasolina = false;
            // Navegar por los elementos de Conceptos
            XElement conceptosElement = comprobanteElement.Element(cfdiNamespace + "Conceptos");

            foreach (XElement conceptoElement in conceptosElement.Elements(cfdiNamespace + "Concepto"))
            {
                // Obtener atributos de Concepto
                string claveProducto = conceptoElement.Attribute("ClaveProdServ").Value;
                string descripcion = conceptoElement.Attribute("Descripcion").Value;

                if (claveProducto.Equals("15101514") || claveProducto.Equals("15101515") || claveProducto.Equals("15111512"))
                {
                    bandera_gasolina = true;
                }



            }

            // Obtener el elemento Impuestos fuera de Conceptos
            XElement impuestosComprobanteElement = comprobanteElement.Element(cfdiNamespace + "Impuestos");
            if (impuestosComprobanteElement != null)
            {
                if (impuestosComprobanteElement.Attribute("TotalImpuestosTrasladados") != null)
                {
                    var totalImpuestosTrasladados = impuestosComprobanteElement.Attribute("TotalImpuestosTrasladados").Value;
                    mov_reembolso.IVATrasladados = double.Parse(totalImpuestosTrasladados);
                }
                else
                {
                    mov_reembolso.IVATrasladados = 0.0;
                }

                XElement trasladosElement = impuestosComprobanteElement.Element(cfdiNamespace + "Traslados");
                if (trasladosElement != null)
                {
                    foreach (XElement trasladoElement in trasladosElement.Elements(cfdiNamespace + "Traslado"))
                    {
                        // Obtener atributos de Traslado en Impuestos del comprobante
                        string tipoImpuesto = trasladoElement.Attribute("Impuesto").Value;
                    }
                }

                XElement retencionesElement = impuestosComprobanteElement.Element(cfdiNamespace + "Retenciones");
                if (retencionesElement != null)
                {
                    foreach (XElement retencionElement in retencionesElement.Elements(cfdiNamespace + "Retencion"))
                    {
                        // Obtener atributos de Traslado en Impuestos del comprobante
                        string tipoImpuesto = retencionElement.Attribute("Impuesto").Value;
                        string importe = retencionElement.Attribute("Importe").Value;
                        if (tipoImpuesto.Equals("001"))
                        {
                            mov_reembolso.ISR = Double.Parse(importe);
                        }

                        if (tipoImpuesto.Equals("002"))
                        {
                            mov_reembolso.IVARetenidos = Double.Parse(importe);
                        }
                    }
                }

                if (bandera_gasolina)
                {
                    double subtotal = mov_reembolso.Subtotal;
                    double iva = (double)mov_reembolso.IVATrasladados;
                    double base_gravable = iva / 0.16;
                    mov_reembolso.IEPS = subtotal - base_gravable;

                    //Console.WriteLine("Comprobando gass : " + (base_gravable + iva + mov_reembolso.IEPS));
                }

            }

            // Obtener el complemento TimbreFiscalDigital
            XNamespace tfdNamespace = "http://www.sat.gob.mx/TimbreFiscalDigital";
            XElement timbreFiscalDigitalElement = comprobanteElement.Element(cfdiNamespace + "Complemento")
                .Element(tfdNamespace + "TimbreFiscalDigital");
            if (timbreFiscalDigitalElement != null)
            {
                // Obtener atributos del TimbreFiscalDigital
                string uuid = timbreFiscalDigitalElement.Attribute("UUID").Value;
                string fechaTimbrado = timbreFiscalDigitalElement.Attribute("FechaTimbrado").Value;

                mov_reembolso.Uuid = uuid;
                string format = "yyyy-MM-ddTHH:mm:ss";
                mov_reembolso.FechaTimbrado = DateTime.ParseExact(fechaTimbrado, format, CultureInfo.InvariantCulture);
            }


            // Obtener el complemento
            

            XElement impuestosLocalesElement = comprobanteElement.Element(cfdiNamespace + "Complemento")
                .Element(implocalNamespace+"ImpuestosLocales");
            if (impuestosLocalesElement != null)
            {
                // Obtener atributos del TimbreFiscalDigital
                string totalTraslados = impuestosLocalesElement.Attribute("TotaldeTraslados").Value;

                mov_reembolso.ISH = Double.Parse(totalTraslados);
            }

            Console.WriteLine("XML navegado exitosamente.");
            return mov_reembolso;

        }




        public async Task<Comprobante> GetComprobanteByXML(string xml_path, CreateComprobanteCommand request)
        {
            var mov_viatico = _mapper.Map<Comprobante>(request);


            // XML string
            string xmlString = @"C:\" + xml_path;

            // Navegar por los elementos usando LINQ
            XElement comprobanteElement = XElement.Load(xmlString);
            //XNamespace cfdiNamespace = "http://www.sat.gob.mx/cfd/4";
            XNamespace cfdiNamespace = comprobanteElement.GetNamespaceOfPrefix("cfdi");
            XNamespace implocalNamespace = "http://www.sat.gob.mx/implocal";

            // Obtener los atributos del comprobante
            string certificado = comprobanteElement.Attribute("Certificado").Value;
            string lugarExpedicion = comprobanteElement.Attribute("LugarExpedicion").Value;
            string tipoComprobante = comprobanteElement.Attribute("TipoDeComprobante").Value;
            string metodoPago = comprobanteElement.Attribute("MetodoPago").Value;
            string formaPago = comprobanteElement.Attribute("FormaPago").Value;
            string subTotal = comprobanteElement.Attribute("SubTotal").Value;
            string total = comprobanteElement.Attribute("Total").Value;


            mov_viatico.LugarExpedicion = lugarExpedicion;
            var tipo_comprobante_elem = await _repositoryAsyncTipoComprobante.FirstOrDefaultAsync(new TipoComprobanteByClaveSpecification(tipoComprobante));
            mov_viatico.TipoComprobanteId = tipo_comprobante_elem.Id;
            var metodo_pago_elem = await _repositoryAsyncMetodoPago.FirstOrDefaultAsync(new MetodoPagoByClaveSpecification(metodoPago));
            mov_viatico.MetodoPagoId = metodo_pago_elem.Id;
            var forma_pago_elem = await _repositoryAsyncFormaPago.FirstOrDefaultAsync(new FormaPagoByClaveSpecification(formaPago));
            mov_viatico.FormaPagoId = forma_pago_elem.Id;
            mov_viatico.SubTotal = float.Parse(subTotal);
            mov_viatico.Total = float.Parse(total);

            mov_viatico.IVATrasladados = 0.0;
            mov_viatico.IVARetenidos = 0.0;
            mov_viatico.ISR = 0.0;
            mov_viatico.IEPS = 0.0;
            mov_viatico.ISH = 0.0;

            // Obtener los elementos Emisor y Receptor
            XElement emisorElement = comprobanteElement.Element(cfdiNamespace + "Emisor");
            XElement receptorElement = comprobanteElement.Element(cfdiNamespace + "Receptor");

            // Obtener atributos de Emisor
            string emisorNombre = emisorElement.Attribute("Nombre").Value;
            string emisorRfc = emisorElement.Attribute("Rfc").Value;
            string emisorRegimenFiscal = emisorElement.Attribute("RegimenFiscal").Value;

            mov_viatico.EmisorNombre = emisorNombre;
            mov_viatico.EmisorRFC = emisorRfc;
            var regimen_fiscal_elem = await _repositoryAsyncRegimenFiscal.FirstOrDefaultAsync(new RegimenFicalByClaveSpecification(emisorRegimenFiscal));
            mov_viatico.RegimenFiscalId = regimen_fiscal_elem.Id;


            // Obtener atributos de Receptor
            string receptorNombre = receptorElement.Attribute("Nombre").Value;
            string receptorRfc = receptorElement.Attribute("Rfc").Value;

            mov_viatico.ReceptorNombre = receptorNombre;
            mov_viatico.ReceptorRFC = receptorRfc;

            bool bandera_gasolina = false;
            // Navegar por los elementos de Conceptos
            XElement conceptosElement = comprobanteElement.Element(cfdiNamespace + "Conceptos");

            foreach (XElement conceptoElement in conceptosElement.Elements(cfdiNamespace + "Concepto"))
            {
                // Obtener atributos de Concepto
                string claveProducto = conceptoElement.Attribute("ClaveProdServ").Value;
                string descripcion = conceptoElement.Attribute("Descripcion").Value;

                if (claveProducto.Equals("15101514") || claveProducto.Equals("15101515") || claveProducto.Equals("15111512"))
                {
                    bandera_gasolina = true;
                }



            }

            // Obtener el elemento Impuestos fuera de Conceptos
            XElement impuestosComprobanteElement = comprobanteElement.Element(cfdiNamespace + "Impuestos");
            if (impuestosComprobanteElement != null)
            {
                if (impuestosComprobanteElement.Attribute("TotalImpuestosTrasladados") != null)
                {
                    var totalImpuestosTrasladados = impuestosComprobanteElement.Attribute("TotalImpuestosTrasladados").Value;
                    mov_viatico.IVATrasladados = double.Parse(totalImpuestosTrasladados);
                }
                else
                {
                    mov_viatico.IVATrasladados = 0.0;
                }

                XElement trasladosElement = impuestosComprobanteElement.Element(cfdiNamespace + "Traslados");
                if (trasladosElement != null)
                {
                    foreach (XElement trasladoElement in trasladosElement.Elements(cfdiNamespace + "Traslado"))
                    {
                        // Obtener atributos de Traslado en Impuestos del comprobante
                        string tipoImpuesto = trasladoElement.Attribute("Impuesto").Value;
                    }
                }

                XElement retencionesElement = impuestosComprobanteElement.Element(cfdiNamespace + "Retenciones");
                if (retencionesElement != null)
                {
                    foreach (XElement retencionElement in retencionesElement.Elements(cfdiNamespace + "Retencion"))
                    {
                        // Obtener atributos de Traslado en Impuestos del comprobante
                        string tipoImpuesto = retencionElement.Attribute("Impuesto").Value;
                        string importe = retencionElement.Attribute("Importe").Value;
                        if (tipoImpuesto.Equals("001"))
                        {
                            mov_viatico.ISR = Double.Parse(importe);
                        }

                        if (tipoImpuesto.Equals("002"))
                        {
                            mov_viatico.IVARetenidos = Double.Parse(importe);
                        }
                    }
                }

                if (bandera_gasolina)
                {
                    double subtotal = (double)mov_viatico.SubTotal;
                    double iva = (double)mov_viatico.IVATrasladados;
                    double base_gravable = iva / 0.16;
                    mov_viatico.IEPS = subtotal - base_gravable;

                    //Console.WriteLine("Comprobando gass : " + (base_gravable + iva + mov_reembolso.IEPS));
                }

            }

            // Obtener el complemento TimbreFiscalDigital
            XNamespace tfdNamespace = "http://www.sat.gob.mx/TimbreFiscalDigital";
            XElement timbreFiscalDigitalElement = comprobanteElement.Element(cfdiNamespace + "Complemento")
                .Element(tfdNamespace + "TimbreFiscalDigital");
            if (timbreFiscalDigitalElement != null)
            {
                // Obtener atributos del TimbreFiscalDigital
                string uuid = timbreFiscalDigitalElement.Attribute("UUID").Value;
                string fechaTimbrado = timbreFiscalDigitalElement.Attribute("FechaTimbrado").Value;

                mov_viatico.Uuid = uuid;
                string format = "yyyy-MM-ddTHH:mm:ss";
                mov_viatico.FechaTimbrado = DateTime.ParseExact(fechaTimbrado, format, CultureInfo.InvariantCulture);
            }


            // Obtener el complemento


            XElement impuestosLocalesElement = comprobanteElement.Element(cfdiNamespace + "Complemento")
                .Element(implocalNamespace + "ImpuestosLocales");
            if (impuestosLocalesElement != null)
            {
                // Obtener atributos del TimbreFiscalDigital
                string totalTraslados = impuestosLocalesElement.Attribute("TotaldeTraslados").Value;

                mov_viatico.ISH = Double.Parse(totalTraslados);
            }

            Console.WriteLine("XML navegado exitosamente.");
            return mov_viatico;

        }


    }




}
