using Application.DTOs.Facturas;
using Application.Exceptions;
using Application.Feautres.Catalogos.Estados.GetEstadoByNombre;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Facturas;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Specifications.MiPortal.AhorrosWise;
using Application.Specifications.MiPortal.Prestamos;
using Application.Specifications.Nominas;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using iText.Layout.Element;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Shared.Services
{
    public class TimboxService : ITimboxService
    {


        private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
        private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsyncFacturaMovimiento;

        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
        private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsyncComplementoPagoFactura;

        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;
        private readonly IRepositoryAsync<TipoComprobante> _repositoryAsyncTipoComprobante;
        private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncTipoMoneda;
        private readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
        private readonly IRepositoryAsync<UsoCfdi> _repositoryAsyncUsoCfdi;

        private readonly IRepositoryAsync<UnidadMedida> _repositoryAsyncUnidadMedida;
        private readonly IRepositoryAsync<CveProducto> _repositoryAsyncCveProducto;
        private readonly IRepositoryAsync<ObjetoImpuesto> _repositoryAsyncObjetoImpuesto;

        private readonly EnvironmentService _environmentService;

        private readonly ITotalesMovsService _totalesMovsService;

        private string _baseFile;

        private string _fileOpenSsl;
        private string _fileCadenaOriginal_4_0;

        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly IRepositoryAsync<NominaPercepcion> _repositoryAsyncNominaPercepciones;
        private readonly IRepositoryAsync<NominaDeduccion> _repositoryAsyncNominaDeducciones;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
        private readonly IRepositoryAsync<Puesto> _repositoryAsyncPuesto;
        private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
        private readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsyncTipoPeriocidadPago;
        private readonly IRepositoryAsync<TipoRiesgoTrabajo> _repositoryAsyncTipoRiesgoTrabajo;
        private readonly IRepositoryAsync<TipoContrato> _repositoryAsyncTipoContrato;
        private readonly IRepositoryAsync<TipoJornada> _repositoryAsyncTipoJornada;
        private readonly IRepositoryAsync<TipoRegimen> _repositoryAsyncTipoRegimen;
        private readonly IRepositoryAsync<NominaOtroPago> _repositoryAsyncNominaOtroPago;
        private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;
        private readonly IRepositoryAsync<AhorroWise> _repositoryAsyncAhorroWise;
        private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsyncMovimientoAhorroWise;
        private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
        private readonly IRepositoryAsync<MovimientoPrestamo> _repositoryAsyncMovimientoPrestamo;
        private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
        private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsyncMovimientoAhorroVoluntario;

        public TimboxService(IRepositoryAsync<Factura> repositoryAsyncFactura,
            IRepositoryAsync<FacturaMovimiento> repositoryAsyncFacturaMovimiento, IRepositoryAsync<Company> repositoryAsyncCompany,
            IRepositoryAsync<FormaPago> repositoryAsyncFormaPago, IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago,
            IRepositoryAsync<TipoComprobante> repositoryAsyncTipoComprobante, IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda,
            IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal, IRepositoryAsync<UsoCfdi> repositoryAsyncUsoCfdi,
            IRepositoryAsync<UnidadMedida> repositoryAsyncUnidadMedida, IRepositoryAsync<CveProducto> repositoryAsyncCveProducto,
            IRepositoryAsync<ObjetoImpuesto> repositoryAsyncObjetoImpuesto, EnvironmentService environmentService,
            ITotalesMovsService totalesMovsService, IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago,
            IRepositoryAsync<ComplementoPagoFactura> repositoryAsyncComplementoPagoFactura, IRepositoryAsync<Nomina> repositoryAsyncNomina,
            IRepositoryAsync<NominaPercepcion> repositoryAsyncNominaPercepciones, IRepositoryAsync<NominaDeduccion> repositoryAsyncNominaDeducciones,
            IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Puesto> repositoryAsyncPuesto, IRepositoryAsync<Departamento> repositoryAsyncDepartamento,
            IRepositoryAsync<TipoPeriocidadPago> repositoryAsyncTipoPeriocidadPago, IRepositoryAsync<TipoRiesgoTrabajo> repositoryAsyncTipoRiesgoTrabajo,
            IRepositoryAsync<TipoContrato> repositoryAsyncTipoContrato, IRepositoryAsync<TipoJornada> repositoryAsyncTipoJornada,
            IRepositoryAsync<TipoRegimen> repositoryAsyncTipoRegimen, IRepositoryAsync<NominaOtroPago> repositoryAsyncNominaOtroPago,
            IRepositoryAsync<Estado> repositoryAsyncEstado, IRepositoryAsync<AhorroWise> repositoryAsyncAhorroWise,
            IRepositoryAsync<MovimientoAhorroWise> repositoryAsyncMovimientoAhorroWise, IRepositoryAsync<Prestamo> repositoryPrestamo,
            IRepositoryAsync<MovimientoPrestamo> repositoryAsyncMovimientoPrestamo, IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario,
            IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsyncMovimientoAhorroVoluntario)
        {
            _repositoryAsyncFactura = repositoryAsyncFactura;
            _repositoryAsyncFacturaMovimiento = repositoryAsyncFacturaMovimiento;
            _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
            _repositoryAsyncComplementoPagoFactura = repositoryAsyncComplementoPagoFactura; 

            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncFormaPago = repositoryAsyncFormaPago;
            _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;
            _repositoryAsyncTipoComprobante = repositoryAsyncTipoComprobante;
            _repositoryAsyncTipoMoneda = repositoryAsyncTipoMoneda;
            _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;
            _repositoryAsyncUsoCfdi = repositoryAsyncUsoCfdi;
            _repositoryAsyncUnidadMedida = repositoryAsyncUnidadMedida;
            _repositoryAsyncCveProducto = repositoryAsyncCveProducto;
            _repositoryAsyncObjetoImpuesto = repositoryAsyncObjetoImpuesto;

            _environmentService = environmentService;
            _totalesMovsService = totalesMovsService;

            _baseFile = @"C:\StaticFiles\Mate\temp\";

            _fileOpenSsl = @"C:\StaticFiles\Mate\openssl\openssl.exe";
            _fileCadenaOriginal_4_0 = "C:\\StaticFiles\\Mate\\Timbrado\\cadenaoriginal_4_0.xslt";

            _repositoryAsyncNomina = repositoryAsyncNomina;
            _repositoryAsyncNominaPercepciones = repositoryAsyncNominaPercepciones;
            _repositoryAsyncNominaDeducciones = repositoryAsyncNominaDeducciones;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncPuesto = repositoryAsyncPuesto;
            _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
            _repositoryAsyncTipoPeriocidadPago = repositoryAsyncTipoPeriocidadPago;
            _repositoryAsyncTipoRiesgoTrabajo = repositoryAsyncTipoRiesgoTrabajo;
            _repositoryAsyncTipoContrato = repositoryAsyncTipoContrato;
            _repositoryAsyncTipoJornada = repositoryAsyncTipoJornada;
            _repositoryAsyncTipoRegimen = repositoryAsyncTipoRegimen;
            _repositoryAsyncNominaOtroPago = repositoryAsyncNominaOtroPago;
            _repositoryAsyncEstado = repositoryAsyncEstado;
            _repositoryAsyncAhorroWise = repositoryAsyncAhorroWise;
            _repositoryAsyncMovimientoAhorroWise = repositoryAsyncMovimientoAhorroWise;
            _repositoryAsyncPrestamo = repositoryPrestamo;
            _repositoryAsyncMovimientoPrestamo = repositoryAsyncMovimientoPrestamo;
            _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
            _repositoryAsyncMovimientoAhorroVoluntario = repositoryAsyncMovimientoAhorroVoluntario;
        }

        public async Task<Response<bool>> timbrar(int Id)
        {
            var factura = await _repositoryAsyncFactura.GetByIdAsync(Id);

            if (factura == null)
            {
                throw new ApiException($"Factura no encontrado para Id ${Id}");
            }

            if (factura.Estatus != 1)
            {
                throw new ApiException($"Factura no puede ser timbrado para Id ${Id}");
            }

            var facturaMovimientoList = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(Id));

            if (facturaMovimientoList.Count == 0)
            {
                throw new ApiException($"Factura no cuenta con movimientos");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(factura.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            if (company.Certificado == null || company.Certificado.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con certificados.");
            }

            if (company.PrivateKeyFile == null || company.PrivateKeyFile.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con llave privada");
            }

            if (company.PassPrivateKey == null || company.PassPrivateKey.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con constrasenia de llave privada");
            }

            TimeZoneInfo setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime fechaActual = TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);
            fechaActual = fechaActual.AddHours(-1);

            string generalName = factura.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;
            string fechaActualString = fechaActual.ToString("yyyy-MM-ddThh:mm:ss");

            var certificadoPem = getCertificadoPem(generalName, company.Certificado);


            if (certificadoPem.Equals(""))
            {
                throw new ApiException($"Certificado invalido");
            }

            certificadoPem = certificadoPem.Replace("-----BEGIN CERTIFICATE-----", "");
            certificadoPem = certificadoPem.Replace("-----END CERTIFICATE-----", "");
            certificadoPem = certificadoPem.Replace("\n", "");

            var noCertificado = getNoCertificado(company.Certificado);

            var totalMovs = _totalesMovsService.getTotalesFormMovs(facturaMovimientoList);

            using (XmlWriter writer = XmlWriter.Create($"{_baseFile}{generalName}.xml"))
            {
                var fp = await _repositoryAsyncFormaPago.GetByIdAsync(factura.FormaPagoId);
                var mp = await _repositoryAsyncMetodoPago.GetByIdAsync(factura.MetodoPagoId);
                var tc = await _repositoryAsyncTipoComprobante.GetByIdAsync(factura.TipoComprobanteId);
                var tm = await _repositoryAsyncTipoMoneda.GetByIdAsync(factura.TipoMonedaId);
                var uc = await _repositoryAsyncUsoCfdi.GetByIdAsync(factura.UsoCfdiId);
                var rfe = await _repositoryAsyncRegimenFiscal.GetByIdAsync(factura.EmisorRegimenFiscalId);
                var rfr = await _repositoryAsyncRegimenFiscal.GetByIdAsync(factura.ReceptorRegimenFiscalId);

                if (company.FolioFactura == null)
                {
                    company.FolioFactura = 1;
                }
                else
                {
                    company.FolioFactura = company.FolioFactura + 1;
                }

                //escribiendo cabecera
                writer.WriteStartDocument();
                writer.WriteStartElement("cfdi", "Comprobante", "http://www.sat.gob.mx/cfd/4");
                writer.WriteAttributeString("xmlns", "cfdi", null, "http://www.sat.gob.mx/cfd/4");
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd");
                writer.WriteAttributeString("Version", "4.0");
                writer.WriteAttributeString("Serie", "A");
                writer.WriteAttributeString("Folio", company.FolioFactura.ToString().Trim());
                writer.WriteAttributeString("Fecha", fechaActualString);
                writer.WriteAttributeString("Sello", "");
                writer.WriteAttributeString("FormaPago", fp.FormaDePago);
                writer.WriteAttributeString("NoCertificado", noCertificado);
                writer.WriteAttributeString("Certificado", certificadoPem);
                writer.WriteAttributeString("SubTotal", totalMovs.subTotal.ToString("0.00"));
                writer.WriteAttributeString("Total", totalMovs.total.ToString("0.00"));
                writer.WriteAttributeString("TipoDeComprobante", tc.TipoDeComprobante);
                writer.WriteAttributeString("MetodoPago", mp.MetodoDePago);
                writer.WriteAttributeString("LugarExpedicion", factura.LugarExpedicion);
                writer.WriteAttributeString("Exportacion", "01");
                writer.WriteAttributeString("TipoCambio", "1");
                writer.WriteAttributeString("Moneda", tm.CodigoIso);
                writer.WriteAttributeString("Descuento", totalMovs.descuentoTotal.ToString("0.00"));

                writer.WriteStartElement("cfdi", "Emisor", null);
                writer.WriteAttributeString("Rfc", factura.EmisorRfc);
                writer.WriteAttributeString("Nombre", factura.EmisorRazonSocial);
                writer.WriteAttributeString("RegimenFiscal", rfe.RegimenFiscalCve);
                writer.WriteEndElement();

                writer.WriteStartElement("cfdi", "Receptor", null);
                writer.WriteAttributeString("Rfc", factura.ReceptorRfc);
                writer.WriteAttributeString("Nombre", factura.ReceptorRazonSocial);
                writer.WriteAttributeString("RegimenFiscalReceptor", rfr.RegimenFiscalCve); // agregar columna
                writer.WriteAttributeString("DomicilioFiscalReceptor", factura.ReceptorDomicilioFiscal); // agregar columna
                writer.WriteAttributeString("UsoCFDI", uc.UsoDeCfdi);
                writer.WriteEndElement();

                writer.WriteStartElement("cfdi", "Conceptos", null);

                foreach (var fmTemp in facturaMovimientoList)
                {

                    var um = await _repositoryAsyncUnidadMedida.GetByIdAsync(fmTemp.UnidadMedidaId);
                    var cp = await _repositoryAsyncCveProducto.GetByIdAsync(fmTemp.CveProductoId);
                    var oi = await _repositoryAsyncObjetoImpuesto.GetByIdAsync(fmTemp.ObjetoImpuestoId);

                    writer.WriteStartElement("cfdi", "Concepto", null);
                    writer.WriteAttributeString("ClaveProdServ", cp.Producto.ToString().Trim());
                    writer.WriteAttributeString("ClaveUnidad", um.UnidadDeMedida.Trim());
                    writer.WriteAttributeString("Cantidad", fmTemp.Cantidad.ToString().Trim());
                    writer.WriteAttributeString("Unidad", um.Nombre.Trim());
                    writer.WriteAttributeString("Descripcion", fmTemp.Descripcion.ToString().Trim());
                    writer.WriteAttributeString("ValorUnitario", fmTemp.PrecioUnitario.ToString().Trim());
                    decimal importe = fmTemp.Cantidad * fmTemp.PrecioUnitario;
                    writer.WriteAttributeString("Importe", importe.ToString("0.00"));
                    decimal descuento = fmTemp.Descuento;
                    writer.WriteAttributeString("Descuento", descuento.ToString("0.00"));
                    writer.WriteAttributeString("ObjetoImp", oi.ObjetosImpuesto.ToString().Trim());

                    if (fmTemp.ObjetoImpuestoId == 2)
                    {
                        writer.WriteStartElement("cfdi", "Impuestos", null);

                        if (fmTemp.Iva)
                        {
                            writer.WriteStartElement("cfdi", "Traslados", null);
                            writer.WriteStartElement("cfdi", "Traslado", null);
                            decimal ivaImporte = importe * 0.16m;
                            writer.WriteAttributeString("Base", importe.ToString("0.00"));
                            writer.WriteAttributeString("Impuesto", "002");
                            writer.WriteAttributeString("TipoFactor", "Tasa");
                            writer.WriteAttributeString("TasaOCuota", "0.160000");
                            writer.WriteAttributeString("Importe", ivaImporte.ToString("0.00"));
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }

                        if (fmTemp.Iva6 || fmTemp.RetencionIsr || fmTemp.RetencionIva)
                        {
                            writer.WriteStartElement("cfdi", "Retenciones", null);
                            if (fmTemp.Iva6)
                            {
                                writer.WriteStartElement("cfdi", "Retencion", null);
                                decimal iva6Importe = importe * 0.06m;
                                writer.WriteAttributeString("Base", importe.ToString("0.00"));
                                writer.WriteAttributeString("Impuesto", "002");
                                writer.WriteAttributeString("TipoFactor", "Tasa");
                                writer.WriteAttributeString("TasaOCuota", "0.060000");
                                writer.WriteAttributeString("Importe", iva6Importe.ToString("0.00"));
                                writer.WriteEndElement();
                            }
                            if (fmTemp.RetencionIsr)
                            {
                                writer.WriteStartElement("cfdi", "Retencion", null);
                                decimal retencionIsrImporte = importe * 0.100000m;
                                writer.WriteAttributeString("Base", importe.ToString("0.00"));
                                writer.WriteAttributeString("Impuesto", "001");
                                writer.WriteAttributeString("TipoFactor", "Tasa");
                                writer.WriteAttributeString("TasaOCuota", "0.100000");
                                writer.WriteAttributeString("Importe", retencionIsrImporte.ToString("0.00"));
                                writer.WriteEndElement();
                            }
                            if (fmTemp.RetencionIva)
                            {
                                writer.WriteStartElement("cfdi", "Retencion", null);
                                decimal retencionIvamporte = importe * 0.106700m;
                                writer.WriteAttributeString("Base", importe.ToString("0.00"));
                                writer.WriteAttributeString("Impuesto", "002");
                                writer.WriteAttributeString("TipoFactor", "Tasa");
                                writer.WriteAttributeString("TasaOCuota", "0.106700");
                                writer.WriteAttributeString("Importe", retencionIvamporte.ToString("0.00"));
                                writer.WriteEndElement();
                            }

                            writer.WriteEndElement();

                        }

                        writer.WriteEndElement();

                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                if (totalMovs.tieneTraslados || totalMovs.tieneRetencionIva 
                    || totalMovs.tieneRetencionIsr || totalMovs.tieneRetencionIva6)
                {

                    writer.WriteStartElement("cfdi", "Impuestos", null);

                    if (totalMovs.tieneRetencionIva || totalMovs.tieneRetencionIsr || totalMovs.tieneRetencionIva6)
                    {
                        writer.WriteAttributeString("TotalImpuestosRetenidos", totalMovs.retenidosTotal.ToString("0.00"));
                    }

                    if (totalMovs.tieneTraslados)
                    {
                        writer.WriteAttributeString("TotalImpuestosTrasladados", totalMovs.trasladadosTotal.ToString("0.00"));
                    }

                    if (totalMovs.tieneRetencionIva || totalMovs.tieneRetencionIsr || totalMovs.tieneRetencionIva6)
                    {
                        writer.WriteStartElement("cfdi", "Retenciones", null);
                        if (totalMovs.tieneRetencionIva)
                        {
                            writer.WriteStartElement("cfdi", "Retencion", null);
                            writer.WriteAttributeString("Impuesto", "002");
                            writer.WriteAttributeString("Importe", totalMovs.retencionIvaTotal.ToString("0.00"));
                            writer.WriteEndElement();
                        }
                        if (totalMovs.tieneRetencionIsr)
                        {
                            writer.WriteStartElement("cfdi", "Retencion", null);
                            writer.WriteAttributeString("Impuesto", "001");
                            writer.WriteAttributeString("Importe", totalMovs.retencionIsrTotal.ToString("0.00"));
                            writer.WriteEndElement();
                        }
                        if (totalMovs.tieneRetencionIva6)
                        {
                            writer.WriteStartElement("cfdi", "Retencion", null);
                            writer.WriteAttributeString("Impuesto", "002");
                            writer.WriteAttributeString("Importe", totalMovs.retencionIva6Total.ToString("0.00"));
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }

                    if (totalMovs.tieneTraslados)
                    {
                        writer.WriteStartElement("cfdi", "Traslados", null);
                        writer.WriteStartElement("cfdi", "Traslado", null);
                        writer.WriteAttributeString("Base", totalMovs.baseIva.ToString("0.00"));
                        writer.WriteAttributeString("Impuesto", "002");
                        writer.WriteAttributeString("TipoFactor", "Tasa");
                        writer.WriteAttributeString("TasaOCuota", "0.160000");
                        writer.WriteAttributeString("Importe", totalMovs.trasladadosTotal.ToString("0.00"));
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

            }

            var cadenaOriginal = getCadenaOriginal($"{_baseFile}{generalName}.xml");
            var selloDigital = getSelloDigital(company.PrivateKeyFile, company.PassPrivateKey, cadenaOriginal, generalName);

            if (selloDigital == null || selloDigital.Equals(""))
            {
                throw new ApiException($"Certificados no validos"); ;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load($"{_baseFile}{generalName}.xml");

            XmlNode node = xmlDocument.SelectSingleNode("/cfdi:Comprobante", GetXmlNamespaceManager(xmlDocument));

            node.Attributes["Sello"].Value = selloDigital;
            xmlDocument.Save($"{_baseFile}{generalName}_sellado.xml");

            StreamReader streamReader = new StreamReader($"{_baseFile}{generalName}_sellado.xml");
            string xml = "";
            while (!streamReader.EndOfStream)
            {
                xml += streamReader.ReadLine();
            }
            streamReader.Close();

            File.Delete($"{_baseFile}{generalName}_sellado.xml");
            File.Delete($"{_baseFile}{generalName}.xml");

            var plainTextBytes = Encoding.UTF8.GetBytes(xml);
            var xml_base64 = Convert.ToBase64String(plainTextBytes);

            string cfdiTimbrado = "";

            try
            {
                TimboxPruebas.timbrar_cfdi_result response = new TimboxPruebas.timbrar_cfdi_result();
                Timbox.timbrar_cfdi_result responseProd = new Timbox.timbrar_cfdi_result();

                if (_environmentService.getName().Equals("QA"))
                {
                    var cliente_timbrar = new TimboxPruebas.timbrado_cfdi40_portClient();

                    response = await cliente_timbrar.timbrar_cfdiAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", xml_base64);
                }

                if (_environmentService.getName().Equals("Production"))
                {
                    var cliente_timbrar = new Timbox.timbrado_cfdi40_portClient();
                    responseProd = await cliente_timbrar.timbrar_cfdiAsync("SIT160613TN0", "6_yXFnUeUcmix8-QCBJg", xml_base64);

                }

                cfdiTimbrado = @"C:\StaticFiles\Mate\Timbrado\" + company.Id;

                if (!Directory.Exists(cfdiTimbrado))
                {
                    Directory.CreateDirectory(cfdiTimbrado);
                }

                cfdiTimbrado = @"C:\StaticFiles\Mate\Timbrado\" + company.Id + @"\" + $"{generalName}.xml";

                using (StreamWriter writer = new StreamWriter(cfdiTimbrado))
                {
                    if (_environmentService.getName().Equals("QA"))
                    {
                        
                            writer.WriteLine(response.xml.ToString());
                        
                    }
                    if (_environmentService.getName().Equals("Production"))
                    {
                        
                            writer.WriteLine(responseProd.xml.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error en el proceso de timbrado: ${e.Message}");
            }

            xmlDocument.Load(cfdiTimbrado);
            XmlNode timbreFiscal = xmlDocument.SelectSingleNode("//tfd:TimbreFiscalDigital", GetXmlNamespaceManagerTimbrado(xmlDocument));

            string noCertificadoSAT = timbreFiscal.Attributes["NoCertificadoSAT"].Value;
            string uuid = timbreFiscal.Attributes["UUID"].Value;
            string selloSat = timbreFiscal.Attributes["SelloSAT"].Value;
            string fechaTimbrado = timbreFiscal.Attributes["FechaTimbrado"].Value;

            factura.CadenaOriginal = cadenaOriginal;
            factura.FechaTimbrado = DateTime.Parse(fechaTimbrado);
            factura.SelloCfdi = selloDigital;
            factura.SelloSat = selloSat;
            factura.Uuid = uuid;
            factura.Estatus = 2;
            factura.FileXmlTimbrado = @"\StaticFiles\Mate\Timbrado\" + company.Id + @"\" + $"{generalName}.xml";
            factura.NoCertificadoSat = noCertificadoSAT;

            factura.Folio = company.FolioFactura;

            await _repositoryAsyncFactura.UpdateAsync(factura);
            await _repositoryAsyncCompany.UpdateAsync(company);

            return new Response<bool>(true);

        }
        public async Task<Response<bool>> timbrarComplementoPago(int Id)
        {
            var complementoPago = await _repositoryAsyncComplementoPago.GetByIdAsync(Id);

            if (complementoPago == null)
            {
                throw new ApiException($"ComplementoPago no encontrado para Id ${Id}");
            }

            if (complementoPago.Estatus != 1)
            {
                throw new ApiException($"ComplementoPago no puede ser timbrado para Id ${Id}");
            }

            var facturasAsociadas = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(Id));

            if (facturasAsociadas.Count == 0)
            {
                throw new ApiException($"ComplementoPago no cuenta con facturas asociadas");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(complementoPago.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            if (company.Certificado == null || company.Certificado.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con certificados.");
            }

            if (company.PrivateKeyFile == null || company.PrivateKeyFile.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con llave privada");
            }

            if (company.PassPrivateKey == null || company.PassPrivateKey.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con constrasenia de llave privada");
            }

            TimeZoneInfo setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime fechaActual = TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);
            fechaActual = fechaActual.AddHours(-1);

            string generalName = complementoPago.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;
            string fechaActualString = fechaActual.ToString("yyyy-MM-ddThh:mm:ss");

            var certificadoPem = getCertificadoPem(generalName, company.Certificado);


            if (certificadoPem.Equals(""))
            {
                throw new ApiException($"Certificado invalido");
            }

            certificadoPem = certificadoPem.Replace("-----BEGIN CERTIFICATE-----", "");
            certificadoPem = certificadoPem.Replace("-----END CERTIFICATE-----", "");
            certificadoPem = certificadoPem.Replace("\n", "");

            var noCertificado = getNoCertificado(company.Certificado);

            using(XmlWriter writer = XmlWriter.Create($"{_baseFile}{generalName}.xml"))
            {
                var fp = await _repositoryAsyncFormaPago.GetByIdAsync(complementoPago.FormaPagoId);
                var tm = await _repositoryAsyncTipoMoneda.GetByIdAsync(complementoPago.TipoMonedaId);
                var rfe = await _repositoryAsyncRegimenFiscal.GetByIdAsync(complementoPago.EmisorRegimenFiscalId);
                var rfr = await _repositoryAsyncRegimenFiscal.GetByIdAsync(complementoPago.ReceptorRegimenFiscalId);

                if (company.FolioFactura == null)
                {
                    company.FolioFactura = 1;
                }
                else
                {
                    company.FolioFactura = company.FolioFactura + 1;
                }

                //escribiendo cabecera
                writer.WriteStartDocument();
                writer.WriteStartElement("cfdi", "Comprobante", "http://www.sat.gob.mx/cfd/4");
                writer.WriteAttributeString("xmlns", "cfdi", null, "http://www.sat.gob.mx/cfd/4");
                writer.WriteAttributeString("xmlns", "pago20", null, "http://www.sat.gob.mx/Pagos20");
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd "
                                                                        + "http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd");
                writer.WriteAttributeString("Version", "4.0");
                writer.WriteAttributeString("Serie", "A");
                writer.WriteAttributeString("Folio", company.FolioFactura.ToString().Trim());
                writer.WriteAttributeString("Fecha", fechaActualString);
                writer.WriteAttributeString("Sello", "");
                writer.WriteAttributeString("NoCertificado", noCertificado);
                writer.WriteAttributeString("Certificado", certificadoPem);
                writer.WriteAttributeString("SubTotal", "0");
                writer.WriteAttributeString("Total", "0");
                writer.WriteAttributeString("TipoDeComprobante", "P");
                writer.WriteAttributeString("LugarExpedicion", complementoPago.LugarExpedicion);
                writer.WriteAttributeString("Exportacion", "01");
                writer.WriteAttributeString("Moneda", "XXX");

                writer.WriteStartElement("cfdi", "CfdiRelacionados", null);
                writer.WriteAttributeString("TipoRelacion", "04");

                var total = 0.0;
                var iva16 = 0.0;
                foreach(var item in facturasAsociadas)
                {
                    if (item.iva == true)
                    {
                        var calculo = item.Monto * 0.16;
                        iva16 += Math.Round(calculo, 2);
                    }
                    
                    total += item.Monto;
                    var factura = await _repositoryAsyncFactura.GetByIdAsync(item.FacturaId);
                    writer.WriteStartElement("cfdi", "CfdiRelacionado", null);
                    writer.WriteAttributeString("UUID", factura.Uuid);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("cfdi", "Emisor",null);
                writer.WriteAttributeString("Rfc", complementoPago.EmisorRfc);
                writer.WriteAttributeString("Nombre", complementoPago.EmisorRazonSocial);
                writer.WriteAttributeString("RegimenFiscal", rfe.RegimenFiscalCve);
                writer.WriteEndElement();

                writer.WriteStartElement("cfdi", "Receptor", null);
                writer.WriteAttributeString("Rfc", complementoPago.ReceptorRfc);
                writer.WriteAttributeString("Nombre", complementoPago.ReceptorRazonSocial);
                writer.WriteAttributeString("RegimenFiscalReceptor", rfr.RegimenFiscalCve); // agregar columna
                writer.WriteAttributeString("DomicilioFiscalReceptor", complementoPago.ReceptorDomicilioFiscal); // agregar columna
                writer.WriteAttributeString("UsoCFDI", "CP01");
                writer.WriteEndElement();

                writer.WriteStartElement("cfdi", "Conceptos", null);
                writer.WriteStartElement("cfdi", "Concepto", null);
                writer.WriteAttributeString("ClaveProdServ", "84111506");
                writer.WriteAttributeString("ClaveUnidad", "ACT");
                writer.WriteAttributeString("Cantidad", "1");
                writer.WriteAttributeString("Descripcion", "Pago");
                writer.WriteAttributeString("ValorUnitario", "0");
                writer.WriteAttributeString("Importe", "0");
                writer.WriteAttributeString("ObjetoImp", "01");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("cfdi", "Complemento", null);
                writer.WriteStartElement("pago20", "Pagos", null);
                writer.WriteAttributeString("Version", "2.0");
                writer.WriteStartElement("pago20", "Totales", null);
                if (iva16 > 0)
                {
                    var totaliva = Math.Round(total, 2)  + Math.Round(iva16,2);
                    writer.WriteAttributeString("TotalTrasladosBaseIVA16", total.ToString("0.00"));
                    writer.WriteAttributeString("TotalTrasladosImpuestoIVA16", iva16.ToString("0.00"));
                    writer.WriteAttributeString("MontoTotalPagos", totaliva.ToString("0.00"));
                } else
                {
                    writer.WriteAttributeString("MontoTotalPagos", total.ToString("0.00"));
                }
                writer.WriteEndElement();

                foreach (var item in facturasAsociadas)
                {
                    writer.WriteStartElement("pago20", "Pago", null);
                    writer.WriteAttributeString("TipoCambioP", "1");
                    writer.WriteAttributeString("FechaPago", complementoPago.FechaPago.ToString("yyyy-MM-ddThh:mm:ss"));
                    writer.WriteAttributeString("FormaDePagoP", fp.FormaDePago);
                    writer.WriteAttributeString("MonedaP", tm.CodigoIso);
                    if (item.iva == true)
                    {
                        var calculo = item.Monto * 0.16;
                        var monto = Math.Round(calculo + item.Monto, 2);
                        writer.WriteAttributeString("Monto", monto.ToString("0.00"));
                    }else
                    {
                        writer.WriteAttributeString("Monto", item.Monto.ToString("0.00"));
                    }
                    


                    writer.WriteStartElement("pago20", "DoctoRelacionado", null);
                    var factura = await _repositoryAsyncFactura.GetByIdAsync(item.FacturaId);
                    writer.WriteAttributeString("IdDocumento", factura.Uuid);
                    writer.WriteAttributeString("MonedaDR", tm.CodigoIso);

                    var facturaMovs = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(factura.Id));
                    var totalesFactura = _totalesMovsService.getTotalesFormMovs(facturaMovs);
                    var facturasAsociadasByFactura = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByFacturaSpecification(factura.Id));
                   
                    double totalAsociados = 0.0;
                    foreach (var cpf in facturasAsociadasByFactura)
                    {
                        var complemento = await _repositoryAsyncComplementoPago.GetByIdAsync(cpf.ComplementoPagoId);
                        if (complemento.Estatus != 3)                            
                            totalAsociados += cpf.Monto;
                    }

                    var saldoAnterior = (double)totalesFactura.total - (totalAsociados) + item.Monto;
                    var saldoInsoluto = (double)totalesFactura.total - (totalAsociados);

                    writer.WriteAttributeString("NumParcialidad", facturasAsociadasByFactura.Count.ToString());
                    writer.WriteAttributeString("ImpSaldoAnt", saldoAnterior.ToString("0.00"));
                    writer.WriteAttributeString("ImpPagado", item.Monto.ToString("0.00"));
                    writer.WriteAttributeString("ImpSaldoInsoluto", saldoInsoluto.ToString("0.00"));

                    if (item.iva == true)
                    {
                        writer.WriteAttributeString("ObjetoImpDR", "02");
                    }
                    else
                    {
                        writer.WriteAttributeString("ObjetoImpDR", "01");
                    }
                    


                    writer.WriteAttributeString("EquivalenciaDR", "1");

                    if (item.iva == true)
                    {
                        var iva = item.Monto * 0.16;
                        //Iva 16
                        writer.WriteStartElement("pago20", "ImpuestosDR", null);
                        writer.WriteStartElement("pago20", "TrasladosDR", null);
                        writer.WriteStartElement("pago20", "TrasladoDR", null);
                        writer.WriteAttributeString("BaseDR", item.Monto.ToString("0.00"));
                        writer.WriteAttributeString("ImpuestoDR", "002");
                        writer.WriteAttributeString("TipoFactorDR", "Tasa");
                        writer.WriteAttributeString("TasaOCuotaDR", "0.160000");
                        writer.WriteAttributeString("ImporteDR", iva.ToString("0.00"));

                        //Termina iva16
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    //Iva16 ImpuestosP
                    if(iva16 > 0)
                    {
                        var iva = item.Monto * 0.16;
                        writer.WriteStartElement("pago20", "ImpuestosP", null);
                        writer.WriteStartElement("pago20", "TrasladosP", null);
                        writer.WriteStartElement("pago20", "TrasladoP", null);
                        writer.WriteAttributeString("BaseP", item.Monto.ToString("0.00"));
                        writer.WriteAttributeString("ImpuestoP", "002");
                        writer.WriteAttributeString("TipoFactorP", "Tasa");
                        writer.WriteAttributeString("TasaOCuotaP", "0.160000");
                        writer.WriteAttributeString("ImporteP", iva.ToString("0.00"));
                        


                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }






                    writer.WriteEndElement();
                }
                
                writer.WriteEndElement();
                writer.WriteEndElement();
                
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

            }

            var cadenaOriginal = getCadenaOriginal($"{_baseFile}{generalName}.xml");
            var selloDigital = getSelloDigital(company.PrivateKeyFile, company.PassPrivateKey, cadenaOriginal, generalName);

            if (selloDigital == null || selloDigital.Equals(""))
            {
                throw new ApiException($"Certificados no validos"); ;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load($"{_baseFile}{generalName}.xml");

            XmlNode node = xmlDocument.SelectSingleNode("/cfdi:Comprobante", GetXmlNamespaceManager(xmlDocument));

            node.Attributes["Sello"].Value = selloDigital;
            xmlDocument.Save($"{_baseFile}{generalName}_sellado.xml");

            StreamReader streamReader = new StreamReader($"{_baseFile}{generalName}_sellado.xml");
            string xml = "";
            while (!streamReader.EndOfStream)
            {
                xml += streamReader.ReadLine();
            }
            streamReader.Close();

            //File.Delete($"{_baseFile}{generalName}_sellado.xml");
            //File.Delete($"{_baseFile}{generalName}.xml");

            var plainTextBytes = Encoding.UTF8.GetBytes(xml);
            var xml_base64 = Convert.ToBase64String(plainTextBytes);

            string cfdiTimbrado = "";

            try
            {
                TimboxPruebas.timbrar_cfdi_result response = new TimboxPruebas.timbrar_cfdi_result();
                Timbox.timbrar_cfdi_result responseProd = new Timbox.timbrar_cfdi_result();

                if (_environmentService.getName().Equals("QA"))
                {
                    var cliente_timbrar = new TimboxPruebas.timbrado_cfdi40_portClient();

                    response = await cliente_timbrar.timbrar_cfdiAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", xml_base64);
                }

                if (_environmentService.getName().Equals("Production"))
                {
                    var cliente_timbrar = new Timbox.timbrado_cfdi40_portClient();
                    responseProd = await cliente_timbrar.timbrar_cfdiAsync("SIT160613TN0", "6_yXFnUeUcmix8-QCBJg", xml_base64);

                }

                cfdiTimbrado = @"C:\StaticFiles\Mate\Timbrado\" + company.Id;

                if (!Directory.Exists(cfdiTimbrado))
                {
                    Directory.CreateDirectory(cfdiTimbrado);
                }

                cfdiTimbrado = @"C:\StaticFiles\Mate\Timbrado\" + company.Id + @"\" + $"{generalName}.xml";

                using (StreamWriter writer = new StreamWriter(cfdiTimbrado))
                {
                    if (_environmentService.getName().Equals("QA"))
                    {

                        writer.WriteLine(response.xml.ToString());

                    }
                    if (_environmentService.getName().Equals("Production"))
                    {

                        writer.WriteLine(responseProd.xml.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error en el proceso de timbrado: ${e.Message}");
            }

            xmlDocument.Load(cfdiTimbrado);
            XmlNode timbreFiscal = xmlDocument.SelectSingleNode("//tfd:TimbreFiscalDigital",
                GetXmlNamespaceManagerTimbrado(xmlDocument));

            string noCertificadoSAT = timbreFiscal.Attributes["NoCertificadoSAT"].Value;
            string uuid = timbreFiscal.Attributes["UUID"].Value;
            string selloSat = timbreFiscal.Attributes["SelloSAT"].Value;
            string fechaTimbrado = timbreFiscal.Attributes["FechaTimbrado"].Value;

            complementoPago.CadenaOriginal = cadenaOriginal;
            complementoPago.FechaTimbrado = DateTime.Parse(fechaTimbrado);
            complementoPago.SelloCfdi = selloDigital;
            complementoPago.SelloSat = selloSat;
            complementoPago.Uuid = uuid;
            complementoPago.Estatus = 2;
            complementoPago.FileXmlTimbrado = @"\StaticFiles\Mate\Timbrado\" + company.Id + @"\" + $"{generalName}.xml";
            complementoPago.NoCertificadoSat = noCertificadoSAT;

            complementoPago.Folio = company.FolioFactura;

            await _repositoryAsyncComplementoPago.UpdateAsync(complementoPago);
            await _repositoryAsyncCompany.UpdateAsync(company);

            return new Response<bool>(true);

        }
        public async Task<Response<bool>> timbrarNomina(int Id)
        {
            //Variable que trae la nomina
            var nomina = await _repositoryAsyncNomina.GetByIdAsync(Id);

            if (nomina == null)
            {
                throw new ApiException($"Nomina no encontrada con Id ${Id}");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(nomina.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            if (company.Certificado == null || company.Certificado.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con certificados.");
            }

            if (company.PrivateKeyFile == null || company.PrivateKeyFile.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con llave privada");
            }

            if (company.PassPrivateKey == null || company.PassPrivateKey.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con constrasenia de llave privada");
            }

            //Empleado de la nomina
            var employee = await _repositoryAsyncEmployee.GetByIdAsync(nomina.EmployeeId);
            //Variable que contiene las percepciones de la nomina
            var nominaPercepciones = await _repositoryAsyncNominaPercepciones.ListAsync(new PercepcionesByNominaSpecification(nomina.Id));
            //Variable que contiene las deducciones de la nomina
            var nominaDeducciones = await _repositoryAsyncNominaDeducciones.ListAsync(new DeduccionesByNominaSpecification(nomina.Id));
            //Variable que contiene otros pagos
            var nominaOtrosPagos = await _repositoryAsyncNominaOtroPago.ListAsync(new OtrosPagosByNominaSpecification(nomina.Id));
            //Obtenemos el estado por el nombre del estado
            var estado = await _repositoryAsyncEstado.ListAsync(new GetEstadoByNombre(employee.Estado));

            string claveEstado = "";
            if (estado.Count > 0)
            {
                claveEstado = estado[0].Clave;
            }
            //Descuento
            var TotalDeducciones = 0.0;
            //Total Impuestos retenidos
            var retencionD = 0.0;
            //Total otras deducciones
            var deduccionD = 0.0;
            //variable para descuento wise
            var seguroretiro = 0.0;
            //variable para descuento de prestamo
            var descuentoprestamo = 0.0;
            //variable para descuento de ahorro voluntario
            var descuentoahorro = 0.0;

            foreach (var deduccion in nominaDeducciones)
            {
                TotalDeducciones += Math.Round(deduccion.Importe,2);
                if (deduccion.Clave == "002")
                {
                    retencionD += Math.Round(deduccion.Importe,2);
                }
                else
                {
                    deduccionD += Math.Round(deduccion.Importe,2);
                }

                //Seguro de retiro
                if (deduccion.Clave == "048")
                {
                    seguroretiro = Math.Round(deduccion.Importe, 2);
                }

                //Prestamos
                if (deduccion.Clave == "004")
                {
                    descuentoprestamo = Math.Round(deduccion.Importe, 2);
                }

                //Ahorro
                if (deduccion.Clave == "023")
                {
                    descuentoahorro = Math.Round(deduccion.Importe, 2);
                }
            }

            //Importe
            var TotalImporteGravado = 0.0;
            foreach (var gravado in nominaPercepciones)
            {
                TotalImporteGravado += Math.Round(gravado.ImporteGravado,2);
            }

            var TotalImporteExento = 0.0;
            foreach (var excento in nominaPercepciones)
            {
                TotalImporteExento += Math.Round(excento.ImporteExento, 2);
            }

            var TotalOtrosPagos = 0.0;
            /**   Aqui va la suma de otros pagos, falta el specification **/


            var subtotal = TotalImporteGravado + TotalImporteExento + TotalOtrosPagos;

            var total = subtotal - TotalDeducciones;

            TimeZoneInfo setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime fechaActual = TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);
            fechaActual = fechaActual.AddHours(-1);

            string generalName = nomina.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;
            string fechaActualString = fechaActual.ToString("yyyy-MM-ddThh:mm:ss");


            var certificadoPem = getCertificadoPem(generalName, company.Certificado);

            if (certificadoPem.Equals(""))
            {
                throw new ApiException($"Certificado invalido");
            }

            certificadoPem = certificadoPem.Replace("-----BEGIN CERTIFICATE-----", "");
            certificadoPem = certificadoPem.Replace("-----END CERTIFICATE-----", "");
            certificadoPem = certificadoPem.Replace("\n", "");

            var noCertificado = getNoCertificado(company.Certificado);

            using (XmlWriter writer = XmlWriter.Create($"{_baseFile}{generalName}.xml"))
            {
                var tm = await _repositoryAsyncTipoMoneda.GetByIdAsync(nomina.TipoMonedaId);
                var uc = await _repositoryAsyncUsoCfdi.GetByIdAsync(nomina.ReceptorUsoCfdiId);
                var rfe = await _repositoryAsyncRegimenFiscal.GetByIdAsync(nomina.EmisorRegimenFistalId);
                var rfr = await _repositoryAsyncRegimenFiscal.GetByIdAsync(nomina.ReceptorRegimenFiscalId);

                if (company.FolioNomina == null)
                {
                    company.FolioNomina = 1;
                }
                else
                {
                    company.FolioNomina = company.FolioNomina + 1;
                }

                //escribiendo cabecera
                writer.WriteStartDocument();
                writer.WriteStartElement("cfdi", "Comprobante", "http://www.sat.gob.mx/cfd/4");
                writer.WriteAttributeString("xmlns", "cfdi", null, "http://www.sat.gob.mx/cfd/4");
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns", "nomina12", null, "http://www.sat.gob.mx/nomina12");
                writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd");
                writer.WriteAttributeString("Version", "4.0");
                writer.WriteAttributeString("Serie", "N");
                writer.WriteAttributeString("Folio", company.FolioNomina.ToString().Trim());

                //aqui va las deducciones como descuento si es mayor a 0
                if (TotalDeducciones > 0)
                {
                    writer.WriteAttributeString("Descuento", TotalDeducciones.ToString("0.00"));
                }

                writer.WriteAttributeString("Fecha", fechaActualString);
                writer.WriteAttributeString("Sello", "");
                writer.WriteAttributeString("NoCertificado", noCertificado);
                writer.WriteAttributeString("Certificado", certificadoPem);
                writer.WriteAttributeString("SubTotal", subtotal.ToString("0.00"));
                writer.WriteAttributeString("Total", total.ToString("0.00"));
                writer.WriteAttributeString("TipoDeComprobante", "N");
                writer.WriteAttributeString("MetodoPago", "PUE");
                writer.WriteAttributeString("LugarExpedicion", nomina.LugarExpedicion);
                writer.WriteAttributeString("Exportacion", "01");
                writer.WriteAttributeString("TipoCambio", "1");
                writer.WriteAttributeString("Moneda", tm.CodigoIso);

                //Emisor(compania)
                writer.WriteStartElement("cfdi", "Emisor", null);
                writer.WriteAttributeString("Rfc", company.Rfc);
                writer.WriteAttributeString("Nombre", nomina.EmisorRazonSocial);
                writer.WriteAttributeString("RegimenFiscal", rfe.RegimenFiscalCve);
                writer.WriteEndElement();

                //Receptor(Empleado)
                writer.WriteStartElement("cfdi", "Receptor", null);
                writer.WriteAttributeString("Rfc", nomina.ReceptorRfc);
                writer.WriteAttributeString("Nombre", nomina.ReceptorRazonSocial);
                writer.WriteAttributeString("RegimenFiscalReceptor", rfr.RegimenFiscalCve);
                writer.WriteAttributeString("DomicilioFiscalReceptor", nomina.ReceptorDomicilioFiscal);
                writer.WriteAttributeString("UsoCFDI", uc.UsoDeCfdi);
                writer.WriteEndElement();

                //Conceptos
                writer.WriteStartElement("cfdi", "Conceptos", null);
                writer.WriteStartElement("cfdi", "Concepto", null);
                writer.WriteAttributeString("Cantidad", "1");
                writer.WriteAttributeString("ClaveProdServ", "84111505");
                writer.WriteAttributeString("ObjetoImp", "01");
                writer.WriteAttributeString("ClaveUnidad", "ACT");
                writer.WriteAttributeString("Descripcion", "Pago de nómina");
                if (TotalDeducciones > 0)
                {
                    writer.WriteAttributeString("Descuento", TotalDeducciones.ToString("0.00"));
                }
                writer.WriteAttributeString("Importe", subtotal.ToString("0.00"));
                writer.WriteAttributeString("ValorUnitario", subtotal.ToString("0.00"));
                writer.WriteEndElement();

                //cierra conceptos
                writer.WriteEndElement();

                //Complemento
                writer.WriteStartElement("cfdi", "Complemento", null);

                DateTime fechaDesde = nomina.Desde;
                string fechaDesdeString = fechaDesde.ToString("yyyy-MM-dd");

                DateTime fechaHasta = nomina.Hasta;
                string fechaHastaString = fechaHasta.ToString("yyyy-MM-dd");

                DateTime fechaPago = nomina.Hasta;
                if (fechaPago.DayOfWeek == DayOfWeek.Saturday || fechaPago.DayOfWeek == DayOfWeek.Sunday)
                {
                    do
                    {
                        fechaPago = fechaPago.AddDays(-1);
                    } while (fechaPago.DayOfWeek == DayOfWeek.Saturday || fechaPago.DayOfWeek == DayOfWeek.Sunday);
                }
                string fechaPagoString = fechaPago.ToString("yyyy-MM-dd");

                var totalPercepciones = TotalImporteGravado + TotalImporteExento;

                //Nomina
                writer.WriteStartElement("nomina12", "Nomina", null);
                writer.WriteAttributeString("Version", "1.2");
                writer.WriteAttributeString("FechaFinalPago", fechaHastaString);
                writer.WriteAttributeString("FechaInicialPago", fechaDesdeString);
                writer.WriteAttributeString("FechaPago", fechaPagoString);
                writer.WriteAttributeString("NumDiasPagados", nomina.DiasPago.ToString().Trim());
                writer.WriteAttributeString("TipoNomina", "O");

                if (TotalDeducciones > 0)
                {
                    writer.WriteAttributeString("TotalDeducciones", TotalDeducciones.ToString("0.00"));
                }
                else
                {
                    writer.WriteAttributeString("TotalDeducciones", "0.00");
                }

                writer.WriteAttributeString("TotalPercepciones", subtotal.ToString("0.00"));

                if (TotalOtrosPagos > 0)
                {
                    writer.WriteAttributeString("TotalOtrosPagos", TotalOtrosPagos.ToString("0.00"));
                }
                else
                {
                    writer.WriteAttributeString("TotalOtrosPagos", "0.00");
                }

                //Registro patronal
                writer.WriteStartElement("nomina12", "Emisor", null);
                writer.WriteAttributeString("RegistroPatronal", company.RegistroPatronal);
                writer.WriteEndElement();

                //Dias entre dos fechas
                DateTime fechaContrato = (DateTime)employee.FechaContrato;
                TimeSpan diferencia = fechaHasta - fechaContrato;
                int numeroDeDias = diferencia.Days;
                int semanas = numeroDeDias / 7;
                var antiguedad = "P" + semanas.ToString().Trim() + "W";

                var puesto = await _repositoryAsyncPuesto.GetByIdAsync(nomina.PuestoId);
                var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(puesto.DepartamentoId);

                DateTime fechaIngreso = employee.Ingreso;
                string fechaIngresoString = fechaIngreso.ToString("yyyy-MM-dd");

                var tipoperiodicidad = await _repositoryAsyncTipoPeriocidadPago.GetByIdAsync(nomina.TipoPeriocidadPagoId);
                var TipoRiesgoTrabajo = await _repositoryAsyncTipoRiesgoTrabajo.GetByIdAsync(nomina.TipoRiesgoTrabajoId);
                var tipoContrato = await _repositoryAsyncTipoContrato.GetByIdAsync(nomina.TipoContratoId);
                var tipoJornada = await _repositoryAsyncTipoJornada.GetByIdAsync(nomina.TipoJornadaId);
                var tipoRegimen = await _repositoryAsyncTipoRegimen.GetByIdAsync(nomina.TipoRegimenId);

                //Nomina receptor
                writer.WriteStartElement("nomina12", "Receptor", null);
                writer.WriteAttributeString("Antigüedad", antiguedad);
                writer.WriteAttributeString("ClaveEntFed", claveEstado);
                writer.WriteAttributeString("Curp", employee.Curp);
                writer.WriteAttributeString("Departamento", departamento.Clave);
                writer.WriteAttributeString("FechaInicioRelLaboral", fechaIngresoString);
                writer.WriteAttributeString("NumEmpleado", employee.NoEmpleado.ToString().Trim());
                writer.WriteAttributeString("NumSeguridadSocial", employee.Nss);
                writer.WriteAttributeString("PeriodicidadPago", tipoperiodicidad.Clave);
                writer.WriteAttributeString("Puesto", puesto.Clave);
                writer.WriteAttributeString("RiesgoPuesto", TipoRiesgoTrabajo.Clave);
                writer.WriteAttributeString("SalarioBaseCotApor", employee.SalarioDiarioIntegrado.ToString().Trim());
                writer.WriteAttributeString("SalarioDiarioIntegrado", employee.SalarioDiario.ToString().Trim());
                writer.WriteAttributeString("Sindicalizado", "No");
                writer.WriteAttributeString("TipoContrato", tipoContrato.Clave);
                writer.WriteAttributeString("TipoJornada", tipoJornada.Clave);
                writer.WriteAttributeString("TipoRegimen", tipoRegimen.Clave);
                writer.WriteEndElement();

                //Percepciones
                writer.WriteStartElement("nomina12", "Percepciones", null);
                writer.WriteAttributeString("TotalExento", TotalImporteExento.ToString("0.00").Trim());
                writer.WriteAttributeString("TotalGravado", TotalImporteGravado.ToString("0.00").Trim());
                writer.WriteAttributeString("TotalSueldos", totalPercepciones.ToString("0.00").Trim());

                //Movimientos percepciones
                foreach (var percepciones in nominaPercepciones)
                {
                    writer.WriteStartElement("nomina12", "Percepcion", null);
                    writer.WriteAttributeString("Clave", percepciones.Clave);
                    writer.WriteAttributeString("Concepto", percepciones.Concepto.Trim());
                    writer.WriteAttributeString("ImporteExento", percepciones.ImporteExento.ToString("0.00").Trim());
                    writer.WriteAttributeString("ImporteGravado", percepciones.ImporteGravado.ToString("0.00").Trim());
                    writer.WriteAttributeString("TipoPercepcion", percepciones.Tipo.Trim());
                    writer.WriteEndElement();
                }

                //Cierra percepciones
                writer.WriteEndElement();

                //Deducciones
                if(retencionD > 0 || deduccionD > 0)
                {
                    writer.WriteStartElement("nomina12", "Deducciones", null);
                    if (retencionD > 0)
                    {
                        writer.WriteAttributeString("TotalImpuestosRetenidos", retencionD.ToString("0.00").Trim());
                    }
                    if (deduccionD > 0)
                    {
                        writer.WriteAttributeString("TotalOtrasDeducciones", deduccionD.ToString("0.00").Trim());
                    }

                    foreach (var deduccion in nominaDeducciones)
                    {
                        writer.WriteStartElement("nomina12", "Deduccion", null);
                        writer.WriteAttributeString("Clave", deduccion.Clave);
                        writer.WriteAttributeString("Concepto", deduccion.Concepto.Trim());
                        writer.WriteAttributeString("Importe", deduccion.Importe.ToString("0.00").Trim());
                        writer.WriteAttributeString("TipoDeduccion", deduccion.Tipo);
                        writer.WriteEndElement();
                    }

                    //Cierra Deducciones
                    writer.WriteEndElement();

                }

                //Otos pagos
                writer.WriteStartElement("nomina12", "OtrosPagos", null);
                if (nominaOtrosPagos.Count() > 0)
                {
                    foreach (var otrosPagos in nominaOtrosPagos)
                    {
                        writer.WriteStartElement("nomina12", "OtroPago", null);
                        writer.WriteAttributeString("Clave", otrosPagos.Clave);
                        writer.WriteAttributeString("Concepto", otrosPagos.Concepto.Trim());
                        writer.WriteAttributeString("Importe", otrosPagos.Importe.ToString("0.00").Trim());
                        writer.WriteAttributeString("TipoOtroPago", otrosPagos.Tipo);


                        writer.WriteStartElement("nomina12", "SubsidioAlEmpleo", null);
                        writer.WriteAttributeString("SubsidioCausado", otrosPagos.Importe.ToString("0.00").Trim());
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    }
                }
                else
                {
                    writer.WriteStartElement("nomina12", "OtroPago", null);
                    writer.WriteAttributeString("Clave", "002");
                    writer.WriteAttributeString("Concepto", "Subsidio para el empleo (efectivamente entregado al trabajador)");
                    writer.WriteAttributeString("Importe", "0.00");
                    writer.WriteAttributeString("TipoOtroPago", "002");

                    writer.WriteStartElement("nomina12", "SubsidioAlEmpleo", null);
                    writer.WriteAttributeString("SubsidioCausado", "0.00");
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }

                //Cierra OtrosPagos
                writer.WriteEndElement();

                //Cierra Nomina
                writer.WriteEndElement();

                //Cierra complemento
                writer.WriteEndElement();

                //Cierra comprobante
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }

            var cadenaOriginal = getCadenaOriginal($"{_baseFile}{generalName}.xml");
            var selloDigital = getSelloDigital(company.PrivateKeyFile, company.PassPrivateKey, cadenaOriginal, generalName);

            if (selloDigital == null || selloDigital.Equals(""))
            {
                throw new ApiException($"Certificados no validos"); ;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load($"{_baseFile}{generalName}.xml");

            XmlNode node = xmlDocument.SelectSingleNode("/cfdi:Comprobante", GetXmlNamespaceManager(xmlDocument));

            node.Attributes["Sello"].Value = selloDigital;
            xmlDocument.Save($"{_baseFile}{generalName}_sellado.xml");

            StreamReader streamReader = new StreamReader($"{_baseFile}{generalName}_sellado.xml");
            string xml = "";
            while (!streamReader.EndOfStream)
            {
                xml += streamReader.ReadLine();
            }
            streamReader.Close();

            File.Delete($"{_baseFile}{generalName}_sellado.xml");
            File.Delete($"{_baseFile}{generalName}.xml");

            var plainTextBytes = Encoding.UTF8.GetBytes(xml);
            var xml_base64 = Convert.ToBase64String(plainTextBytes);

            string cfdiTimbrado = "";

            try
            {
                TimboxPruebas.timbrar_cfdi_result response = new TimboxPruebas.timbrar_cfdi_result();
                Timbox.timbrar_cfdi_result responseProd = new Timbox.timbrar_cfdi_result();

                if (_environmentService.getName().Equals("QA"))
                {
                    var cliente_timbrar = new TimboxPruebas.timbrado_cfdi40_portClient();

                    response = await cliente_timbrar.timbrar_cfdiAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", xml_base64);
                }

                if (_environmentService.getName().Equals("Production"))
                {
                    var cliente_timbrar = new Timbox.timbrado_cfdi40_portClient();
                    responseProd = await cliente_timbrar.timbrar_cfdiAsync("SIT160613TN0", "6_yXFnUeUcmix8-QCBJg", xml_base64);
                }

                cfdiTimbrado = @"C:\StaticFiles\Mate\Timbrado\" + company.Id;

                if (!Directory.Exists(cfdiTimbrado))
                {
                    Directory.CreateDirectory(cfdiTimbrado);
                }

                cfdiTimbrado = @"C:\StaticFiles\Mate\Timbrado\" + company.Id + @"\" + $"{generalName}.xml";

                using (StreamWriter writer = new StreamWriter(cfdiTimbrado))
                {
                    if (_environmentService.getName().Equals("QA"))
                    {
                        writer.WriteLine(response.xml.ToString());
                    }
                    if (_environmentService.getName().Equals("Production"))
                    {
                        writer.WriteLine(responseProd.xml.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error en el proceso de timbrado: ${e.Message}");
            }

            xmlDocument.Load(cfdiTimbrado);
            XmlNode timbreFiscal = xmlDocument.SelectSingleNode("//tfd:TimbreFiscalDigital", GetXmlNamespaceManagerTimbrado(xmlDocument));

            string noCertificadoSAT = timbreFiscal.Attributes["NoCertificadoSAT"].Value;
            string uuid = timbreFiscal.Attributes["UUID"].Value;
            string selloSat = timbreFiscal.Attributes["SelloSAT"].Value;
            string fechaTimbrado = timbreFiscal.Attributes["FechaTimbrado"].Value;

            nomina.CadenaOriginal = cadenaOriginal;
            nomina.FechaTimbrado = DateTime.Parse(fechaTimbrado);
            nomina.SelloCfdi = selloDigital;
            nomina.SelloSat = selloSat;
            nomina.Uuid = uuid;
            nomina.Estatus = 2;
            nomina.FileXmlTimbrado = @"\StaticFiles\Mate\Timbrado\" + company.Id + @"\" + $"{generalName}.xml";
            nomina.NoCertificadoSat = noCertificadoSAT;
            nomina.Folio = company.FolioNomina;

            var periodo = 0;
            if (nomina.Hasta.Day > 15)
            {
                periodo = nomina.Hasta.Year * 100 + nomina.Hasta.Month * 2;
            }
            else
                periodo = nomina.Hasta.Year * 100 + nomina.Hasta.Month * 2 - 1;


            //tipo nomina wise, se agrega movimiento a ahorro wise
            if (employee.TipoNomina == 2)
            {
                
                var wise = await _repositoryAsyncAhorroWise.ListAsync(new AhorroWiseByEmployeeIdSpecification(employee.Id));
                
                if (wise.Count == 0)
                {
                    //creamos registro de nomina wise para poder cargar movimientos
                    AhorroWise ahorro = new AhorroWise();
                    ahorro.EmployeeId = employee.Id;
                    ahorro.CompanyId = employee.CompanyId;
                    ahorro.FechaInicio = DateTime.Now;
                    ahorro.Estatus = Domain.Enums.EstatusOperacion.Activo;
                    
                    ahorro.PeriodoInicial = periodo;
                    var nuevowise = await _repositoryAsyncAhorroWise.AddAsync(ahorro);

                    //Creamos movimiento wise
                    MovimientoAhorroWise movimientoAhorroWise = new MovimientoAhorroWise();

                    movimientoAhorroWise.AhorroWiseId = nuevowise.Id;
                    movimientoAhorroWise.EmployeeId = employee.Id;
                    movimientoAhorroWise.CompanyId = employee.CompanyId;
                    movimientoAhorroWise.Rendimiento = 0;
                    movimientoAhorroWise.Periodo = periodo;
                    movimientoAhorroWise.EstadoTransaccion = Domain.Enums.EstadoTransaccion.Exitoso;
                    movimientoAhorroWise.Interes = 0;
                    movimientoAhorroWise.Monto = (float)seguroretiro;
                    movimientoAhorroWise.MovimientoId = 1;

                    var data = await _repositoryAsyncMovimientoAhorroWise.AddAsync(movimientoAhorroWise);

                } else
                {
                    MovimientoAhorroWise movimientoAhorroWise = new MovimientoAhorroWise();

                    movimientoAhorroWise.AhorroWiseId = wise[0].Id;
                    movimientoAhorroWise.EmployeeId = employee.Id;
                    movimientoAhorroWise.CompanyId = employee.CompanyId;
                    movimientoAhorroWise.Rendimiento = 0;
                    movimientoAhorroWise.Periodo = periodo;
                    movimientoAhorroWise.EstadoTransaccion = Domain.Enums.EstadoTransaccion.Exitoso;
                    movimientoAhorroWise.Interes = 0;
                    movimientoAhorroWise.Monto = (float)seguroretiro;

                    var list = await _repositoryAsyncMovimientoAhorroWise.ListAsync(new MovimientoAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdSpecification(employee.CompanyId, employee.Id, wise[0].Id));

                    // Calcular el siguiente MovimientoId
                    var ultimoMovimientoId = list.Any() ? list.Max(m => m.MovimientoId) : 0;
                    var siguienteMovimientoId = ultimoMovimientoId + 1;

                    movimientoAhorroWise.MovimientoId = siguienteMovimientoId;

                    var data = await _repositoryAsyncMovimientoAhorroWise.AddAsync(movimientoAhorroWise);

                    
                }
            }

            if (descuentoprestamo > 0)
            {
                var prestamoId = 0;
                var prestamoActivo = await _repositoryAsyncPrestamo.ListAsync(new PrestamoByEmployeeIdAndIsActivoSpecification(employee.Id));

                if (prestamoActivo.Count != 0)
                {
                    Prestamo prestamo = prestamoActivo[0];
                    prestamoId = prestamoActivo[0].Id;

                    var list = await _repositoryAsyncMovimientoPrestamo.ListAsync(new MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdSpecification(employee.CompanyId, employee.Id, prestamoId));
                    var ultimoMovimientoId = list.Any() ? list.Max(m => m.MovimientoId) : 0;
                    var siguienteMovimientoId = ultimoMovimientoId + 1;

                    MovimientoPrestamo movimientoprestamo = new MovimientoPrestamo();
                    movimientoprestamo.MovimientoId = siguienteMovimientoId;
                    movimientoprestamo.PrestamoId = prestamoId;
                    movimientoprestamo.EmployeeId = employee.Id;
                    movimientoprestamo.CompanyId = employee.CompanyId;
                    movimientoprestamo.Monto = (float)descuentoprestamo;
                    movimientoprestamo.Rendimiento = 0;
                    movimientoprestamo.Moratorio = 0;
                    movimientoprestamo.EstadoTransaccion = EstadoTransaccion.Exitoso;
                    movimientoprestamo.Periodo = periodo;

                    movimientoprestamo.Capital = (float)descuentoprestamo * ( prestamoActivo[0].Monto / prestamoActivo[0].Total );
                    movimientoprestamo.Interes = (float)descuentoprestamo * (prestamoActivo[0].Interes / prestamoActivo[0].Total);
                    movimientoprestamo.FondoGarantia = (float)descuentoprestamo * (prestamoActivo[0].FondoGarantia / prestamoActivo[0].Total);

                    var saldo = 0.0;
                    foreach (var movimiento in list)
                    {
                        saldo += movimiento.Monto;
                    }

                    saldo += descuentoprestamo;

                    if (prestamo.Total - saldo < 10)
                    {
                        prestamo.Estatus = EstatusOperacion.Finiquitado;
                    }

                    movimientoprestamo.SaldoActual = prestamo.Total - (float)saldo;

                    prestamo.MontoPagado = (float)saldo;
                    await _repositoryAsyncPrestamo.UpdateAsync(prestamo);
                    var data = await _repositoryAsyncMovimientoPrestamo.AddAsync(movimientoprestamo);

                }
            }


            if (descuentoahorro > 0)
            {
                var ahorroactivo = await _repositoryAsyncAhorroVoluntario.ListAsync(new AhorroVoluntarioByEmployeeIdAndIsActivoSpecification(nomina.EmployeeId));
                if (ahorroactivo.Count > 0)
                {
                    AhorroVoluntario ahorro = ahorroactivo[0];
                    var list = await _repositoryAsyncMovimientoAhorroVoluntario.ListAsync(new MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdSpecification(nomina.CompanyId, nomina.EmployeeId, ahorro.Id));
                    
                    // Calcular el siguiente MovimientoId
                    var ultimoMovimientoId = list.Any() ? list.Max(m => m.MovimientoId) : 0;
                    var siguienteMovimientoId = ultimoMovimientoId + 1;

                    MovimientoAhorroVoluntario movimientoahorro = new MovimientoAhorroVoluntario();
                    movimientoahorro.AhorroVoluntarioId = ahorro.Id;
                    movimientoahorro.EmployeeId = nomina.EmployeeId;
                    movimientoahorro.CompanyId = nomina.CompanyId;
                    movimientoahorro.MovimientoId = siguienteMovimientoId;
                    movimientoahorro.Periodo = periodo;
                    movimientoahorro.Monto = (float)descuentoahorro;
                    movimientoahorro.Rendimiento = 0;
                    movimientoahorro.EstadoTransaccion = EstadoTransaccion.Exitoso;
                    movimientoahorro.Interes = 0;
                    var data = await _repositoryAsyncMovimientoAhorroVoluntario.AddAsync(movimientoahorro);
                }
            }

            await _repositoryAsyncNomina.UpdateAsync(nomina);
            await _repositoryAsyncCompany.UpdateAsync(company);

            return new Response<bool>(true);
        }
        public async Task<Response<EstatusCancelacionDto>> cancelar(int Id)
        {
            var factura = await _repositoryAsyncFactura.GetByIdAsync(Id);

            if (factura == null)
            {
                throw new ApiException($"Factura no encontrado para Id ${Id}");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(factura.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            if (company.Certificado == null || company.Certificado.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con certificados.");
            }

            if (company.PrivateKeyFile == null || company.PrivateKeyFile.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con llave privada");
            }

            if (company.PassPrivateKey == null || company.PassPrivateKey.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con constrasenia de llave privada");
            }

            var facturaMovimientoList = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(Id));

            if (facturaMovimientoList.Count == 0)
            {
                throw new ApiException($"Factura no cuenta con movimientos");
            }

            var totalMovs = _totalesMovsService.getTotalesFormMovs(facturaMovimientoList);

            DateTime fechaActual = DateTime.Now;

            string generalName = factura.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;

            var cerPEM = getCertificadoPem(generalName, company.Certificado);
            var keyPEM = getKeyPem(generalName, company.PrivateKeyFile, company.PassPrivateKey);

            if (_environmentService.getName().Equals("QA"))
            {
                var folios = new TimboxCancelacionPruebas.folios();

                var temp_folios = new TimboxCancelacionPruebas.folio[1];

                var folio = new TimboxCancelacionPruebas.folio();

                folio.rfc_receptor = factura.ReceptorRfc;
                folio.total = totalMovs.total.ToString("0.00");
                folio.uuid = factura.Uuid;
                folio.motivo = "02";

                temp_folios.SetValue(folio, 0);

                folios.folio = temp_folios;

                var timbox_cliente = new TimboxCancelacionPruebas.cancelacion_portClient();
                TimboxCancelacionPruebas.cancelar_cfdi_result response = new TimboxCancelacionPruebas.cancelar_cfdi_result();

                response = await timbox_cliente.cancelar_cfdiAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", factura.EmisorRfc, folios, cerPEM, keyPEM);

                XmlDocument folios_cancelacion = new XmlDocument();
                folios_cancelacion.LoadXml(response.folios_cancelacion);

                XmlNode mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/mensaje");
                string mensaje = mensajeNode.InnerText;
                mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/codigo");
                string codigo = mensajeNode.InnerText;

                var estatusCancelacionDto = await consultarEstatus(factura.Id);

                estatusCancelacionDto.Data.Mensaje = mensaje;

                if (codigo.Equals("201") || codigo.Equals("202"))
                {
                    factura.Estatus = 3;
                    await _repositoryAsyncFactura.UpdateAsync(factura);
                }

                return estatusCancelacionDto;
            }
            else 
            {

                var folios = new TimboxCancelacion.folios();

                var temp_folios = new TimboxCancelacion.folio[1];

                var folio = new TimboxCancelacion.folio();

                folio.rfc_receptor = factura.ReceptorRfc;
                folio.total = totalMovs.total.ToString("0.00");
                folio.uuid = factura.Uuid;
                folio.motivo = "02";

                temp_folios.SetValue(folio, 0);

                folios.folio = temp_folios;

                var timbox_cliente = new TimboxCancelacion.cancelacion_portClient();
                TimboxCancelacion.cancelar_cfdi_result response = new TimboxCancelacion.cancelar_cfdi_result();

                response = await timbox_cliente.cancelar_cfdiAsync("SIT160613TN0", "6_yXFnUeUcmix8-QCBJg", factura.EmisorRfc, folios, cerPEM, keyPEM);

                XmlDocument folios_cancelacion = new XmlDocument();
                folios_cancelacion.LoadXml(response.folios_cancelacion);

                XmlNode mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/mensaje");
                string mensaje = mensajeNode.InnerText;
                mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/codigo");
                string codigo = mensajeNode.InnerText;

                var estatusCancelacionDto = await consultarEstatus(factura.Id);

                estatusCancelacionDto.Data.Mensaje = mensaje;

                if (codigo.Equals("201") || codigo.Equals("202"))
                {
                    factura.Estatus = 3;
                    factura.FechaCancelacion = DateTime.Now;
                    await _repositoryAsyncFactura.UpdateAsync(factura);
                }

                return estatusCancelacionDto;

            }

        }
        public async Task<Response<EstatusCancelacionDto>> consultarEstatus(int Id)
        {
            var factura = await _repositoryAsyncFactura.GetByIdAsync(Id);

            if (factura == null)
            {
                throw new ApiException($"Factura no encontrado para Id ${Id}");
            }

            var timbox_cliente = new TimboxCancelacionPruebas.cancelacion_portClient();
            TimboxCancelacionPruebas.consultar_estatus_result response = new TimboxCancelacionPruebas.consultar_estatus_result();

            var facturaMovimientoList = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(Id));

            if (facturaMovimientoList.Count == 0)
            {
                throw new ApiException($"Factura no cuenta con movimientos");
            }

            var totalMovs = _totalesMovsService.getTotalesFormMovs(facturaMovimientoList);

            var total = totalMovs.total.ToString("0.00");

            response = await timbox_cliente.consultar_estatusAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", factura.Uuid, factura.EmisorRfc, factura.ReceptorRfc, total);

            EstatusCancelacionDto estatusCancelacionDto = new EstatusCancelacionDto();

            estatusCancelacionDto.EstatusCancelacion = response.estatus_cancelacion;
            estatusCancelacionDto.EsCancelable = response.es_cancelable;
            estatusCancelacionDto.Estado = response.estado;
            estatusCancelacionDto.CodigoEstatus = response.codigo_estatus;

            return new Response<EstatusCancelacionDto>(estatusCancelacionDto);

        }
        public async Task<Response<EstatusCancelacionDto>> cancelarComplementoPago(int Id)
        {
            var complementoPago = await _repositoryAsyncComplementoPago.GetByIdAsync(Id);

            if (complementoPago == null)
            {
                throw new ApiException($"ComplementoPago no encontrado para Id ${Id}");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(complementoPago.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            if (company.Certificado == null || company.Certificado.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con certificados.");
            }

            if (company.PrivateKeyFile == null || company.PrivateKeyFile.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con llave privada");
            }

            if (company.PassPrivateKey == null || company.PassPrivateKey.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con constrasenia de llave privada");
            }

            var facturaAsociadasList = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(Id));

            if (facturaAsociadasList.Count == 0)
            {
                throw new ApiException($"ComplementoPago no cuenta con movimientos");
            }

            double total = 0.0;
            foreach(var temp in facturaAsociadasList)
            {
                total += temp.Monto;
            }

            DateTime fechaActual = DateTime.Now;

            string generalName = complementoPago.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;

            var cerPEM = getCertificadoPem(generalName, company.Certificado);
            var keyPEM = getKeyPem(generalName, company.PrivateKeyFile, company.PassPrivateKey);

            if (_environmentService.getName().Equals("QA"))
            {
                var folios = new TimboxCancelacionPruebas.folios();

                var temp_folios = new TimboxCancelacionPruebas.folio[1];

                var folio = new TimboxCancelacionPruebas.folio();

                folio.rfc_receptor = complementoPago.ReceptorRfc;
                folio.total = "0.00";
                folio.uuid = complementoPago.Uuid;
                folio.motivo = "02";

                temp_folios.SetValue(folio, 0);

                folios.folio = temp_folios;

                var timbox_cliente = new TimboxCancelacionPruebas.cancelacion_portClient();
                TimboxCancelacionPruebas.cancelar_cfdi_result response = new TimboxCancelacionPruebas.cancelar_cfdi_result();

                response = await timbox_cliente.cancelar_cfdiAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", complementoPago.EmisorRfc, folios, cerPEM, keyPEM);

                XmlDocument folios_cancelacion = new XmlDocument();
                folios_cancelacion.LoadXml(response.folios_cancelacion);

                XmlNode mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/mensaje");
                string mensaje = mensajeNode.InnerText;
                mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/codigo");
                string codigo = mensajeNode.InnerText;

                var estatusCancelacionDto = await consultarEstatusComplementoPago(complementoPago.Id);

                estatusCancelacionDto.Data.Mensaje = mensaje;

                if (codigo.Equals("201") || codigo.Equals("202"))
                {
                    complementoPago.Estatus = 3;
                    await _repositoryAsyncComplementoPago.UpdateAsync(complementoPago);
                }

                return estatusCancelacionDto;
            }
            else
            {

                var folios = new TimboxCancelacion.folios();

                var temp_folios = new TimboxCancelacion.folio[1];

                var folio = new TimboxCancelacion.folio();

                folio.rfc_receptor = complementoPago.ReceptorRfc;
                folio.total = "0.00";
                folio.uuid = complementoPago.Uuid;
                folio.motivo = "02";

                temp_folios.SetValue(folio, 0);

                folios.folio = temp_folios;

                var timbox_cliente = new TimboxCancelacion.cancelacion_portClient();
                TimboxCancelacion.cancelar_cfdi_result response = new TimboxCancelacion.cancelar_cfdi_result();

                response = await timbox_cliente.cancelar_cfdiAsync("SIT160613TN0", "6_yXFnUeUcmix8-QCBJg", complementoPago.EmisorRfc, folios, cerPEM, keyPEM);

                XmlDocument folios_cancelacion = new XmlDocument();
                folios_cancelacion.LoadXml(response.folios_cancelacion);

                XmlNode mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/mensaje");
                string mensaje = mensajeNode.InnerText;
                mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/codigo");
                string codigo = mensajeNode.InnerText;

                var estatusCancelacionDto = await consultarEstatusComplementoPago(complementoPago.Id);

                estatusCancelacionDto.Data.Mensaje = mensaje;

                if (codigo.Equals("201") || codigo.Equals("202"))
                {
                    complementoPago.Estatus = 3;
                    complementoPago.FechaCancelacion = DateTime.Now;
                    await _repositoryAsyncComplementoPago.UpdateAsync(complementoPago);
                }

                return estatusCancelacionDto;

            }
        }
        public async Task<Response<EstatusCancelacionDto>> consultarEstatusComplementoPago(int Id)
        {
            var complementoPago = await _repositoryAsyncComplementoPago.GetByIdAsync(Id);

            if (complementoPago == null)
            {
                throw new ApiException($"ComplementoPago  no encontrado para Id ${Id}");
            }

            var timbox_cliente = new TimboxCancelacionPruebas.cancelacion_portClient();
            TimboxCancelacionPruebas.consultar_estatus_result response = new TimboxCancelacionPruebas.consultar_estatus_result();

            var facturaAsociadasList = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(Id));

            if (facturaAsociadasList.Count == 0)
            {
                throw new ApiException($"ComplementoPago no cuenta con movimientos");
            }

            double total = 0.0;
            foreach (var temp in facturaAsociadasList)
            {
                total += temp.Monto;
            }

            var totalC = total.ToString("0.00");

            response = await timbox_cliente.consultar_estatusAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", complementoPago.Uuid, complementoPago.EmisorRfc, complementoPago.ReceptorRfc, totalC);

            EstatusCancelacionDto estatusCancelacionDto = new EstatusCancelacionDto();

            estatusCancelacionDto.EstatusCancelacion = response.estatus_cancelacion;
            estatusCancelacionDto.EsCancelable = response.es_cancelable;
            estatusCancelacionDto.Estado = response.estado;
            estatusCancelacionDto.CodigoEstatus = response.codigo_estatus;

            return new Response<EstatusCancelacionDto>(estatusCancelacionDto);
        }

        public async Task<Response<EstatusCancelacionDto>> cancelarNomina(int Id)
        {
            var nomina = await _repositoryAsyncNomina.GetByIdAsync(Id);

            if (nomina == null)
            {
                throw new ApiException($"Nomina no encontrada con Id ${Id}");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(nomina.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            if (company.Certificado == null || company.Certificado.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con certificados.");
            }

            if (company.PrivateKeyFile == null || company.PrivateKeyFile.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con llave privada");
            }

            if (company.PassPrivateKey == null || company.PassPrivateKey.Equals(""))
            {
                throw new ApiException($"Compania no cuenta con constrasenia de llave privada");
            }


            //Variable que contiene las percepciones de la nomina
            var nominaPercepciones = await _repositoryAsyncNominaPercepciones.ListAsync(new PercepcionesByNominaSpecification(nomina.Id));
            //Variable que contiene las deducciones de la nomina
            var nominaDeducciones = await _repositoryAsyncNominaDeducciones.ListAsync(new DeduccionesByNominaSpecification(nomina.Id));
            //Variable que contiene otros pagos
            var nominaOtrosPagos = await _repositoryAsyncNominaOtroPago.ListAsync(new OtrosPagosByNominaSpecification(nomina.Id));

            //Descuento
            var TotalDeducciones = 0.0;
            //Total Impuestos retenidos
            var retencionD = 0.0;
            //Total otras deducciones
            var deduccionD = 0.0;
            foreach (var deduccion in nominaDeducciones)
            {
                TotalDeducciones += Math.Round(deduccion.Importe, 2);
                if (deduccion.Clave == "002")
                {
                    retencionD += Math.Round(deduccion.Importe, 2);
                }
                else
                {
                    deduccionD += Math.Round(deduccion.Importe, 2);
                }
            }

            //Importe
            var TotalImporteGravado = 0.0;
            foreach (var gravado in nominaPercepciones)
            {
                TotalImporteGravado += Math.Round(gravado.ImporteGravado, 2);
            }

            var TotalImporteExento = 0.0;
            foreach (var excento in nominaPercepciones)
            {
                TotalImporteExento += Math.Round(excento.ImporteExento, 2);
            }

            var TotalOtrosPagos = 0.0;
            /**   Aqui va la suma de otros pagos, falta el specification **/

            var subtotal = TotalImporteGravado + TotalImporteExento + TotalOtrosPagos;

            var total = subtotal - TotalDeducciones;

            DateTime fechaActual = DateTime.Now;

            string generalName = nomina.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;

            var cerPEM = getCertificadoPem(generalName, company.Certificado);
            var keyPEM = getKeyPem(generalName, company.PrivateKeyFile, company.PassPrivateKey);

            if (_environmentService.getName().Equals("QA"))
            {
                var folios = new TimboxCancelacionPruebas.folios();

                var temp_folios = new TimboxCancelacionPruebas.folio[1];

                var folio = new TimboxCancelacionPruebas.folio();

                folio.rfc_receptor = nomina.ReceptorRfc;
                folio.total = total.ToString("0.00");
                folio.uuid = nomina.Uuid;
                folio.motivo = "02";

                temp_folios.SetValue(folio, 0);

                folios.folio = temp_folios;

                var timbox_cliente = new TimboxCancelacionPruebas.cancelacion_portClient();
                TimboxCancelacionPruebas.cancelar_cfdi_result response = new TimboxCancelacionPruebas.cancelar_cfdi_result();

                response = await timbox_cliente.cancelar_cfdiAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", company.Rfc, folios, cerPEM, keyPEM);

                XmlDocument folios_cancelacion = new XmlDocument();
                folios_cancelacion.LoadXml(response.folios_cancelacion);

                XmlNode mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/mensaje");
                string mensaje = mensajeNode.InnerText;
                mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/codigo");
                string codigo = mensajeNode.InnerText;

                var estatusCancelacionDto = await consultarEstatusNomina(nomina.Id);

                estatusCancelacionDto.Data.Mensaje = mensaje;

                if (codigo.Equals("201") || codigo.Equals("202"))
                {
                    nomina.Estatus = 3;
                    nomina.FechaCancelacion = DateTime.Now;
                    await _repositoryAsyncNomina.UpdateAsync(nomina);
                }

                return estatusCancelacionDto;
            }
            else
            {

                var folios = new TimboxCancelacion.folios();

                var temp_folios = new TimboxCancelacion.folio[1];

                var folio = new TimboxCancelacion.folio();

                folio.rfc_receptor = nomina.ReceptorRfc;
                folio.total = "0.00";
                folio.uuid = nomina.Uuid;
                folio.motivo = "02";

                temp_folios.SetValue(folio, 0);

                folios.folio = temp_folios;

                var timbox_cliente = new TimboxCancelacion.cancelacion_portClient();
                TimboxCancelacion.cancelar_cfdi_result response = new TimboxCancelacion.cancelar_cfdi_result();

                response = await timbox_cliente.cancelar_cfdiAsync("SIT160613TN0", "6_yXFnUeUcmix8-QCBJg", company.Rfc, folios, cerPEM, keyPEM);

                XmlDocument folios_cancelacion = new XmlDocument();
                folios_cancelacion.LoadXml(response.folios_cancelacion);

                XmlNode mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/mensaje");
                string mensaje = mensajeNode.InnerText;
                mensajeNode = folios_cancelacion.SelectSingleNode("/folios_cancelacion/folio_cancelacion/codigo");
                string codigo = mensajeNode.InnerText;

                var estatusCancelacionDto = await consultarEstatusNomina(nomina.Id);

                estatusCancelacionDto.Data.Mensaje = mensaje;

                if (codigo.Equals("201") || codigo.Equals("202"))
                {
                    nomina.Estatus = 3;
                    nomina.FechaCancelacion = DateTime.Now;
                    await _repositoryAsyncNomina.UpdateAsync(nomina);
                }

                return estatusCancelacionDto;

            }
        }

        public async Task<Response<EstatusCancelacionDto>> consultarEstatusNomina(int Id)
        {
            var nomina = await _repositoryAsyncNomina.GetByIdAsync(Id);

            if (nomina == null)
            {
                throw new ApiException($"Nomina no encontrado para Id ${Id}");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(nomina.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            var timbox_cliente = new TimboxCancelacionPruebas.cancelacion_portClient();
            TimboxCancelacionPruebas.consultar_estatus_result response = new TimboxCancelacionPruebas.consultar_estatus_result();

            

            //Variable que contiene las percepciones de la nomina
            var nominaPercepciones = await _repositoryAsyncNominaPercepciones.ListAsync(new PercepcionesByNominaSpecification(Id));
            //Variable que contiene las deducciones de la nomina
            var nominaDeducciones = await _repositoryAsyncNominaDeducciones.ListAsync(new DeduccionesByNominaSpecification(Id));
            //Variable que contiene otros pagos
            var nominaOtrosPagos = await _repositoryAsyncNominaOtroPago.ListAsync(new OtrosPagosByNominaSpecification(Id));

            //Descuento
            var TotalDeducciones = 0.0;
            //Total Impuestos retenidos
            var retencionD = 0.0;
            //Total otras deducciones
            var deduccionD = 0.0;
            foreach (var deduccion in nominaDeducciones)
            {
                TotalDeducciones += Math.Round(deduccion.Importe, 2);
                if (deduccion.Clave == "002")
                {
                    retencionD += Math.Round(deduccion.Importe, 2);
                }
                else
                {
                    deduccionD += Math.Round(deduccion.Importe, 2);
                }
            }

            //Importe
            var TotalImporteGravado = 0.0;
            foreach (var gravado in nominaPercepciones)
            {
                TotalImporteGravado += Math.Round(gravado.ImporteGravado, 2);
            }

            var TotalImporteExento = 0.0;
            foreach (var excento in nominaPercepciones)
            {
                TotalImporteExento += Math.Round(excento.ImporteExento, 2);
            }

            var TotalOtrosPagos = 0.0;
            /**   Aqui va la suma de otros pagos, falta el specification **/

            var subtotal = TotalImporteGravado + TotalImporteExento + TotalOtrosPagos;

            var total = subtotal - TotalDeducciones;

            var totalC = total.ToString("0.00");

            response = await timbox_cliente.consultar_estatusAsync("SIT160613TN0", "JX5xqU4UuJ9Yzi1W7Tb_", nomina.Uuid, company.Rfc, nomina.ReceptorRfc, totalC);

            EstatusCancelacionDto estatusCancelacionDto = new EstatusCancelacionDto();

            estatusCancelacionDto.EstatusCancelacion = response.estatus_cancelacion;
            estatusCancelacionDto.EsCancelable = response.es_cancelable;
            estatusCancelacionDto.Estado = response.estado;
            estatusCancelacionDto.CodigoEstatus = response.codigo_estatus;

            return new Response<EstatusCancelacionDto>(estatusCancelacionDto);
        }

        private string getSelloDigital(string privateKeyFile, string passPrivateKey, string cadenaOriginal, string generalName)
        {

            string cadenaOriginalTxt = @"C:\StaticFiles\Mate\cadena" + generalName + ".txt";
            File.WriteAllText(cadenaOriginalTxt, cadenaOriginal);

            string pem = @"C:\StaticFiles\Mate\pem" + generalName + ".pem";

            var arguments = @"pkcs8 -inform DET -in C:\" + privateKeyFile + " -passin pass:" + passPrivateKey + " -out " + pem;

            string outConsole = "";
            excutionProcess(arguments, out outConsole);

            string bin = @"C:\StaticFiles\Mate\sha" + generalName + ".bin";
            arguments = "dgst -sha256 -out " + bin + " -sign " + pem + " " + cadenaOriginalTxt;

            excutionProcess(arguments, out outConsole);

            string sello = @"C:\StaticFiles\Mate\sello" + generalName + ".sello";
            arguments = "enc -in " + bin + " -a -A -out " + sello;
            excutionProcess(arguments, out outConsole);

            StreamReader streamReader = new StreamReader(sello);
            var firma = streamReader.ReadLine();
            streamReader.Close();

            File.Delete(sello);
            File.Delete(pem);
            File.Delete(bin);
            File.Delete(cadenaOriginalTxt);

            return firma;

        }
        private string getCadenaOriginal(string fileXml)
        {
            var cadenaOriginal = "";

            XmlUrlResolver resolver = new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;
            resolver.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            XslCompiledTransform myXslTrans = new System.Xml.Xsl.XslCompiledTransform();
            myXslTrans.Load(_fileCadenaOriginal_4_0, new XsltSettings(), resolver);
            using (StringWriter sw = new StringWriter())
            using (XmlWriter xwo = XmlWriter.Create(sw, myXslTrans.OutputSettings))
            {
                myXslTrans.Transform(fileXml, xwo);
                cadenaOriginal = sw.ToString();
            }

            return cadenaOriginal;

        }
        private string getCertificadoPem(string generalName, string fileCertificado)
        {

            string fileOut = @"C:\StaticFiles\Mate\fileCerPem" + generalName + ".cer.pem";

            string arguments = @"x509 -inform der -in C:\" + fileCertificado + " -out " + fileOut;

            string textConsole;
            excutionProcess(arguments, out textConsole);

            StreamReader streamReader;

            try
            {
                streamReader = new StreamReader(fileOut);
            }
            catch
            {
                return "";
            }

            string txtCerPem = "";

            while (!streamReader.EndOfStream) txtCerPem += streamReader.ReadLine();

            streamReader.Close();

            File.Delete(fileOut);

            return txtCerPem;

        }
        private string getKeyPem(string generalName, string fileKey, string passKey)
        {

            string fileOut = @"C:\StaticFiles\Mate\fileKeyPem" + generalName + ".key.pem";

            string arguments = @"pkcs8 -inform DER -in C:\" + fileKey + " -passin pass:" + passKey + " -out " + fileOut;

            string textConsole;
            excutionProcess(arguments, out textConsole);

            StreamReader streamReader;

            try
            {
                streamReader = new StreamReader(fileOut);
            }
            catch
            {
                return "";
            }

            string txtKeyPem = "";

            while (!streamReader.EndOfStream) txtKeyPem += streamReader.ReadLine();

            streamReader.Close();

            File.Delete(fileOut);

            return txtKeyPem;

        } 
        public string getNoCertificado(string fileCertificado)
        {
            var arguments = @"x509 -inform DER -in C:\" + fileCertificado + " -noout -serial";

            string output;
            excutionProcess(arguments, out output);

            var serialTxt = output.Replace("serial=", "");
            serialTxt = serialTxt.Replace("\n", "");

            string noCertificado = "";

            for (int i = 0; i < serialTxt.Length; i++) if (i % 2 == 1) noCertificado += serialTxt[i];

            return noCertificado;

        }       
        private void excutionProcess(string arguments, out string textConsole)
        {
            Process process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            process.StartInfo.FileName = _fileOpenSsl;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            textConsole = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
        private static XmlNamespaceManager GetXmlNamespaceManager(XmlDocument xmlDoc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/4");
            return nsmgr;
        }
        private static XmlNamespaceManager GetXmlNamespaceManagerTimbrado(XmlDocument xmlDoc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
            return nsmgr;
        }
        public async Task<Response<string>> prueba()
        {
            return new Response<string>(_environmentService.getName()) ;
        }
    }
}
