using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Sistemas.Commands.UpdateSistema
{
    public class UpdateSistemaCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string? Color { get; set; }
        public int EstadoId { get; set; }

    }

    public class Handler : IRequestHandler<UpdateSistemaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;

        public Handler(IRepositoryAsync<Sistema> repositoryAsyncSistema)
        {
            _repositoryAsyncSistema = repositoryAsyncSistema;
        }

        public async Task<Response<int>> Handle(UpdateSistemaCommand request, CancellationToken cancellationToken)
        {
            var sistema = await _repositoryAsyncSistema.GetByIdAsync(request.Id);

            if (sistema == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                try
                {
                    sistema.Nombre = request.Nombre;
                    sistema.Clave = request.Clave;
                    sistema.Descripcion = request.Descripcion;
                    sistema.Color = request.Color;
                    sistema.EstadoId = request.EstadoId;

                    await _repositoryAsyncSistema.UpdateAsync(sistema);
                }
                catch (Exception ex)
                {
                    throw new KeyNotFoundException($"Error al asignar nuevo estatus. Error: "+  ex.ToString());
                }

                return new Response<int>(sistema.Id);

            }
        }
    }
}
