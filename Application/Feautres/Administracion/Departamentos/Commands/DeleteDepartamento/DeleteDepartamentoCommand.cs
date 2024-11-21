using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Departamentos.Commands.DeleteDepartamento
{
    public class DeleteDepartamentoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteDepartamentoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Departamento> _repositoryAsync;

            public Handler(IRepositoryAsync<Departamento> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteDepartamentoCommand request, CancellationToken cancellationToken)
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
