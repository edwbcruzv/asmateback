using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Commands.DeleteIncidenciaCommand
{
    public class DeleteIncidenciaCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler:IRequestHandler<DeleteIncidenciaCommand,Response<int>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencia;

            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencia)
            {
                _repositoryAsyncIncidencia = repositoryAsyncIncidencia;
            }

            public async Task<Response<int>> Handle(DeleteIncidenciaCommand request, CancellationToken cancellationToken)
            {
                Incidencia incidencia = await _repositoryAsyncIncidencia.GetByIdAsync(request.Id);

                if (incidencia == null) 
                {
                    throw new KeyNotFoundException($"No se encontró la incidencia con Id {request.Id}");
                }
                else
                {
                    await _repositoryAsyncIncidencia.DeleteAsync(incidencia);

                    Response<int> respuesta = new Response<int>();
                    respuesta.Succeeded = true;
                    respuesta.Message = "La incidencia fue eliminada con éxito";
                    respuesta.Data = request.Id;

                    return respuesta;
                }



            }
        }
    }
}
