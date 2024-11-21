using Application.Feautres.Kanban.Tickets.Commands.DeleteTicket;
using Application.Interfaces;
using Application.Specifications.Kanban.SistemasDepartamentos;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.SistemasDepartamentos.Commands.Delete
{
    public class DeleteSistemaDepartamentoCommand : IRequest<Response<Boolean>>
    {
        public int SistemaId { get; set; }
        public int DepartamentoId { get; set; }

        public class Handler : IRequestHandler<DeleteSistemaDepartamentoCommand, Response<Boolean>>
        {
            private readonly IRepositoryAsync<SistemaDepartamento> _repositoryAsync;

            public Handler(IRepositoryAsync<SistemaDepartamento> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<Boolean>> Handle(DeleteSistemaDepartamentoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.FirstOrDefaultAsync(new SistemaDepartamentoBySistemaIdAndDepartamentoIdSpecification(request.SistemaId,request.DepartamentoId));
                
                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el ids {request.SistemaId} y {request.DepartamentoId}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<Boolean>(true);
            }
        }
    }
}
