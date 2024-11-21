using Application.DTOs.Administracion;
using Application.DTOs.Facturas;
using Application.Exceptions;
using Application.Feautres.Catalogos.Estados.GetEstadoByNombre;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Facturas;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Specifications.MiPortal.Prestamos;
using Application.Wrappers;
using Domain.Entities;
using Humanizer;
using iText.Html2pdf;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Application.Specifications.MiPortal.AhorrosWise;
using System.Data.Common;
using System.ComponentModel;



namespace Shared.Services
{
    public class PdfService : IPdfService
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

        private readonly ITotalesMovsService _totalesMovsService;
        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
        private readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsyncTipoPeriodicidadPago;
        private readonly IRepositoryAsync<TipoJornada> _repositoryAsyncTipoJornada;
        private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;
        private readonly IRepositoryAsync<NominaPercepcion> _repositoryAsyncNominaPercepciones;
        private readonly IRepositoryAsync<NominaDeduccion> _repositoryAsyncNominaDeducciones;

        private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;
        private readonly IRepositoryAsync<TipoIncidencia> _repositoryAsyncTipoIncidencias;
        private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;
        private readonly ITimboxService _timboxService;

        private readonly IRepositoryAsync<UnidadMedida> _repositoryAsyncUnidadMedida;
        private readonly IRepositoryAsync<CveProducto> _repositoryAsyncCveProducto;
        private readonly IAhorroWiseService _ahorroWiseService;
        private readonly IRepositoryAsync<Periodo> _repositoryAsyncPeriodo;
        private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
        private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
        private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsyncMovimientoAhorroVoluntario;
        private readonly IRepositoryAsync<AhorroWise> _repositoryAsyncAhorroWise;
        private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsyncMovimientoAhorroWise;
        private readonly IRepositoryAsync<MovimientoPrestamo> _repositoryAsyncMovimientoPrestamo;

        public PdfService(
            IRepositoryAsync<Factura> repositoryAsyncFactura,
            IRepositoryAsync<FacturaMovimiento> repositoryAsyncFacturaMovimiento,
            IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago,
            IRepositoryAsync<ComplementoPagoFactura> repositoryAsyncComplementoPagoFactura,
            IRepositoryAsync<Company> repositoryAsyncCompany,
            IRepositoryAsync<FormaPago> repositoryAsyncFormaPago,
            IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago,
            IRepositoryAsync<TipoComprobante> repositoryAsyncTipoComprobante,
            IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda,
            IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal,
            IRepositoryAsync<UsoCfdi> repositoryAsyncUsoCfdi,
            ITotalesMovsService totalesMovsService,
            IRepositoryAsync<Nomina> repositoryAsyncNomina,
            IRepositoryAsync<Employee> repositoryAsyncEmployee,
            IRepositoryAsync<TipoPeriocidadPago> repositoryAsyncTipoPeriodicidadPago,
            IRepositoryAsync<TipoJornada> repositoryAsyncTipoJornada,
            IRepositoryAsync<Banco> repositoryAsyncBanco,
            IRepositoryAsync<NominaPercepcion> repositoryAsyncNominaPercepciones,
            IRepositoryAsync<NominaDeduccion> repositoryAsyncNominaDeducciones,
            IRepositoryAsync<Incidencia> repositoryAsyncIncidencias,
            IRepositoryAsync<TipoIncidencia> repositoryAsyncTipoIncidencias,
            IRepositoryAsync<Estado> repositoryAsyncEstado,
            ITimboxService timboxService,
            IRepositoryAsync<UnidadMedida> repositoryAsyncUnidadMedida, 
            IRepositoryAsync<CveProducto> repositoryAsyncCveProducto,
            IAhorroWiseService ahorroWiseService,
            IRepositoryAsync<Periodo> repositoryAsyncPeriodo,
            IRepositoryAsync<Prestamo> repositoryAsyncPrestamo,
            IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario,
            IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsyncMovimientoAhorroVoluntario,
            IRepositoryAsync<AhorroWise> repositoryAsyncAhorroWise,
            IRepositoryAsync<MovimientoAhorroWise> repositoryAsyncMovimientoAhorroWise,
            IRepositoryAsync<MovimientoPrestamo> repositoryAsyncMovimientoPrestamo)
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
            _totalesMovsService = totalesMovsService;
            _repositoryAsyncNomina = repositoryAsyncNomina;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncTipoPeriodicidadPago = repositoryAsyncTipoPeriodicidadPago;
            _repositoryAsyncTipoJornada = repositoryAsyncTipoJornada;
            _repositoryAsyncBanco = repositoryAsyncBanco;
            _repositoryAsyncNominaPercepciones = repositoryAsyncNominaPercepciones;
            _repositoryAsyncNominaDeducciones = repositoryAsyncNominaDeducciones;
            _repositoryAsyncIncidencias = repositoryAsyncIncidencias;
            _repositoryAsyncTipoIncidencias = repositoryAsyncTipoIncidencias;
            _repositoryAsyncEstado = repositoryAsyncEstado;
            _timboxService = timboxService;


