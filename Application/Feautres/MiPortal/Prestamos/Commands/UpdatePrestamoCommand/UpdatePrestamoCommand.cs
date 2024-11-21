using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Prestamos.Commands.UpdatePrestamoCommand
{
    public class UpdatePrestamoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        public int? PeriodoFinal { get; set; }
        //public EstatusOperacion Estatus { get; set; }

        //public DateTime FechaEstatusPendiente { get; set; }
        //public DateTime? FechaEstatusActivo { get; set; }
        //public DateTime? FechaEstatusInactivo { get; set; }
        //public DateTime? FechaEstatusFiniquitado { get; set; }

        public int Plazo { get; set; }
        public float Monto { get; set; }
        public float MontoPagado { get; set; }
        public float Interes { get; set; }
        public float TazaInteres { get; set; }
        public float PlazoInteres { get; set; }
        public float FondoGarantia { get; set; }
        public float TazaFondoGarantia { get; set; }
        public DateTime? FechaTransferencia { get; set; }
        public float Descuento { get; set; }
        public float Total { get; set; }
        public TipoPrestamo Tipo { get; set; }

        //public IFormFile? FileAcuseFirmado { get; set; }
        //public IFormFile? FilePagare { get; set; }
        //public IFormFile? FileConstanciaRetiro { get; set; }
        //public IFormFile? FileSolicitudRetiro { get; set; }

        public class Handler : IRequestHandler<UpdatePrestamoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
            private readonly IFilesManagerService _filesManagerService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsyncPrestamo, IMapper mapper, IFilesManagerService filesManagerService)
            {
                _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
                _mapper = mapper;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<int>> Handle(UpdatePrestamoCommand request, CancellationToken cancellationToken)
            {
                var prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(request.Id);

                if (prestamo == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        
                        prestamo.EmployeeId = request.EmployeeId;
                        prestamo.CompanyId = request.CompanyId;
                        prestamo.FechaInicio = request.FechaInicio;
                        prestamo.FechaFinal = request.FechaFinal;
                        prestamo.PeriodoInicial = request.PeriodoInicial;
                        prestamo.PeriodoFinal = request.PeriodoFinal;
                        //prestamo.Estatus = request.Estatus; // ya se hace

                        //prestamo.FechaEstatusPendiente = request.FechaEstatusPendiente;
                        //prestamo.FechaEstatusActivo = request.FechaEstatusActivo;
                        //prestamo.FechaEstatusInactivo = request.FechaEstatusInactivo;
                        //prestamo.FechaEstatusFiniquitado = request.FechaEstatusFiniquitado;

                        prestamo.Plazo = request.Plazo;
                        prestamo.Monto = request.Monto;
                        prestamo.MontoPagado = request.MontoPagado;
                        prestamo.Interes = request.Interes;
                        prestamo.TazaInteres = request.TazaInteres;
                        prestamo.PlazoInteres = request.PlazoInteres;
                        prestamo.FondoGarantia = request.FondoGarantia;
                        prestamo.TazaFondoGarantia = request.TazaFondoGarantia;
                        prestamo.FechaTransferencia = request.FechaTransferencia;
                        prestamo.Descuento = request.Descuento;
                        prestamo.Total = request.Total;
                        prestamo.Tipo = request.Tipo;


                        ////////////ya se hace, pero es un ejemplo del manejo de archivo :D

                        //if (request.FileAcuseFirmado != null)
                        //{
                        //    _filesManagerService.UpdateFile(request.FileAcuseFirmado, prestamo.SrcDocAcuseFirmado);
                        //}

                        //if (request.FilePagare != null)
                        //{
                        //    _filesManagerService.UpdateFile(request.FilePagare, prestamo.SrcDocPagare);
                        //}

                        //if (request.FileConstanciaRetiro != null)
                        //{
                        //    _filesManagerService.UpdateFile(request.FileConstanciaRetiro, prestamo.SrcDocConstanciaRetiro);
                        //}

                        //if (request.FileSolicitudRetiro != null)
                        //{
                        //    _filesManagerService.UpdateFile(request.FileSolicitudRetiro, prestamo.SrcDocSolicitudRetiro);
                        //}

                        await _repositoryAsyncPrestamo.UpdateAsync(prestamo);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar Solicitud." + ex.ToString());
                    }

                    return new Response<int>(prestamo.Id);
                }

            }
        }
    }
}
