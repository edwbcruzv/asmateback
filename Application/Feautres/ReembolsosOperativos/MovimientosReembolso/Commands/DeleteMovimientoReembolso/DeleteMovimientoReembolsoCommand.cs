using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.DeleteMovimientoReembolso
{
    public class DeleteMovimientoReembolsoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteMovimientoReembolsoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsync;

            public Handler(IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso)
            {
                _repositoryAsync = repositoryAsyncMovimientoReembolso;
            }

            public async Task<Response<int>> Handle(DeleteMovimientoReembolsoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"MovimientoReembolso no encontrado con el id {request.Id}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id);
            }
        }
    }
}
