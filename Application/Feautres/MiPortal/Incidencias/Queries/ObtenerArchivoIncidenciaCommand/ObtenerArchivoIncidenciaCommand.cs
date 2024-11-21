using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Queries.ObtenerArchivoIncidenciaCommand
{
    public class ObtenerArchivoIncidenciaCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<ObtenerArchivoIncidenciaCommand, Response<string>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;

            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencias)
            {
                _repositoryAsyncIncidencias = repositoryAsyncIncidencias;
            }

            public async Task<Response<string>> Handle(ObtenerArchivoIncidenciaCommand request, CancellationToken cancellationToken)
            {
                Incidencia incidencia = await _repositoryAsyncIncidencias.GetByIdAsync(request.Id);
                if (incidencia == null)
                {
                    throw new KeyNotFoundException($"La incidencia con Id {request.Id} no existe");

                }
                else
                {
                    string rutaArchivo = incidencia.ArchivoSrc;

                    if (rutaArchivo == null)
                    {
                        throw new KeyNotFoundException("No se encontró la ruta del archivo");
                    }
                    else
                    {
                        Response<string> respuesta = new Response<string>();
                        respuesta.Succeeded = true;
                        respuesta.Data = rutaArchivo;

                        return respuesta;

                    }
                }
            }
        }
    }
}