            _repositoryAsyncUnidadMedida = repositoryAsyncUnidadMedida;
            _repositoryAsyncCveProducto = repositoryAsyncCveProducto;
            _ahorroWiseService = ahorroWiseService;
            _repositoryAsyncPeriodo = repositoryAsyncPeriodo;
            _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
            _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
            _repositoryAsyncMovimientoAhorroVoluntario = repositoryAsyncMovimientoAhorroVoluntario;
            _repositoryAsyncAhorroWise = repositoryAsyncAhorroWise;
            _repositoryAsyncMovimientoAhorroWise = repositoryAsyncMovimientoAhorroWise;
            _repositoryAsyncMovimientoPrestamo = repositoryAsyncMovimientoPrestamo;
        }

        public async Task<Response<SourceFileDto>> PdfComplementoPago(int Id)
        {
            var complementoPago = await _repositoryAsyncComplementoPago.GetByIdAsync(Id);

            if (complementoPago == null)
            {
                throw new ApiException($"ComplementoPago no encontrado para Id {Id}");
            }

            var facturaAsociadas = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(Id));

            if (facturaAsociadas == null)
            {
                throw new ApiException($"Complemento pago ${Id} no cuenta con facturas asociadas");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(complementoPago.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania con Id {complementoPago.CompanyId} no encontrado");
            }

            string complentoHTML = File.ReadAllText(System.IO.Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\ComplementoPago\PlantillaComplementoPago.html")).ToString();

            if (complementoPago.Estatus == 3)
            {
                complentoHTML += "<div style=' position: abosolute; top: 250px; width:100%'><p style='font-size: 20px; color: red; text-align: center'>CANCELADO CANCELADO CANCELADO CANCELADO CANCELADO</p></div>";
            }

            var fp = await _repositoryAsyncFormaPago.GetByIdAsync(complementoPago.FormaPagoId);
            var tm = await _repositoryAsyncTipoMoneda.GetByIdAsync(complementoPago.TipoMonedaId);
            var rfe = await _repositoryAsyncRegimenFiscal.GetByIdAsync(complementoPago.EmisorRegimenFiscalId);

            complentoHTML = complentoHTML.Replace("@Logo", @"C:\" + complementoPago.LogoSrcCompany);
            //Datos emisor
            complentoHTML = complentoHTML.Replace("@RazonSocial", complementoPago.EmisorRazonSocial);
            complentoHTML = complentoHTML.Replace("@RFC", complementoPago.EmisorRfc);
            complentoHTML = complentoHTML.Replace("@TipoComprobante", "P - Pago");
            complentoHTML = complentoHTML.Replace("@LugarExpedicion", complementoPago.LugarExpedicion);
            complentoHTML = complentoHTML.Replace("@RegimenFiscal", rfe.RegimenFiscalCve + " - " + rfe.RegimenFiscalDesc);
            complentoHTML = complentoHTML.Replace("@FormaPago", fp.Descripcion);
            complentoHTML = complentoHTML.Replace("@Serie", "P");
            complentoHTML = complentoHTML.Replace("@Folio", complementoPago.Folio.ToString());
            complentoHTML = complentoHTML.Replace("@Fecha", complementoPago.Created?.ToString("dd-MM-yyyy hh:mm:ss"));

            //datos de receptor
            complentoHTML = complentoHTML.Replace("@ReceptorRazolSocial", complementoPago.ReceptorRazonSocial);
            complentoHTML = complentoHTML.Replace("@ReceptorRFC", complementoPago.ReceptorRfc);

            CultureInfo culture = null;

            if (tm.Culture.Equals("") || tm.Culture == null)
                culture = new CultureInfo("es-US");
            else
                culture = new CultureInfo(tm.Culture);

            var movimientos = "<tr><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td></tr>";
            movimientos += "<tr class='detalles'>";
            movimientos += "<td><p>1</p></td>";
            movimientos += "<td><p>ACT</p></td>";
            movimientos += "<td><p>84111506</p></td>";
            movimientos += "<td class='descripcion'><p>Pago</p></td>";

            var precioUnitario = 0.0;

            movimientos += "<td class='pu'><p>" + precioUnitario.ToString("C", culture) + "</p></td>";

            double importe = 0.0;

            movimientos += "<td class='importe'><p>" + importe.ToString("C", culture) + "</p></td>";
            movimientos += "</tr>";

            complentoHTML = complentoHTML.Replace("@Detalles", movimientos);

            var impuestos = "<tr>";
            impuestos += "<td><p><strong>FECHA DE PAGO:</strong></p></td>";
            impuestos += "<td><p>" + complementoPago.FechaPago.ToString("dd-MM-yyyy") + "</p></td>";
            impuestos += "</tr>";

            impuestos += "<tr>";
            impuestos += "<td><p><strong>FORMA DE PAGO:</strong></p></td>";
            impuestos += "<td><p>" + fp.Descripcion + "</p></td>";
            impuestos += "</tr>";

            impuestos += "<tr>";
            impuestos += "<td><p><strong>MONEDA</strong></p></td>";
            impuestos += "<td><p>" + tm.CodigoIso + "</p></td>";
            impuestos += "</tr>";

            string textQR = "https://cipe.mx";
            string fileQR = "http://chart.apis.google.com/chart?cht=qr&chs=400x400&chl=" + textQR;

            complentoHTML = complentoHTML.Replace("@QR", fileQR);

            complentoHTML = complentoHTML.Replace("@Impuestos", impuestos);

            var total = 0.0;
            var iva16 = 0.0;
            foreach (var item in facturaAsociadas)
            {
                if(item.iva == true)
                {
                    var calculo = item.Monto * 0.16;
                    iva16 += Math.Round(calculo,2);
                }

                total += item.Monto;
            }
            var totalmasiva = 0.0;
            if (iva16 > 0.0)
            {
                complentoHTML = complentoHTML.Replace("@TituloIva", "<td><p><strong>Iva</strong></p></td>");
                var totaliva = "<td><p>" + iva16.ToString("C", culture) + "</p></td>";
                complentoHTML = complentoHTML.Replace("@Iva", totaliva);
                totalmasiva = total + iva16;
                complentoHTML = complentoHTML.Replace("@Total", totalmasiva.ToString("C", culture));
            } else
            {
                complentoHTML = complentoHTML.Replace("@TituloIva", "");
                complentoHTML = complentoHTML.Replace("@Iva", "");
                complentoHTML = complentoHTML.Replace("@Total", total.ToString("C", culture));
            }


            string[] partes;
            if (iva16 > 0.0)
            {
                partes = totalmasiva.ToString("0.00").Split('.');
            } else
            {
                partes = total.ToString("0.00").Split('.');
            }
            int entero = int.Parse(partes[0]);
            int decimalNum = int.Parse(partes[1]);

            string enteroLetter = entero.ToWords(WordForm.Normal, GrammaticalGender.Masculine, new CultureInfo("es-Mx")).ToUpper()
                + " " + tm.Modena.ToUpper() + " " + decimalNum.ToString().ToUpper() + "/100 " + "M.N.";

            complentoHTML = complentoHTML.Replace("@Cantidad", enteroLetter);

            var complementos = "";
            var ivacomplementos = "";
            if (iva16 > 0) 
            {
                ivacomplementos = "<tr><td class='detalleTd'><table><tr><th class='tittle'><p>Base</p></th><th class='tittle'><p>Impuesto</p></th><th class='tittle'><p>Tipo Factor</p></th><th class='tittle'><p>Cuota</p></th><th class='tittle'><p>Importe</p></th></tr>";
            }

            foreach (var item in facturaAsociadas)
            {

                var factura = await _repositoryAsyncFactura.GetByIdAsync(item.FacturaId);

                complementos += "<tr><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td></tr>";
                complementos += "<tr class='detalles'>";
                complementos += "<td><p>" + factura.Uuid + "</p></td>";
                complementos += "<td><p>" + item.Folio + "</p></td>";
                complementos += "<td><p>" + tm.CodigoIso + "</p></td>";
                complementos += "<td><p>" + fp.Descripcion + "</p></td>";

                var movs = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(factura.Id));

                var totalesFactura = _totalesMovsService.getTotalesFormMovs(movs);

                var asociados = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByFacturaSpecification(item.FacturaId));

                double totalAsociados = 0.0;
                foreach (var cpf in asociados)
                {
                    var complemento = await _repositoryAsyncComplementoPago.GetByIdAsync(cpf.ComplementoPagoId);
                    if (complemento.Estatus != 3)
                        totalAsociados += cpf.Monto;
                }

                var saldoAnterior = (double)totalesFactura.total - (totalAsociados) + item.Monto;
                var saldoInsoluto = (double)totalesFactura.total - (totalAsociados);


                complementos += "<td><p>" + saldoAnterior.ToString("C", culture) + "</p></td>";
                complementos += "<td><p>" + item.Monto.ToString("C", culture) + "</p></td>";
                complementos += "<td><p>" + saldoInsoluto.ToString("C", culture) + "</p></td>";

                complementos += "</tr>";


                if (item.iva == true)
                {
                    var iva = item.Monto * 0.16;
                    var montoiva = Math.Round(item.Monto,2) + Math.Round(iva,2);
                    ivacomplementos += "<tr><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td></tr>";
                    ivacomplementos += "<tr class='detalles'>";
                    ivacomplementos += "<td><p>" + item.Monto.ToString("C", culture) + "</p></td>";
                    ivacomplementos += "<td><p>IVA</p></td>";
                    ivacomplementos += "<td><p>Tasa</p></td>";
                    ivacomplementos += "<td><p>0.160000</p></td>";
                    ivacomplementos += "<td><p>" + montoiva.ToString("C", culture) + "</p></td>";
                    ivacomplementos += "</tr>";
                }
            }

            if (iva16 > 0) ivacomplementos += "</table></td></tr>";

            complentoHTML = complentoHTML.Replace("@DetalleComplementos", complementos);

            if(iva16 > 0) complentoHTML = complentoHTML.Replace("@Detalleiva", ivacomplementos);

            complentoHTML = complentoHTML.Replace("@UUID", complementoPago.Uuid);
            complentoHTML = complentoHTML.Replace("@FechTimbrado", complementoPago.FechaTimbrado?.ToString("dd-MM-yyyy hh:mm:ss"));
            complentoHTML = complentoHTML.Replace("@SelloCFDI", complementoPago.SelloCfdi);
            complentoHTML = complentoHTML.Replace("@SelloSAT", complementoPago.SelloSat);
            complentoHTML = complentoHTML.Replace("@NoSerie", complementoPago.NoCertificadoSat);
            if (complementoPago.SelloSat != null)
                complentoHTML = complentoHTML.Replace("@NoSeriSAT", complementoPago.SelloSat.Substring(0, 25));

            var cadenaOriginalSat = "||1.0|" + complementoPago.Uuid + "|" + complementoPago.FechaTimbrado?.ToString("yyyy-MM-ddThh:mm:ssZ") + "|" + complementoPago.SelloCfdi + "|" + complementoPago.NoCertificadoSat + "||";

            complentoHTML = complentoHTML.Replace("@CadenaOriginal", cadenaOriginalSat);



            DateTime fechaActual = DateTime.Now;
            string generalName = complementoPago.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;


            //Buscar una mejor forma de crear archivos PDF
            var html = complentoHTML;

            string css = File.ReadAllText(System.IO.Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\ComplementoPago\PlantillaComplementoPago.css")).ToString();

            var _client = new HttpClient();
            Uri uri = new Uri(string.Format("https://www.maxal-cloud.com.mx/cidnia/api/Correo/html2pdf", string.Empty));

            CorreoDTO correoDTO = new CorreoDTO();
            correoDTO.correo = css;
            correoDTO.DescripcionCorreo = html;
            correoDTO.fileName = System.IO.Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\ComplementosPagoCreadas\", generalName) + ".pdf"; ;

            var _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            try
            {
                string json = JsonSerializer.Serialize<CorreoDTO>(correoDTO, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);

                Byte[] bytes = null;

                if (response.IsSuccessStatusCode)
                {
                    String pdf64 = await response.Content.ReadAsStringAsync();
                    bytes = Convert.FromBase64String(pdf64);
                }

                File.WriteAllBytes(@"C:\StaticFiles\Mate\FormatosPDF\ComplementosPagoCreadas\" + generalName + ".pdf", bytes);


            }
            catch (Exception ex)
            {
                throw new ApiException(@"\tERROR {0}" + ex.Message);
            }

            SourceFileDto sourceFileDto = new SourceFileDto();

            sourceFileDto.SourceFile = @$"StaticFiles\Mate\FormatosPDF\ComplementosPagoCreadas\{generalName}.pdf";

            return new Response<SourceFileDto>(sourceFileDto);


        }

        public async Task<Response<FacturaPdfDto>> PdfFactura(int Id)
        {

            var factura = await _repositoryAsyncFactura.GetByIdAsync(Id);

            if (factura == null)
            {
                throw new ApiException($"Factura no encontrado para Id ${Id}");
            }

            var facturaMovimientoList = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(Id));

            if (facturaMovimientoList.Count == 0)
            {
                throw new ApiException($"Factura ${Id} no cuenta con movimientos");
            }

            var company = await _repositoryAsyncCompany.GetByIdAsync(factura.CompanyId);

            if (company == null)
            {
                throw new ApiException($"Compania no existe");
            }

            string facturaHTML = File.ReadAllText(System.IO.Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Factura\PlantillaFactura.html")).ToString();

            if (factura.Estatus == 3)
            {
                facturaHTML += "<div style=' position: abosolute; top: 250px; width:100%'><p style='font-size: 20px; color: red; text-align: center'>CANCELADO CANCELADO CANCELADO CANCELADO CANCELADO</p></div>";
            }

            var fp = await _repositoryAsyncFormaPago.GetByIdAsync(factura.FormaPagoId);
            var mp = await _repositoryAsyncMetodoPago.GetByIdAsync(factura.MetodoPagoId);
            var tc = await _repositoryAsyncTipoComprobante.GetByIdAsync(factura.TipoComprobanteId);
            var tm = await _repositoryAsyncTipoMoneda.GetByIdAsync(factura.TipoMonedaId);
            var uc = await _repositoryAsyncUsoCfdi.GetByIdAsync(factura.UsoCfdiId);
            var rfe = await _repositoryAsyncRegimenFiscal.GetByIdAsync(factura.EmisorRegimenFiscalId);
            var rfr = await _repositoryAsyncRegimenFiscal.GetByIdAsync(factura.ReceptorRegimenFiscalId);

            facturaHTML = facturaHTML.Replace("@Logo", @"C:\" + factura.LogoSrcCompany);
            //Datos emisor
            facturaHTML = facturaHTML.Replace("@RazonSocial", factura.EmisorRazonSocial);
            facturaHTML = facturaHTML.Replace("@RFC", factura.EmisorRfc);
            facturaHTML = facturaHTML.Replace("@TipoComprobante", tc.Descripcion);
            facturaHTML = facturaHTML.Replace("@LugarExpedicion", factura.LugarExpedicion);
            facturaHTML = facturaHTML.Replace("@RegimenFiscal", rfe.RegimenFiscalCve + " - " + rfe.RegimenFiscalDesc);
            facturaHTML = facturaHTML.Replace("@MetodoPago", mp.Descripcion);
            facturaHTML = facturaHTML.Replace("@FormaPago", fp.Descripcion);
            facturaHTML = facturaHTML.Replace("@Serie", "A");
            facturaHTML = facturaHTML.Replace("@Folio", factura.Folio.ToString());
            facturaHTML = facturaHTML.Replace("@Fecha", factura.Created?.ToString("dd-MM-yyyy hh:mm:ss"));

            //datos de receptor
            facturaHTML = facturaHTML.Replace("@ReceptorRazolSocial", factura.ReceptorRazonSocial);
            facturaHTML = facturaHTML.Replace("@ReceptorRFC", factura.ReceptorRfc);
            facturaHTML = facturaHTML.Replace("@UsoCFDI", uc.Descripcion);

            CultureInfo culture = null;

            if (tm.Culture.Equals("") || tm.Culture == null)
                culture = new CultureInfo("es-US");
            else
                culture = new CultureInfo(tm.Culture);

            string movimientos = "";

            foreach (var fmTemp in facturaMovimientoList)
            {
                var um = await _repositoryAsyncUnidadMedida.GetByIdAsync(fmTemp.UnidadMedidaId);
                var cp = await _repositoryAsyncCveProducto.GetByIdAsync(fmTemp.CveProductoId);

                movimientos += "<tr><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td><td class='space-sm border-left'></td></tr>";
                movimientos += "<tr class='detalles'>";
                movimientos += "<td><p>" + fmTemp.Cantidad + "</p></td>";
                movimientos += "<td><p>" + um.UnidadDeMedida.Trim() + "</p></td>";
                movimientos += "<td><p>" + cp.Producto.Trim() + "</p></td>";
                movimientos += "<td class='descripcion'><p>" + fmTemp.Descripcion + "</p></td>";

                var precioUnitario = (double)fmTemp.PrecioUnitario;

                movimientos += "<td class='pu'><p>" + precioUnitario.ToString("C", culture) + "</p></td>";

                double importe = (double)fmTemp.PrecioUnitario * (double)fmTemp.Cantidad;


                movimientos += "<td class='importe'><p>" + importe.ToString("C", culture) + "</p></td>";
                movimientos += "</tr>";

            }

            facturaHTML = facturaHTML.Replace("@Detalles", movimientos);

            var totalMovs = _totalesMovsService.getTotalesFormMovs(facturaMovimientoList);

            string impuestos = "";

            if (totalMovs.descuentoTotal > 0)
            {
                impuestos += "<tr>";
                impuestos += "<td><p><strong>DESCUENTO:</strong></p></td>";
                impuestos += "<td><p>" + totalMovs.descuentoTotal.ToString("C", culture) + "</p></td>";
                impuestos += "</tr>";
            }

            if (totalMovs.tieneTraslados)
            {
                impuestos += "<tr>";
                impuestos += "<td><p><strong>IVA:</strong></p></td>";
                impuestos += "<td><p>" + totalMovs.trasladadosTotal.ToString("C", culture) + "</p></td>";
                impuestos += "</tr>";
            }

            if (totalMovs.tieneRetencionIva6)
            {
                impuestos += "<tr>";
                impuestos += "<td><p><strong>RETENCIÓN IVA 6:</strong></p></td>";
                impuestos += "<td><p>-" + totalMovs.retencionIva6Total.ToString("C", culture) + "</p></td>";
                impuestos += "</tr>";
            }

            if (totalMovs.tieneRetencionIva)
            {
                impuestos += "<tr>";
                impuestos += "<td><p><strong>RETENCIÓN IVA 10:</strong></p></td>";
                impuestos += "<td><p>-" + totalMovs.retencionIvaTotal.ToString("C", culture) + "</p></td>";
                impuestos += "</tr>";
            }

            if (totalMovs.tieneRetencionIsr)
            {
                impuestos += "<tr>";
                impuestos += "<td><p><strong>RETENCIÓN ISR:</strong></p></td>";
                impuestos += "<td><p>-" + totalMovs.retencionIsrTotal.ToString("C", culture) + "</p></td>";
                impuestos += "</tr>";
            }

            string textQR = "https://cipe.mx";
            string fileQR = "http://chart.apis.google.com/chart?cht=qr&chs=400x400&chl=" + textQR;

            facturaHTML = facturaHTML.Replace("@QR", fileQR);

            facturaHTML = facturaHTML.Replace("@Impuestos", impuestos);
            facturaHTML = facturaHTML.Replace("@Total", totalMovs.total.ToString("C", culture));

            string[] partes = totalMovs.total.ToString("0.00").Split('.');
            int entero = int.Parse(partes[0]);
            int decimalNum = int.Parse(partes[1]);

            string enteroLetter = entero.ToWords(WordForm.Normal, GrammaticalGender.Masculine, new CultureInfo("es-Mx")).ToUpper()
                + " " + tm.Modena.ToUpper() + " " + decimalNum.ToString().ToUpper() + "/100 " + "M.N.";

            facturaHTML = facturaHTML.Replace("@Cantidad", enteroLetter);
            facturaHTML = facturaHTML.Replace("@UUID", factura.Uuid);
            facturaHTML = facturaHTML.Replace("@FechTimbrado", factura.FechaTimbrado?.ToString("dd-MM-yyyy hh:mm:ss"));
            facturaHTML = facturaHTML.Replace("@SelloCFDI", factura.SelloCfdi);
            facturaHTML = facturaHTML.Replace("@SelloSAT", factura.SelloSat);
            facturaHTML = facturaHTML.Replace("@NoSerie", factura.NoCertificadoSat);
            if (factura.SelloSat != null)
                facturaHTML = facturaHTML.Replace("@NoSeriSAT", factura.SelloSat.Substring(0, 25));

            var cadenaOriginalSat = "||1.0|" + factura.Uuid + "|" + factura.FechaTimbrado?.ToString("yyyy-MM-ddThh:mm:ssZ") + "|" + factura.SelloCfdi + "|" + factura.NoCertificadoSat + "||";

            facturaHTML = facturaHTML.Replace("@CadenaOriginal", cadenaOriginalSat);


            DateTime fechaActual = DateTime.Now;
            string generalName = factura.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;


            //Buscar una mejor forma de crear archivos PDF
            var html = facturaHTML;

            string css = File.ReadAllText(System.IO.Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Factura\PlantillaFactura.css")).ToString();

            var _client = new HttpClient();
            Uri uri = new Uri(string.Format("https://www.maxal-cloud.com.mx/cidnia/api/Correo/html2pdf", string.Empty));

            CorreoDTO correoDTO = new CorreoDTO();
            correoDTO.correo = css;
            correoDTO.DescripcionCorreo = html;
            correoDTO.fileName = System.IO.Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\FacturasCreadas\", generalName) + ".pdf"; ;

            var _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            try
            {
                string json = JsonSerializer.Serialize<CorreoDTO>(correoDTO, _serializerOptions);
                Console.WriteLine(json);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);

                Byte[] bytes = null;

                if (response.IsSuccessStatusCode)
                {
                    String pdf64 = await response.Content.ReadAsStringAsync();
                    bytes = Convert.FromBase64String(pdf64);
                }

                File.WriteAllBytes(@"C:\StaticFiles\Mate\FormatosPDF\FacturasCreadas\" + generalName + ".pdf", bytes);


            }
            catch (Exception ex)
            {
                throw new ApiException(@"\tERROR {0}" + ex.Message);
            }

            FacturaPdfDto facturaPdfDto = new FacturaPdfDto();

            facturaPdfDto.Id = factura.Id;
            facturaPdfDto.SourcePdf = @$"StaticFiles\Mate\FormatosPDF\FacturasCreadas\{generalName}.pdf";

            return new Response<FacturaPdfDto>(facturaPdfDto);

        }

        public async Task<Response<NominaPdfDto>> PdfNomina(int Id)
        {
            var nomina = await _repositoryAsyncNomina.GetByIdAsync(Id);
            if (nomina == null)
            {
                throw new ApiException($"Nómina no encontrada para Id ${Id}");
            }

            string nominaHTML = File.ReadAllText(System.IO.Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Nomina\PlantillaPDFNomina.html")).ToString();

            //Obteniendo datos adicionales.

            var company = await _repositoryAsyncCompany.GetByIdAsync(nomina.CompanyId);
            var emisorRegimenFiscal = await _repositoryAsyncRegimenFiscal.GetByIdAsync(nomina.EmisorRegimenFistalId);
            var employee = await _repositoryAsyncEmployee.GetByIdAsync(nomina.EmployeeId);
            var tipoMoneda = await _repositoryAsyncTipoMoneda.GetByIdAsync(nomina.TipoMonedaId);
            var metodoPago = await _repositoryAsyncMetodoPago.GetByIdAsync(nomina.MetodoPagoId);
            var tipoPeriodicidadPago = await _repositoryAsyncTipoPeriodicidadPago.GetByIdAsync(nomina.TipoPeriocidadPagoId);
            var tipoJornada = await _repositoryAsyncTipoJornada.GetByIdAsync(nomina.TipoJornadaId);
            var banco = await _repositoryAsyncBanco.GetByIdAsync(employee.BancoId);
            var nominaPercepciones = await _repositoryAsyncNominaPercepciones.ListAsync(new PercepcionesByNominaSpecification(nomina.Id));
            var nominaDeducciones = await _repositoryAsyncNominaDeducciones.ListAsync(new DeduccionesByNominaSpecification(nomina.Id));
            var formaPago = await _repositoryAsyncFormaPago.GetByIdAsync(employee.FormaPagoId);
            var ClaveEntFed = _repositoryAsyncEstado.FirstOrDefaultAsync(new GetEstadoByNombre(employee.Estado));
            var NoCertificado = _timboxService.getNoCertificado(company.Certificado);
            var periodo = await _repositoryAsyncPeriodo.GetByIdAsync(nomina.PeriodoId);
            var prestamoActivo = await _repositoryAsyncPrestamo.ListAsync(new PrestamoByEmployeeIdAndIsActivoSpecification(employee.Id));
            var movimientosAhorroVoluntario = new List<MovimientoAhorroVoluntario>();
            var movimientosPrestamo = new List<MovimientoPrestamo>();
            Prestamo prestamo = null;
            if (prestamoActivo.Count != 0)
            {
                prestamo = prestamoActivo[0];
                movimientosPrestamo = await _repositoryAsyncMovimientoPrestamo.ListAsync(new MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdSpecification(company.Id, employee.Id, prestamo.Id));

            }
            var ahorroActivo = await _repositoryAsyncAhorroVoluntario.ListAsync(new AhorroVoluntarioByEmployeeIdAndIsActivoSpecification(employee.Id));
            AhorroVoluntario ahorro = null;
            if (ahorroActivo.Count != 0)
            {
                ahorro = ahorroActivo[0];
                movimientosAhorroVoluntario = await _repositoryAsyncMovimientoAhorroVoluntario.ListAsync(new MovimientoAhorroVoluntarioByEmployeeIdAndAhorroVoluntarioIdSpecification(employee.Id, ahorro.Id));
            }


            CultureInfo culture = null;

            if (tipoMoneda.Culture.Equals("") || tipoMoneda.Culture == null)
                culture = new CultureInfo("es-US");
            else
                culture = new CultureInfo(tipoMoneda.Culture);
            //Reemplazando datos.
            // Logo e información de la compañía.
            nominaHTML = nominaHTML.Replace("@Logo", @"C:\" + company.CompanyProfile);
            nominaHTML = nominaHTML.Replace("@RazonSocial", company.RazonSocial);
            nominaHTML = nominaHTML.Replace("@RFC", employee.Rfc);
            nominaHTML = nominaHTML.Replace("@RegistroPatronal", company.RegistroPatronal);
            nominaHTML = nominaHTML.Replace("@RegimenFiscal", emisorRegimenFiscal.RegimenFiscalCve + " - " + emisorRegimenFiscal.RegimenFiscalDesc);

            // Información del empleado
            nominaHTML = nominaHTML.Replace("@NombreEmpleado", employee.NombreCompleto());
            nominaHTML = nominaHTML.Replace("@NumeroEmpleado", employee.NoEmpleado.ToString());
            nominaHTML = nominaHTML.Replace("@NumeroSeguroSocial", employee.Nss);
            nominaHTML = nominaHTML.Replace("@CURP", employee.Curp);
            nominaHTML = nominaHTML.Replace("@RFC", employee.Rfc);
            nominaHTML = nominaHTML.Replace("@FechaIngreso", employee.Ingreso.ToString("dd/MM/yyyy"));
            nominaHTML = nominaHTML.Replace("@PeriodicidadPago", tipoPeriodicidadPago.Descripcion);
            nominaHTML = nominaHTML.Replace("@TipoJornada", tipoJornada.Descripcion);
            nominaHTML = nominaHTML.Replace("@SueldoDiario", ((double)nomina.SalarioDiario).ToString("N"));
            nominaHTML = nominaHTML.Replace("@PeriodoPago", nomina.Desde.ToString("dd/MM/yyyy") + " al " + nomina.Hasta.ToString("dd/MM/yyyy"));
            var totalAhorroWise = await _ahorroWiseService.CalcularTotalAhorroWise(employee.Id, periodo.Etapa);
            if(totalAhorroWise != 0.0)
            {
                string etiquetaSaldoActualWise = "<th>Saldo actual WISE<//th>";
                nominaHTML = nominaHTML.Replace("@EtiquetaSaldoActualWise", etiquetaSaldoActualWise);
                nominaHTML = nominaHTML.Replace("@SaldoActualWise", "<td>$" + totalAhorroWise.ToString("N") + "</td>");
            }
            else
            {
                nominaHTML = nominaHTML.Replace("@EtiquetaSaldoActualWise", "<th></th>");
                nominaHTML = nominaHTML.Replace("@SaldoActualWise", "<td></td>");
            }
            if (prestamo != null) {
                var saldoActualPrestamo = prestamo.Total;
                foreach (var movimiento in movimientosPrestamo)
                {
                    if (movimiento.Periodo <= periodo.Etapa)
                    {
                        saldoActualPrestamo -= movimiento.Monto;
                    }
                }
                string etiquetaSaldoActualPrestamo = "<th>Saldo actual préstamo<//th>";
                nominaHTML = nominaHTML.Replace("@EtiquetaSaldoActualPrestamo", etiquetaSaldoActualPrestamo);
                nominaHTML = nominaHTML.Replace("@SaldoActualPrestamo", "<td>$" + saldoActualPrestamo.ToString("N") + "</td>");
            }
            else
            {
                nominaHTML = nominaHTML.Replace("@EtiquetaSaldoActualPrestamo", "<th></th>");
                nominaHTML = nominaHTML.Replace("@SaldoActualPrestamo", "<td></td>");

            }
            if (ahorro != null && movimientosAhorroVoluntario.Count != 0)
            {
                var saldoActualAhorro = 0.0;
                foreach(var movimiento in movimientosAhorroVoluntario)
                {
                    if (movimiento.Periodo <= periodo.Etapa)
                    {
                        saldoActualAhorro += movimiento.Monto;
                    }
                }
                string etiquetaAhorroVoluntario = "<th>Ahorro voluntario<//th>";
                nominaHTML = nominaHTML.Replace("@EtiquetaAhorroVoluntario", etiquetaAhorroVoluntario);
                nominaHTML = nominaHTML.Replace("@AhorroVoluntario", "<td>$" + saldoActualAhorro.ToString("N") + "</td>");
            }
            else
            {
                nominaHTML = nominaHTML.Replace("@EtiquetaAhorroVoluntario", "<th></th>");
                nominaHTML = nominaHTML.Replace("@AhorroVoluntario", "<td></td>");
            }


            nominaHTML = nominaHTML.Replace("@DiasTrabajados", nomina.DiasPago.ToString());
            nominaHTML = nominaHTML.Replace("@CuentaBancaria", employee.NoCuenta);
            nominaHTML = nominaHTML.Replace("@Banco", banco.Descripcion);
            nominaHTML = nominaHTML.Replace("@ClaveEntFed", ClaveEntFed.Result.Clave);
            
            var creditosHTML = "";
            if(employee.CreditoFonacot != "" || employee.CreditoInfonavit != "")
            {
                creditosHTML += "<tr class=\"color-1\">";
                creditosHTML += "<th colspan = \"2\"> Crédito FONACOT</th>";
                creditosHTML += "<th colspan = \"2\"> Crédito INFONAVIT</th>";
                creditosHTML += "</tr>";
                creditosHTML += "<tr>";
                creditosHTML += "<td colspan = \"2\">" + employee.CreditoFonacot + "</td>";
                creditosHTML += "<td colspan=\"2\">" + employee.CreditoInfonavit + "</td>";
                creditosHTML += "</tr>";
                nominaHTML = nominaHTML.Replace("@Creditos", creditosHTML);
            }
            else
            {
                nominaHTML = nominaHTML.Replace("@Creditos", creditosHTML);
            }


            // Información de nómina
            nominaHTML = nominaHTML.Replace("@NumeroReciboNomina", nomina.Folio.ToString());
            nominaHTML = nominaHTML.Replace("@FechaEmision", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            nominaHTML = nominaHTML.Replace("@FolioFiscal", nomina.Uuid);
            nominaHTML = nominaHTML.Replace("@NumeroCertificadoDigital", NoCertificado);
            nominaHTML = nominaHTML.Replace("@NumeroCertificadoSAT", nomina.NoCertificadoSat);
            nominaHTML = nominaHTML.Replace("@FechaPago", nomina.Hasta.ToString("dd/MM/yyyy"));
            nominaHTML = nominaHTML.Replace("@FechaCertificacion", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            

            // Conceptos
            nominaHTML = nominaHTML.Replace("@Cantidad", "1");
            nominaHTML = nominaHTML.Replace("@Unidad", "ACT");
            nominaHTML = nominaHTML.Replace("@CveProducto", "84111505");
            nominaHTML = nominaHTML.Replace("@Descripcion", "Pago de nómina");
            var totalPercepciones = 0.0;
            var percepcionesHTML = "";
            // Percepciones
            foreach (var percepcion in nominaPercepciones)
            {
                totalPercepciones += percepcion.ImporteGravado + percepcion.ImporteExento;
                percepcionesHTML += "<tr>";
                percepcionesHTML += "<td class='w-100'>" + percepcion.Tipo + "</td>";
                percepcionesHTML += "<td class='align-left w-100'>" + percepcion.Clave + "</td>";
                percepcionesHTML += "<td class='align-left w-420'>" + percepcion.Concepto + "</td>";
                percepcionesHTML += "<td class='align-left w-150'>" + percepcion.ImporteGravado.ToString("C", culture) + "</td>";
                percepcionesHTML += "<td class='align-left w-150'>" + percepcion.ImporteExento.ToString("C", culture) + "</td>";
                percepcionesHTML += "</tr>";
            }
            nominaHTML = nominaHTML.Replace("@Percepciones", percepcionesHTML);

            //Calcular el valor unitario y el importe. Esto es parte de la sección 3.
            nominaHTML = nominaHTML.Replace("@ValorUnitario", totalPercepciones.ToString("C", culture));
            nominaHTML = nominaHTML.Replace("@ImporteConcepto", totalPercepciones.ToString("C", culture));

            var totalDeducciones = 0.0;
            var deduccionesHTML = "";
            // Deducciones
            foreach (var deduccion in nominaDeducciones)
            {
                totalDeducciones += deduccion.Importe;
                deduccionesHTML += "<tr>";
                deduccionesHTML += "<td class='w-90'>" + deduccion.Tipo + "</td>";
                deduccionesHTML += "<td class='align-left w-90'>" + deduccion.Clave + "</td>";
                deduccionesHTML += "<td class='align-left w-500'>" + deduccion.Concepto + "</td>";
                deduccionesHTML += "<td class='align-left w-145'>" + deduccion.Importe.ToString("C", culture) + "</td>";
                deduccionesHTML += "</tr>";
            }

            nominaHTML = nominaHTML.Replace("@Deducciones", deduccionesHTML);

            var total = totalPercepciones - totalDeducciones;
            // Importe con letra
            string[] partes = total.ToString("0.00").Split('.');
            int entero = int.Parse(partes[0]);
            int decimales = int.Parse(partes[1]);

            string importeConLetra = entero.ToWords(WordForm.Normal, GrammaticalGender.Masculine, new CultureInfo("es-Mx")).ToUpper()
                + " " + tipoMoneda.Modena.ToUpper() + " " + decimales.ToString().ToUpper() + "/100 " + "M.N.";

            nominaHTML = nominaHTML.Replace("@ImporteConLetra", importeConLetra);
            nominaHTML = nominaHTML.Replace("@FormaPago", formaPago.FormaDePago + " - " + formaPago.Descripcion);
            nominaHTML = nominaHTML.Replace("@MetodoPago", metodoPago.MetodoDePago + " - " + metodoPago.Descripcion);

            // Importe
            nominaHTML = nominaHTML.Replace("@Subtotal", totalPercepciones.ToString("C", culture));
            nominaHTML = nominaHTML.Replace("@ImporteDeducciones", totalDeducciones.ToString("C", culture));
            nominaHTML = nominaHTML.Replace("@Total", total.ToString("C", culture));

            //Observaciones
            nominaHTML = nominaHTML.Replace("@Observaciones", "");

            // Código QR
            string textQR = "https://www.maxal-cloud.com.mx/MateBackQA";
            string fileQR = "http://chart.apis.google.com/chart?cht=qr&chs=400x400&chl=" + textQR;
            nominaHTML = nominaHTML.Replace("@CodigoQR", fileQR);

            // Sello digital del CFDI, SAT y Cadena original.
            nominaHTML = nominaHTML.Replace("@SelloDigitalCFDI", nomina.SelloCfdi);
            nominaHTML = nominaHTML.Replace("@SelloDigitalSAT", nomina.SelloSat);
            var cadenaOriginal = "||1.0|" + nomina.Uuid + "|" + nomina.FechaTimbrado?.ToString("yyyy-MM-ddThh:mm:ssZ") + "|" + nomina.SelloCfdi + "|" + nomina.NoCertificadoSat + "||";
            nominaHTML = nominaHTML.Replace("@CadenaCertificacionSAT", cadenaOriginal);

            // Crear PDF

            DateTime fechaActual = DateTime.Now;
            string nombrePDF = nomina.Id + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;

            string stylesPath = System.IO.Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Nomina\PlantillaPDFNominaCSS.css").ToString();
            nominaHTML = nominaHTML.Replace("<head>", $"<head><title>Reporte de nómina.</title><link rel='stylesheet' type='text/css' href='{stylesPath}'>");
            var rutaCompleta = String.Format(System.IO.Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\PDFNominasCreadas", $"{nombrePDF}.pdf"));

            HtmlConverter.ConvertToPdf(nominaHTML, new FileStream(rutaCompleta, FileMode.Create));

            NominaPdfDto nominaPdfDto = new NominaPdfDto();
            nominaPdfDto.Id = nomina.Id;
            nominaPdfDto.SourcePdf = System.IO.Path.Combine("StaticFiles", "Mate", "FormatosPDF", "PDFNominasCreadas", $"{nombrePDF}.pdf");

            return new Response<NominaPdfDto>(nominaPdfDto);
        }

        public async Task<string> PdfVacaciones(int Id, int aniosTrabajados, int diasVacaciones)
        {
            var incidencia = await _repositoryAsyncIncidencias.GetByIdAsync(Id);
            if (incidencia == null)
            {
                throw new KeyNotFoundException($"No se encontró la incidencia con id {Id}");
            }
            else
            {
                var employee = await _repositoryAsyncEmployee.GetByIdAsync(incidencia.EmpleadoId);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"No se encontró el empleado con id {incidencia.EmpleadoId}");
                }
                else
                {
                    DateOnly fechaInicio = DateOnly.FromDateTime(incidencia.FechaInicio);
                    DateOnly fechaFin = DateOnly.FromDateTime((DateTime)incidencia.FechaFin);
                    string plantillaHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Incidencia\PlantillaPDFIncidenciaVacaciones.html")).ToString();

                    plantillaHTML = plantillaHTML.Replace("#dias#", incidencia.Dias.ToString());
                    plantillaHTML = plantillaHTML.Replace("#añosAntiguedad#",aniosTrabajados.ToString());
                    plantillaHTML = plantillaHTML.Replace("#fechaInicio#", fechaInicio.ToString());
                    plantillaHTML = plantillaHTML.Replace("#fechaFin#", fechaFin.ToString());
                    plantillaHTML = plantillaHTML.Replace("#diasPendientes#",(diasVacaciones - incidencia.Dias).ToString());

                    DateOnly fechaReintegro;

                    switch ((int)fechaFin.DayOfWeek)
                    {
                        // Viernes
                        case 5:
                            fechaReintegro = fechaFin.AddDays(3);
                            break;
                        // Sabado
                        case 6:
                            fechaReintegro = fechaFin.AddDays(2);
                            break;
                        // Todos los demás dias
                        default:
                            fechaReintegro = fechaFin.AddDays(1);
                            break;
                    }

                    plantillaHTML = plantillaHTML.Replace("#Tipo#", "Vacaciones");

                    plantillaHTML = plantillaHTML.Replace("#fechaReintegro#", fechaReintegro.ToString());

                    plantillaHTML = plantillaHTML.Replace("#empleadoNombre#", employee.NombreCompleto());

                    var jefe = await _repositoryAsyncEmployee.GetByIdAsync((int)employee.JefeId);
                    if (jefe == null)
                    {
                        plantillaHTML = plantillaHTML.Replace("#jefeNombre#", "Nombre del jefe");
                    }
                    else
                    {
                        plantillaHTML = plantillaHTML.Replace("#jefeNombre#", jefe.NombreCompleto());
                    }

                    // Creando el PDF
                    string nombrePDF = "Incidencia" + incidencia.Id;

                    string stylesPath = Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Incidencia\PlantillaPDFIncidenciasCSS.css").ToString();
                    plantillaHTML = plantillaHTML.Replace("<head>", $"<head><title>Plantilla para el PDF de vacaciones.</title><link rel='stylesheet' type='text/css' href='{stylesPath}'>");
                    var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\IncidenciasCreadas", $"{nombrePDF}.pdf");

                    HtmlConverter.ConvertToPdf(plantillaHTML, new FileStream(rutaCompleta, FileMode.Create));

                    return rutaCompleta.Split(@"C:\").Last();
                    
                }
            }

        }

        /*
        public async Task<string> PdfIncidencia(int Id)
        {
            var incidencia = await _repositoryAsyncIncidencias.GetByIdAsync(Id);
            if (incidencia == null)
            {
                throw new KeyNotFoundException($"No se encontró la incidencia con id {Id}");
            }
            else
            {
                var employee = await _repositoryAsyncEmployee.GetByIdAsync(incidencia.EmpleadoId);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"No se encontró el empleado con id {incidencia.EmpleadoId}");
                }
                else
                {
                    DateOnly fechaInicio = DateOnly.FromDateTime(incidencia.FechaInicio);
                    DateOnly fechaFin = DateOnly.FromDateTime((DateTime)incidencia.FechaFin);
                    string plantillaHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Incidencia\PlantillaPDFIncidencia.html")).ToString();
                    var listaTiposIncidencia = await _repositoryAsyncTipoIncidencias.ListAsync();
                    Dictionary<int, string> diccionarioTiposIncidencia = listaTiposIncidencia.ToDictionary(x => x.Id, x => x.Descripcion);
                    string intencion = "";
                    string tipo = diccionarioTiposIncidencia[incidencia.TipoId];
                    switch (incidencia.TipoId)
                    {

                        // Dia administrativo
                        case 2:
                            break;
                        // Inasistencia por enfermedad
                        case 3:
                            intencion = $"comunico que el día {fechaInicio}, no me fue posible asistir a mis labores debido a: Inasistencia por enfermedad. Anexo copia del justificante médico.";
                            break;
                        // Incapacidad
                        case 4:
                            intencion = $"comunico mi incapacidad por: Incapacidad, la cual será efectiva a partir del día: {fechaInicio}. Anexo copia de mi incapacidad otorgada por mi Institución de Seguro Social.";
                            break;
                        // Permiso con goce de sueldo
                        case 5:
                            intencion = $"solicito permiso para ausentarme de mis labores el día {fechaInicio}, esto por Permiso con goce de sueldo.";
                            break;
                        // Permiso sin goce de sueldo
                        case 6:
                            intencion = $"me dirijo con la intención de justificar mi ausencia laboral del pasado día {fechaInicio}. Quedo en el entendido que se me descontará el día y aprovecho para hacerle llegar mis más sinceras disculpas ante la problemática que mi ausencia haya podido ocasionar.";
                            break;
                        // Permiso para salir temprano
                        case 7:
                            intencion = $"solicito su autorización para salir antes de mi hora habitual el día {fechaInicio}.";
                            break;
                        // Permiso para llegar tarde
                        case 8:
                            intencion = $"solicito su autorización para llegar tarde el día {fechaInicio}.";
                            break;
                        // Incapacidad por paternidad
                        case 9:
                            intencion = $"comunico mi incapacidad por: Incapacidad por paternidad, la cual será efectiva a partir del día: {fechaInicio}. Anexo copia de mi incapacidad otorgada por mi Institución de Seguro Social.";
                            break;
                        // Incapacidad por maternidad
                        case 10:
                            intencion = $"comunico mi incapacidad por: Incapacidad por maternidad, la cual será efectiva a partir del día: {fechaInicio}. Anexo copia de mi incapacidad otorgada por mi Institución de Seguro Social.";
                            break;
                        default:
                            break;
                    }

                    plantillaHTML = plantillaHTML.Replace("#Tipo#", tipo);
                    plantillaHTML = plantillaHTML.Replace("#intencion#", intencion);
                    plantillaHTML = plantillaHTML.Replace("#fechaInicio#", fechaInicio.ToString());
                   
                    plantillaHTML = plantillaHTML.Replace("#empleadoNombre#", employee.NombreCompleto());

                    if (employee.JefeId == null)
                    {
                        plantillaHTML = plantillaHTML.Replace("#jefeNombre#", "Nombre del jefe");
                    }
                    else
                    {
                        var jefe = await _repositoryAsyncEmployee.GetByIdAsync((int)employee.JefeId);
                        if (jefe == null)
                        {
                            plantillaHTML = plantillaHTML.Replace("#jefeNombre#", "Nombre del jefe");
                        }
                        else
                        {
                            plantillaHTML = plantillaHTML.Replace("#jefeNombre#", jefe.NombreCompleto());
                        }
                        
                    }

                    // Creando el PDF
                    string nombrePDF = "Incidencia" + incidencia.Id;

                    string stylesPath = Path.Combine(@"C:\StaticFiles", @"Mate\FormatosPDF\Incidencia\PlantillaPDFIncidenciasCSS.css").ToString();
                    plantillaHTML = plantillaHTML.Replace("<head>", $"<head><title>Plantilla para el PDF de Incidencias.</title><link rel='stylesheet' type='text/css' href='{stylesPath}'>");
                    var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\IncidenciasCreadas", $"{nombrePDF}.pdf");

                    HtmlConverter.ConvertToPdf(plantillaHTML, new FileStream(rutaCompleta, FileMode.Create));

                    return rutaCompleta.Split(@"C:\").Last();

                }
            }

        }
        */

        
        public async Task<string> PdfIncidencia(int Id)
        {
            var incidencia = await _repositoryAsyncIncidencias.GetByIdAsync(Id);
            if (incidencia == null)
            {
                throw new KeyNotFoundException($"No se encontró la incidencia con id {Id}");
            }
            else
            {
                var employee = await _repositoryAsyncEmployee.GetByIdAsync(incidencia.EmpleadoId);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"No se encontró el empleado con id {incidencia.EmpleadoId}");
                }
                else
                {
                    DateOnly fechaInicio = DateOnly.FromDateTime(incidencia.FechaInicio);
                    DateOnly fechaFin = DateOnly.FromDateTime((DateTime)incidencia.FechaFin);

                    var listaTiposIncidencia = await _repositoryAsyncTipoIncidencias.ListAsync();
                    Dictionary<int, string> diccionarioTiposIncidencia = listaTiposIncidencia.ToDictionary(x => x.Id, x => x.Descripcion);
                    string intencion = "";
                    string tipo = diccionarioTiposIncidencia[incidencia.TipoId];
                    int jefeid = 0;
                    if (employee.JefeId != null)
                    {
                        jefeid = (int)employee.JefeId;
                    } else
                        jefeid = 17;

                    var jefe = await _repositoryAsyncEmployee.GetByIdAsync(jefeid);
                    switch (incidencia.TipoId)
                    {

                        // Dia administrativo
                        case 2:
                            break;
                        // Inasistencia por enfermedad
                        case 3:
                            intencion = $"comunico que el día {fechaInicio}, no me fue posible asistir a mis labores debido a: Inasistencia por enfermedad. Anexo copia del justificante médico.";
                            break;
                        // Incapacidad
                        case 4:
                            intencion = $"comunico mi incapacidad por: Incapacidad, la cual será efectiva a partir del día: {fechaInicio}. Anexo copia de mi incapacidad otorgada por mi Institución de Seguro Social.";
                            break;
                        // Permiso con goce de sueldo
                        case 5:
                            intencion = $"solicito permiso para ausentarme de mis labores el día {fechaInicio}, esto por Permiso con goce de sueldo.";
                            break;
                        // Permiso sin goce de sueldo
                        case 6:
                            intencion = $"me dirijo con la intención de justificar mi ausencia laboral del pasado día {fechaInicio}. Quedo en el entendido que se me descontará el día y aprovecho para hacerle llegar mis más sinceras disculpas ante la problemática que mi ausencia haya podido ocasionar.";
                            break;
                        // Permiso para salir temprano
                        case 7:
                            intencion = $"solicito su autorización para salir antes de mi hora habitual el día {fechaInicio}.";
                            break;
                        // Permiso para llegar tarde
                        case 8:
                            intencion = $"solicito su autorización para llegar tarde el día {fechaInicio}.";
                            break;
                        // Incapacidad por paternidad
                        case 9:
                            intencion = $"comunico mi incapacidad por: Incapacidad por paternidad, la cual será efectiva a partir del día: {fechaInicio}. Anexo copia de mi incapacidad otorgada por mi Institución de Seguro Social.";
                            break;
                        // Incapacidad por maternidad
                        case 10:
                            intencion = $"comunico mi incapacidad por: Incapacidad por maternidad, la cual será efectiva a partir del día: {fechaInicio}. Anexo copia de mi incapacidad otorgada por mi Institución de Seguro Social.";
                            break;
                        default:
                            break;
                    }


                    // Creando el PDF
                    string nombrePDF = "Incidencia" + incidencia.Id;

                    var document = Document.Create(container =>
                    {


                        container.Page(page =>
                        {
                            page.Margin(1);


                            page.Header().Layers(layers =>
                            {
                                byte[] imageBanner = File.ReadAllBytes(@"C:\StaticFiles\Mate\img\banner.png");
                                
                                layers.PrimaryLayer().Image(imageBanner);

                                byte[] imageLogo = File.ReadAllBytes(@"C:\StaticFiles\Mate\img\mate_logo_name.png");
                                layers.Layer().Width(2, Unit.Inch).TranslateX(30).TranslateY(50).Image(imageLogo);

                            });

                            page.Content().PaddingVertical(60).Column(column =>
                            {

                                //column.Spacing(10);
                                column.Item().TranslateX(30).TranslateY(0).Width(530).Column(column =>
                                {
                                    column.Item().Text(tipo).FontSize(18).Bold().FontFamily(Fonts.TimesNewRoman).LineHeight(2);
                                    column.Item().Text("Por medio de la presente, " + intencion).FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                    column.Item().Text("Sin otro particular, quedo de usted.").FontSize(12).FontFamily(Fonts.TimesNewRoman).LineHeight(2);
                                });


                                column.Item().TranslateY(400).AlignCenter().Row(row => {
                                    row.Spacing(50);

                                    row.AutoItem().AlignCenter().Column(column => {

                                        column.Item().AlignCenter().Text("Atentamente").FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                        column.Item().Width(200).PaddingTop(20).LineHorizontal(1).LineColor(Colors.Black);
                                        column.Item().AlignCenter().Text("Firma y fecha").FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                        column.Item().AlignCenter().Text(employee.NombreCompleto()).FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                        column.Item().AlignCenter().Text("Solicitante").FontSize(12).FontFamily(Fonts.TimesNewRoman);

                                    });

                                    row.AutoItem().AlignCenter().Column(async column => {

                                        column.Item().AlignCenter().Text("Autorización").FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                        column.Item().Width(200).PaddingTop(20).LineHorizontal(1).LineColor(Colors.Black);
                                        column.Item().AlignCenter().Text("Firma y fecha").FontSize(12).FontFamily(Fonts.TimesNewRoman);

                                        //if (employee.JefeId == null)
                                        //{
                                        //    column.Item().AlignCenter().Text("Nombre del jefe").FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                        //}
                                        //else
                                        //{
                                            if (jefe == null)
                                            {
                                                column.Item().AlignCenter().Text("Nombre del jefe").FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                            }
                                            else
                                            {
                                                column.Item().AlignCenter().Text(jefe.NombreCompleto()).FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                            }
                                        //}

                                        column.Item().AlignCenter().Text("Jefe Directo").FontSize(12).FontFamily(Fonts.TimesNewRoman);

                                    });

                                });
                            });
                        });
                    });

                    var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\IncidenciasCreadas", $"{nombrePDF}.pdf");

                    document.GeneratePdf(rutaCompleta);

                    return rutaCompleta.Split(@"C:\").Last();

                }
            }

        }
        
        public async Task<string> PdfEstadoDeCuentaWise(int ahorroWiseId, int periodo, int companyId)
        {
            var ahorroWise = await _repositoryAsyncAhorroWise.GetByIdAsync(ahorroWiseId);
            var movimientosAhorroWise = await _repositoryAsyncMovimientoAhorroWise.ListAsync(new MovimientoAhorroWiseByCompanyIdAndAhorroWiseIdSpecification(companyId, ahorroWiseId));
            var employee = await _repositoryAsyncEmployee.GetByIdAsync(ahorroWise.EmployeeId);
            var ultimoMovimiento = movimientosAhorroWise.MaxBy(mov => mov.MovimientoId);
            var penultimoMovimiento = movimientosAhorroWise.FirstOrDefault(mov => mov.MovimientoId == ultimoMovimiento.MovimientoId - 1);
            var enlace = "";
            var total = (await _ahorroWiseService.CalcularTotalAhorroWise(employee.Id, penultimoMovimiento.Periodo)).ToString("0.00");
            var movimientosAhorroWiseOrdenados = movimientosAhorroWise.OrderBy(mov => mov.Periodo);
            var company = await _repositoryAsyncCompany.GetByIdAsync(companyId);

            static QuestPDF.Infrastructure.IContainer Block(QuestPDF.Infrastructure.IContainer container)
            {
                return container
                    .Border(1)
                    .Background(Colors.Grey.Lighten3);
            }
            
            DateTime fechaActual = DateTime.Now;
            string nombrePDF = ahorroWiseId + "-" + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond;
            var rutaCompleta = String.Format(System.IO.Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\EstadosDeCuentaWise", $"{nombrePDF}.pdf"));

            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Header().AlignCenter().Padding(20).Row(row =>
                    {
                        // Logo de la compañía.
                        row.RelativeItem().AlignCenter().Height(120).Column(column =>
                        {
                            var image = Image.FromFile(@"C:\" + company.CompanyProfile);
                            column
                                .Item()
                                .Image(image)
                                .WithCompressionQuality(ImageCompressionQuality.High)
                                .FitArea();
                        });

                        // Logo de WISE.
                        row.RelativeItem().AlignCenter().Height(120).PaddingLeft(20).Column(column =>
                        {
                            column.Item().AlignCenter().Text("ESTADO DE CUENTA").Bold().FontSize(14);
                            var image = Image.FromFile(@"C:\" + company.CompanyProfile);
                            column
                                .Item()
                                .Image(image)
                                .WithCompressionQuality(ImageCompressionQuality.High)
                                .FitArea();
                        });
                    });
                    page.Content().Padding(20).Column(row =>
                    {
                        row.Item().ExtendHorizontal().Row(row =>
                        {
                            // Columna con la tabla de información personal.
                            row.RelativeItem().ExtendHorizontal().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header
                                    .Cell()
                                    .ColumnSpan(4)
                                    .Background("#748FC6")
                                    .Text("INFORMACIÓN PERSONAL")
                                    .FontSize(10);
                                });

                                // NOMBRE
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("NOMBRE:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.ApellidoPaterno + " " + employee.ApellidoMaterno + " " + employee.Nombre)
                                    .FontSize(9);

                                // RFC
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("RFC:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.Rfc)
                                    .FontSize(9);

                                // CURP
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("CURP:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.Curp)
                                    .FontSize(9);

                                // ID DEL EMPLEADO
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("ID DEL EMPLEADO:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.Id)
                                    .FontSize(9);

                                // BANCO
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("BANCO:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.Banco)
                                    .FontSize(9);

                                // NO. DE CUENTA
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("NO. DE CUENTA:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.NoCuenta)
                                    .FontSize(9);

                                // PUESTO O CARGO
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("PUESTO O CARGO:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.Puesto)
                                    .FontSize(9);

                                // CORREO ELECTRÓNICO
                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Background("#D8E4EC")
                                    .Padding(2)
                                    .Text("CORREO ELECTRÓNICO:")
                                    .FontSize(9);

                                table
                                    .Cell()
                                    .ColumnSpan(2)
                                    .Padding(2)
                                    .Text(employee.MailCorporativo)
                                    .FontSize(9);

                            });

                            // Columna con la tabla del periodo del estado de cuenta, resumen del periodo y saldo total.
                            row.RelativeItem().Column(column =>
                            {
                                // Tabla del periodo del estado de cuenta.
                                column.Item().PaddingLeft(20).ExtendHorizontal().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    table.Header(header =>
                                    {
                                        header
                                        .Cell()
                                        .ColumnSpan(4)
                                        .Background("#748FC6")    
                                        .Text("PERIODO DEL ESTADO DE CUENTA")
                                        .FontSize(10);
                                    });

                                    table
                                        .Cell()
                                        .ColumnSpan(4)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .AlignCenter()
                                        .Text(ahorroWise.PeriodoInicial + " - " + periodo);
                                });

                                // Tabla de resumen del periodo.
                                column.Item().PaddingLeft(20).PaddingTop(20).ExtendHorizontal().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    table.Header(header =>
                                    {
                                        header
                                        .Cell()
                                        .ColumnSpan(4)
                                        .Background("#748FC6")    
                                        .Text("RESUMEN DEL PERIODO")
                                        .FontSize(10);
                                    });

                                    // SALDO ANTERIOR
                                    table
                                        .Cell()
                                        .ColumnSpan(2)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .Text("SALDO ANTERIOR:")
                                        .FontSize(9);

                                    table
                                        .Cell()
                                        .ColumnSpan(2)    
                                        .Padding(2)
                                        .Text("$" + total)
                                        .FontSize(9);

                                    // QUINCENA
                                    table
                                        .Cell()
                                        .ColumnSpan(2)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .Text("QUINCENA:")
                                        .FontSize(9);

                                    table
                                        .Cell()
                                        .ColumnSpan(2)    
                                        .Padding(2)
                                        .Text(periodo)
                                        .FontSize(9);

                                    // APORTACIÓN
                                    table
                                        .Cell()
                                        .ColumnSpan(2)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .Text("APORTACIÓN:")
                                        .FontSize(9);

                                    table
                                        .Cell()
                                        .ColumnSpan(2)    
                                        .Padding(2)
                                        .Text("$" + ultimoMovimiento.Monto)
                                        .FontSize(9);

                                    // RENDIMIENTO
                                    table
                                        .Cell()
                                        .ColumnSpan(2)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .Text("RENDIMIENTO:")
                                        .FontSize(9);

                                    table
                                        .Cell()
                                        .ColumnSpan(2)    
                                        .Padding(2)
                                        .Text("$" + ultimoMovimiento.Rendimiento)
                                        .FontSize(9);

                                    // SALDO ACTUAL
                                    table
                                        .Cell()
                                        .ColumnSpan(2)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .Text("SALDO ACTUAL:")
                                        .FontSize(9);

                                    table
                                        .Cell()
                                        .ColumnSpan(2)    
                                        .Padding(2)
                                        .Text("$" + total)
                                        .FontSize(9);

                                    // RECUPERACIÓN
                                    table
                                        .Cell()
                                        .ColumnSpan(2)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .Text("RECUPERACIÓN:")
                                        .FontSize(9);

                                    table
                                        .Cell()
                                        .ColumnSpan(2)    
                                        .Padding(2)
                                        .Text("")
                                        .FontSize(9);
                                });

                                // Tabla de saldo total.
                                column.Item().PaddingLeft(20).PaddingTop(20).ExtendHorizontal().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    table.Header(header =>
                                    {
                                        header
                                        .Cell()
                                        .ColumnSpan(4)
                                        .Background("#748FC6")    
                                        .Text("SALDO TOTAL")
                                        .FontSize(10);
                                    });

                                    table
                                        .Cell()
                                        .ColumnSpan(4)
                                        .Background("#D8E4EC")    
                                        .Padding(2)
                                        .AlignCenter()
                                        .Text("$" + total);
                                });
                            });
                        });

                        // Tabla de resumen general.
                        row.Item().ExtendHorizontal().PaddingTop(20).Column(columnRow =>
                        {
                            columnRow.Item().ExtendHorizontal().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header
                                    .Cell()
                                    .ColumnSpan(3)
                                    .Background("#748FC6")
                                    .Text("RESUMEN GENERAL")
                                    .FontSize(10);

                                    header
                                        .Cell()
                                        .Background("#D8E4EC")
                                        .Padding(10)
                                        .AlignCenter()
                                        .Text("PERIODO")
                                        .FontSize(10);

                                    header
                                        .Cell()
                                        .Background("#D8E4EC")
                                        .Padding(10)
                                        .AlignCenter()
                                        .Text("Imagen")
                                        .FontSize(10);

                                    header
                                        .Cell()
                                        .Background("#D8E4EC")
                                        .Padding(10)
                                        .AlignCenter()
                                        .Text("SALDO")
                                        .FontSize(10);
                                });


                                // Sección que contendrá los movimientos del periodo.
                                foreach (var movimiento in movimientosAhorroWiseOrdenados)
                                {
                                    if (movimiento.Periodo <= periodo)
                                    {
                                        var totalAhorroWise = 0.0;
                                        foreach (var mov in movimientosAhorroWise)
                                        {
                                            if (mov.Periodo <= movimiento.Periodo)
                                            {
                                                totalAhorroWise += mov.Monto;
                                            }
                                        }

                                        table
                                            .Cell()
                                            .Padding(10)
                                            .AlignCenter()
                                            .Text(movimiento.Periodo);

                                        table
                                            .Cell()
                                            .Padding(10)
                                            .AlignCenter()
                                            .Text("$" + movimiento.Monto);

                                        table
                                            .Cell()
                                            .Padding(10)
                                            .AlignCenter()
                                            .Text("$" + totalAhorroWise.ToString("0.00"));
                                    }
                                }
                            });
                        });
                    });
                });
            }).GeneratePdf(rutaCompleta);
            return rutaCompleta;
        }

    }
}


