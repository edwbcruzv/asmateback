using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Puestos.Commands.DeletePuesto
{
    public class DeletePuestoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeletePuestoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Puesto> _repositoryAsync;

            public Handler(IRepositoryAsync<Puesto> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeletePuestoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id, "Registro eliminado");
            }
        }
    }
}
