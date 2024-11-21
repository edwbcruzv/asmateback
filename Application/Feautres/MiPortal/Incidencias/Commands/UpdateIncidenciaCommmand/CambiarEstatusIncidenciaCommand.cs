using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Commands.UpdateIncidenciaCommmand
{
    public class CambiarEstatusIncidenciaCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int EstatusId { get; set; }
        public string? Observaciones { get; set; }

        public class Handler: IRequestHandler<CambiarEstatusIncidenciaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Incidencia>  _repositoryAsyncIncidencia;
            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencia, IMapper mapper)
            {
                _repositoryAsyncIncidencia = repositoryAsyncIncidencia;
            }

            public async Task<Response<int>> Handle(CambiarEstatusIncidenciaCommand request, CancellationToken cancellationToken)
            {
                
                Incidencia incidencia = await _repositoryAsyncIncidencia.GetByIdAsync(request.Id);
                if (incidencia != null)
                {
                    incidencia.EstatusId = request.EstatusId;
                    incidencia.Observciones = request.Observaciones;
                    await _repositoryAsyncIncidencia.UpdateAsync(incidencia);
                    string message = "El estatus de la incidencia se ha actualizado correctamente";
                    

                    Response<int> respuesta = new Response<int>();
                    respuesta.Succeeded = true;
                    respuesta.Message = message;
                    respuesta.Data = incidencia.Id;

                    return respuesta;

                }
                else
                {
                    throw new ApiException("Se ha producido un error, vuelva a intentarlo más tarde");
                }
            }
        }
    }

    
}
