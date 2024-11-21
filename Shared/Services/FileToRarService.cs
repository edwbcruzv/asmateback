using Application.DTOs.Administracion;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class FileToRarService : IFileToRarService
    {
        private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsyncFacturaMovimiento;
        private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
        
        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
        private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsyncComplementoPagoFactura;

        private readonly IPdfService _pdfService;

        public FileToRarService(IRepositoryAsync<FacturaMovimiento> repositoryAsyncFacturaMovimiento,
            IPdfService pdfService, IRepositoryAsync<Factura> repositoryAsyncFactura, 
            IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago, 
            IRepositoryAsync<ComplementoPagoFactura> repositoryAsyncComplementoPagoFactura)
        {
            _repositoryAsyncFacturaMovimiento = repositoryAsyncFacturaMovimiento;
            _repositoryAsyncFactura = repositoryAsyncFactura;
            _pdfService = pdfService;
            _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
            _repositoryAsyncComplementoPagoFactura = repositoryAsyncComplementoPagoFactura;
        }

        public async Task<Response<SourceFileDto>> createRarComplementoPago(int[] ids)
        {
            var response = new SourceFileDto();

            foreach (var id in ids)
            {
                var facturasAsociadasList = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(id));

                if (facturasAsociadasList.Count == 0)
                {
                    throw new ApiException($"ComplementoPago ${id} no cuenta con facturas asociadas");
                }
            }

            DateTime date = DateTime.UtcNow;

            var fileRarName = $"ComplementoPago-{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}{date.Millisecond}";
            var basePath = $@"StaticFiles\Mate\FormatosPDF\ComplementosPagoCreadas\";
            var diskPath = $@"C:\";

            var fullPathFile = Path.Combine(diskPath, basePath, fileRarName);

            try
            {
                if (!Directory.Exists(fullPathFile))
                {
                    Directory.CreateDirectory(fullPathFile);
                }
                else
                {
                    throw new ApiException($"Ocurrio un error, intente de nuevo");
                }
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error al crear la carpeta: " + ex.Message);
            }

            foreach (var id in ids)
            {
                var complementoPago = await _repositoryAsyncComplementoPago.GetByIdAsync(id);

                var fileNameItem = $"{complementoPago.Id}-{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}{date.Millisecond}";

                var fileFullPathItem = Path.Combine(fullPathFile, fileNameItem);
                try
                {

                    if (!Directory.Exists(fileFullPathItem))
                    {
                        Directory.CreateDirectory(fileFullPathItem);
                    }
                    else
                    {
                        throw new ApiException($"Ocurrio un error, intente de nuevo");
                    }
                }
                catch (Exception ex)
                {
                    throw new ApiException("Ocurrió un error al crear la carpeta: " + ex.Message);
                }

                var itemPdf = await _pdfService.PdfComplementoPago(id);

                var fileFacturaPdf = Path.Combine(diskPath, itemPdf.Data.SourceFile);
                File.Move(fileFacturaPdf, Path.Combine(fileFullPathItem, $"ComplementoPago-{complementoPago.Uuid}.pdf"));

                if (complementoPago.FileXmlTimbrado != null)
                {
                    var fileFacturaXml = Path.Combine(diskPath, complementoPago.FileXmlTimbrado);
                    File.Copy(fileFacturaXml, Path.Combine(fileFullPathItem, $"ComplementoPago-{complementoPago.Uuid}.xml"));

                }

                if (complementoPago.PagoSrcPdf != null && !complementoPago.PagoSrcPdf.Equals(""))
                {
                    var fileFacturaPago = Path.Combine(diskPath, complementoPago.PagoSrcPdf);
                    File.Copy(fileFacturaPago, Path.Combine(fileFullPathItem, $"ComplementoPagoPago-{complementoPago.Uuid}.pdf"));

                }


            }

            try
            {
                // Comprimir la carpeta en un archivo ZIP
                ZipFile.CreateFromDirectory(fullPathFile, $"{fullPathFile}.zip");
                Directory.Delete(fullPathFile, true);
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error al comprimir la carpeta: " + ex.Message);
            }


            response.SourceFile = Path.Combine(basePath, $"{fileRarName}.zip");

            return new Response<SourceFileDto>(response);

        }

        public async Task<Response<SourceFileDto>> createRarFacturas(int[] ids)
        {
            var response = new SourceFileDto();

            foreach (var id in ids)
            {
                var facturaMovimientoList = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(id));

                if (facturaMovimientoList.Count == 0)
                {
                    throw new ApiException($"Factura ${id} no cuenta con movimientos");
                }

            }

            //Creamos la carpeta donde meteremos todos los PDF a comprimir

            DateTime date = DateTime.UtcNow;

            var fileRarName = $"Facturas-{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}{date.Millisecond}";
            var basePath = $@"StaticFiles\Mate\FormatosPDF\FacturasCreadas\";
            var diskPath = $@"C:\";

            var fullPathFile = Path.Combine(diskPath, basePath, fileRarName);

            try
            {
                if (!Directory.Exists(fullPathFile))
                {
                    Directory.CreateDirectory(fullPathFile);
                }
                else
                {
                    throw new ApiException($"Ocurrio un error, intente de nuevo");
                }
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error al crear la carpeta: " + ex.Message);
            }

            foreach (var id in ids)
            {
                var factura = await _repositoryAsyncFactura.GetByIdAsync(id);

                var fileNameItem = $"{factura.Id}-{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}{date.Millisecond}";

                var fileFullPathItem = Path.Combine(fullPathFile, fileNameItem);
                try
                {

                    if (!Directory.Exists(fileFullPathItem))
                    {
                        Directory.CreateDirectory(fileFullPathItem);
                    }
                    else
                    {
                        throw new ApiException($"Ocurrio un error, intente de nuevo");
                    }
                }
                catch (Exception ex)
                {
                    throw new ApiException("Ocurrió un error al crear la carpeta: " + ex.Message);
                }

                var itemPdf = await _pdfService.PdfFactura(id);

                var fileFacturaPdf = Path.Combine(diskPath, itemPdf.Data.SourcePdf);
                File.Move(fileFacturaPdf, Path.Combine(fileFullPathItem, $"Factura-{factura.Uuid}.pdf"));

                if (factura.FileXmlTimbrado != null)
                {
                    var fileFacturaXml = Path.Combine(diskPath, factura.FileXmlTimbrado);
                    File.Copy(fileFacturaXml, Path.Combine(fileFullPathItem, $"Factura-{factura.Uuid}.xml"));

                }

                if (factura.PagoSrcPdf != null && !factura.PagoSrcPdf.Equals(""))
                {
                    var fileFacturaPago = Path.Combine(diskPath, factura.PagoSrcPdf);
                    File.Copy(fileFacturaPago, Path.Combine(fileFullPathItem, $"FacturaPago-{factura.Uuid}.pdf"));

                }


            }

            try
            {
                // Comprimir la carpeta en un archivo ZIP
                ZipFile.CreateFromDirectory(fullPathFile, $"{fullPathFile}.zip");
                Directory.Delete(fullPathFile, true);
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error al comprimir la carpeta: " + ex.Message);
            }


            response.SourceFile = Path.Combine(basePath, $"{fileRarName}.zip");

            return new Response<SourceFileDto>(response);
        }

        /// <summary>
        ///     Método que convierte el diccionario de reembolsos a comprimir en su respectivo .zip
        /// </summary>
        /// <param name="diccionarioReembolsos">
        ///     Diccionario de reembolsos a comprimir
        /// </param>
        /// <returns>
        ///     La ruta del archivo creado
        /// </returns>
        public async Task<string> createRarReembolso(Dictionary<int, Dictionary<int, List<string>>> diccionarioReembolsos)
        {
            DateTime date = DateTime.UtcNow;

            // Rutas y nombre del archivo
            var fileRarName = $"DescargaMasivaReembolsos-{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}{date.Millisecond}";
            var basePath = $@"StaticFiles\Mate\DescargasMasivas\Reembolsos\";
            var diskPath = $@"C:\";
            var fullPathFile = Path.Combine(diskPath, basePath, fileRarName);

            // Se itera sobre cada reembolso
            foreach (var reembolsoId in diccionarioReembolsos.Keys)
            {
                // Se crea la carpeta del reembolso actual. Si todavía no existe, se crea
                var rutaReembolsoAux = Path.Combine(fullPathFile, $@"Reembolso{reembolsoId}\");
                if (!Directory.Exists(rutaReembolsoAux))
                {
                    Directory.CreateDirectory(rutaReembolsoAux);
                }
                // Se itera sobre cada movimiento dentro del reembolso actual
                foreach (var movimientoRembolsoId in diccionarioReembolsos[reembolsoId].Keys)
                {
                    // Se crea la carpeta del movimiento actual dentro del reembolso actual. Si todavía no existe, se crea
                    var rutaMovimientoReembolsoAux = Path.Combine(rutaReembolsoAux, $@"Movimiento{movimientoRembolsoId}\");
                    if (!Directory.Exists(rutaMovimientoReembolsoAux))
                    {
                        Directory.CreateDirectory(rutaMovimientoReembolsoAux);
                    }
                    // Se obtiene la lista de archivos del movimiento actual, dentro del reembolso actual para iterar sobre ella
                    var listaArchivosPorMovimiento = diccionarioReembolsos[reembolsoId][movimientoRembolsoId];

                    foreach (string rutaArchivoIndividual in listaArchivosPorMovimiento)
                    {
                        // Se ignora la cadena "No", la cual se generó en el otro método
                        if (rutaArchivoIndividual != "No")
                        {
                            // Se obtiene el nombre del archivo 
                            // Al ser siempre el final de la ruta, se separa por "\" y se obtiene la última cadena de esa separación
                            string nombreArchivoConExtension = rutaArchivoIndividual.Split(@"\").Last();
                            // Se copia el archivo de su ruta original a la carpeta actual
                            File.Copy(rutaArchivoIndividual, $"{rutaMovimientoReembolsoAux}{nombreArchivoConExtension}");
                        }
                    }
                }
            }
            // Después de recorrer todos los diccionarios, se crea el .zip
            ZipFile.CreateFromDirectory(fullPathFile, $"{fullPathFile}.zip");

            // Se elimina la carpeta usada para crear el .zip
            Directory.Delete(fullPathFile, true);

            // Se crea la ruta del .zip creado y se envía al otro método
            string sourceFile = Path.Combine(basePath, $"{fileRarName}.zip");

            return sourceFile;
        }
    }
}
