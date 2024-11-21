using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Prestamos.Commands.UpdatePrestamoCommand
{
    public class UpdatePrestamoEstatusCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public EstatusOperacion Estatus { get; set; }

 

        public class Handler : IRequestHandler<UpdatePrestamoEstatusCommand, Response<int>>
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

            public async Task<Response<int>> Handle(UpdatePrestamoEstatusCommand request, CancellationToken cancellationToken)
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
                        if ((int)prestamo.Estatus + 1 < (int)request.Estatus)
                        {
                            return new Response<int>(-1, "El estatus no puede ser aplicado porque se esta saltando un estatus intermedio.");
                        }


                        switch (request.Estatus)
                        {
                            case EstatusOperacion.Pendiente:
                                prestamo.FechaEstatusPendiente = DateTime.Now;
                                break;
                            case EstatusOperacion.Activo:
                                prestamo.FechaEstatusActivo = DateTime.Now;
                                break;
                            case EstatusOperacion.Rechazado:
                                prestamo.FechaEstatusRechazado = DateTime.Now;
                                break;
                            case EstatusOperacion.Finiquitado:
                                prestamo.FechaEstatusFiniquitado = DateTime.Now;
                                break;

                            default:
                                Console.WriteLine("Opción no válida.");
                                break;
                        }

                        prestamo.Estatus = request.Estatus;

                        await _repositoryAsyncPrestamo.UpdateAsync(prestamo);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar el estatus." + ex.ToString());
                    }

                    return new Response<int>(prestamo.Id);
                }

            }
        }
    }
}
