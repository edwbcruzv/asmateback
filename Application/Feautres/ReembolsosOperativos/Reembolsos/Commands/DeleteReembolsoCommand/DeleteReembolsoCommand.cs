using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.DeleteReembolsoCommand
{
    public class DeleteReembolsoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteReembolsoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsync;

            public Handler(IRepositoryAsync<Reembolso> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteReembolsoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Reembolso no encontrado con el id {request.Id}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id);
            }
        }
    }
}
