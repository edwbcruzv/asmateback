
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Viaticos.Commands.DeleteViaticoCommand
{
    public class DeleteViaticoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteViaticoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Viatico> _repositoryAsync;

            public Handler(IRepositoryAsync<Viatico> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteViaticoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Viatico no encontrado con el id {request.Id}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id,"Viatico eliminado");
            }
        }
    }
}
