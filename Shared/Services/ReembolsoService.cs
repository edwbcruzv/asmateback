using Application.DTOs.Administracion;
using Application.DTOs.ReembolsosOperativos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.ReembolsosOperativos.MovimientoReembolsos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class ReembolsoService : IReembolsoService
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryReembolso;
        private readonly IRepositoryAsync<MovimientoReembolso> _repositoryMovReembolso;
        private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncTipoMoneda;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;
        private readonly IFileToRarService _fileToRar;

        public ReembolsoService(IRepositoryAsync<Reembolso> repositoryReembolso, IRepositoryAsync<MovimientoReembolso> repositoryMovReembolso, IExcelService excelService, IMapper mapper, IFileToRarService fileToRar, IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda)
        {
            _repositoryReembolso = repositoryReembolso;
            _repositoryMovReembolso = repositoryMovReembolso;
            _excelService = excelService;
            _mapper = mapper;
            _fileToRar = fileToRar;
            _repositoryAsyncTipoMoneda = repositoryAsyncTipoMoneda;
        }

        public async Task<double> CalcularMontoTotalReembolso(int reembolsoId)
        {
            var Total = 0.0;
            var list_mov_reembolso = await _repositoryMovReembolso.ListAsync(new MovimientoReembolsoByReembolsoIdSpecification(reembolsoId));
            foreach (var movimiento in list_mov_reembolso)
            {
                /*if (movimiento.TipoMonedaId == 115 || movimiento.TipoMonedaId == null)
                {
                    
                    Total += movimiento.Total;
                }
                else
                {
                    Total += (movimiento.Total * (double)movimiento.TipoCambio);
                }*/
                //Total += (movimiento.Total * (double)movimiento.TipoCambio);
                Total += movimiento.Total;
            }
            return Total;
        }

        public async Task<Response<string>> CrearExcelMovimientoReembolso(int reembolsoId)
        {
            var listaTipoMoneda = await _repositoryAsyncTipoMoneda.ListAsync();

            var diccionarioTipoMoneda = listaTipoMoneda.ToDictionary(x => x.Id, x => x.CodigoIso);


            var listaMovimientos = await _repositoryMovReembolso.ListAsync(new MovimientoReembolsoByReembolsoIdSpecification(reembolsoId));
            
            if (listaMovimientos.Count == 0) throw new ApiException($"El reembolso con Id {reembolsoId} no cuenta con movimientos");

            List<MovimientoReembolsoDTO> listaMovimientosDTO = new List<MovimientoReembolsoDTO>();

            foreach (var movimiento in listaMovimientos)
            {
                var movimientoDTO = _mapper.Map<MovimientoReembolsoDTO>(movimiento);
                if (movimientoDTO != null) 
                {
                    if (movimiento.TipoMonedaId != null)
                    {
                        movimientoDTO.TipoMoneda = diccionarioTipoMoneda[(int)movimiento.TipoMonedaId];
                    }
                    listaMovimientosDTO.Add(movimientoDTO);
                }
                
            }

            String rutaArchivo = await _excelService.ArchivoExcelMovimientosReembolso(listaMovimientosDTO);
            Response<string> respuesta = new Response<string>();
            respuesta.Succeeded = true;
            respuesta.Message = "Descarga realizada con éxito";
            respuesta.Data = rutaArchivo;
            return respuesta;
        }

        /// <summary>
        ///     Método para crear el .zip de los reembolsos que se envían del front
        /// </summary>
        /// <param name="ids">
        ///     Ids de los reembolsos a descargar  enviados por el usuario desde el front
        /// </param>
        /// <returns>
        ///     La respuesta al front con la ruta del archivo .zip creado
        /// </returns>
        public async Task<Response<string>> DescargaMasivaReembolsos(int[] ids)
        {
            // Se crea un diccionario principal con pares ReembolsoId:DiccionarioMovimientos
            Dictionary<int, Dictionary<int, List<string>>> diccionarioReembolsos = new Dictionary<int, Dictionary<int, List<string>>>();
            
            // Se itera sobre la lista de ids para crear un diccionario por reembolso
            for (int i = 0; i< ids.Length; i++)
            {
                // Se obtienen los movimientos del reembolso sobre el que se está iterando
                var listaMovimientosPorReembolso = await _repositoryMovReembolso.ListAsync(new MovimientoReembolsoByReembolsoIdSpecification(ids[i]));
                // Se crea el diccionario de movimientos por reembolso, con pares MovimientoReembolsoId : ListaDeArchivos
                Dictionary<int, List<string>> diccionarioMovimientosPorReembolso = new Dictionary<int, List<string>>();

                // Se itera por cada uno de los movimientos dentro del reembolso actual
                foreach (var movimiento in listaMovimientosPorReembolso) {
                    // Se crea la lista de archivos por cada movimiento (En este momento solo son un PDF y un XML)
                    List<string> archivosPorMovimiento = new List<string>();
                    
                    // Si el movimiento no es nulo, se accede a sus datos
                    if (movimiento != null)
                    {
                        // Si tiene registro del archivo PDF, se guarda la ruta en la lista, si no, se guarda una cadena "No" que será ignorada después.
                        if (movimiento.PDFSrcFile != null)
                        {
                            
                            archivosPorMovimiento.Add(movimiento.PDFSrcFile);
                        }
                        else
                        {
                            archivosPorMovimiento.Add("No");
                        }
                        // Si tiene registro del archivo XML, se guarda la ruta en lista, si no, se guarda una cadena "No" que será ignorada después.
                        if (movimiento.XMLSrcFile != null)
                        {
                            
                            archivosPorMovimiento.Add(movimiento.XMLSrcFile);
                        }
                        else
                        {
                            archivosPorMovimiento.Add("No");
                        }
                      // Se guarda el par MovimientoID:ListaDeArchivos en el diccionario de movimientos por reembolso
                      diccionarioMovimientosPorReembolso.Add(movimiento.Id, archivosPorMovimiento);
                    }

                }
                // Despues de iterar por todos los movimientos del reembolso, se guarda el diccionario de movimientos por reembolso dentro de su respectivo
                // reembolso, el cual tenemos referenciado por ids[i]
                diccionarioReembolsos.Add(ids[i], diccionarioMovimientosPorReembolso);
            }

            // Se obtiene la ruta del zip de la función dentro del servicio de RAR
            string rutaArchivo = await _fileToRar.createRarReembolso(diccionarioReembolsos); 
            // Se crea la respuesta y se manda al front
            Response<string> respuesta = new Response<string>();
            respuesta.Succeeded = true;
            respuesta.Message = null;
            respuesta.Data = rutaArchivo;
           
            return respuesta;

        }

        public async Task<Dictionary<string, double>> ObtenerTotalesReembolso(int reembolsoId)
        {
            Dictionary<string, double> diccionarioTotales = new Dictionary<string, double>();

            // LLaves para el diccionario
            string subTotal = "SubTotal";
            string descuento = "Descuento";
            string impuestosRetenidos = "ImpuestosRetenidos";
            string impuestosTrasladados = "ImpuestosTrasladados";
            string ieps = "IEPS";
            string ish = "ISH";
            string total = "TotalPagar";

            // Valores para el diccionario

            double subTotalValor = 0;
            double descuentoValor = 0;
            double impuestosRetenidosValor = 0;
            double impuestosTrasladadosValor = 0;
            double iepsValor = 0;
            double ishValor = 0;
            double totalValor = 0;

            var listaMovimientosReembolso = await _repositoryMovReembolso.ListAsync(new MovimientoReembolsoByReembolsoIdSpecification(reembolsoId));

            foreach(MovimientoReembolso mov_reembolso in listaMovimientosReembolso)
            {
                subTotalValor += mov_reembolso.Subtotal;
                descuentoValor += 0.0;
                if (mov_reembolso.IVARetenidos != null)
                {
                    impuestosRetenidosValor += (double) mov_reembolso.IVARetenidos;
                }
                else
                {
                    impuestosRetenidosValor += 0.0;
                }

                if (mov_reembolso.IVATrasladados != null)
                {
                    impuestosTrasladadosValor += (double)mov_reembolso.IVATrasladados;
                }
                else
                {
                    impuestosTrasladadosValor += 0.0;
                }

                if (mov_reembolso.IEPS != null)
                {
                    iepsValor += (double)mov_reembolso.IEPS;
                }
                else
                {
                    iepsValor += 0.0;
                }

                if (mov_reembolso.ISH != null)
                {
                    ishValor += (double)mov_reembolso.ISH;
                }
                else
                {
                    ishValor += 0.0;
                }

                totalValor += mov_reembolso.Total;
               
            }

            diccionarioTotales.Add(subTotal, subTotalValor);
            diccionarioTotales.Add(impuestosRetenidos, impuestosRetenidosValor);
            diccionarioTotales.Add(impuestosTrasladados, impuestosTrasladadosValor);
            diccionarioTotales.Add(ieps, iepsValor);
            diccionarioTotales.Add(ish, ishValor);
            diccionarioTotales.Add(descuento, descuentoValor);
            diccionarioTotales.Add(total, totalValor);

            /*foreach (var elem in diccionarioTotales.Keys)
            {
                Console.WriteLine(diccionarioTotales[elem]);
            }*/
            return diccionarioTotales;
        }
    }
}
