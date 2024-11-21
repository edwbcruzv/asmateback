using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.UpdateComplementoPagoFacturaCommand;
using Application.Interfaces;
using Application.Specifications.Catalogos;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.UpdateReembolsoCommand
{
    public class UpdateReembolsoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Clabe { get; set; }
        public int EstatusId { get; set; }

        public class Handler : IRequestHandler<UpdateReembolsoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IRepositoryAsync<TipoEstatusReembolso> _repositoryAsyncTipoEstatusReembolso;

            public Handler(IRepositoryAsync<Reembolso> repositoryAsync, IRepositoryAsync<TipoEstatusReembolso> repositoryAsyncTipoEstatusReembolso)
            {
                _repositoryAsyncReembolso = repositoryAsync;
                _repositoryAsyncTipoEstatusReembolso = repositoryAsyncTipoEstatusReembolso;
            }

            public async Task<Response<int>> Handle(UpdateReembolsoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsyncReembolso.GetByIdAsync(request.Id);
                var estatus = await _repositoryAsyncTipoEstatusReembolso.FirstOrDefaultAsync(new TipoEstatusReembolsoByIdSpecifiction(request.EstatusId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Reembolso no encontrado con el id {request.Id}.");
                }

                if (estatus == null)
                {
                    throw new KeyNotFoundException($"El Estatus del reembolso con Id {request.EstatusId} no fue encontrada.");
                }

                elem.Descripcion = request.Descripcion;
                elem.Clabe = request.Clabe;
                elem.EstatusId = estatus.Id;

                await _repositoryAsyncReembolso.UpdateAsync(elem);

                return new Response<int>(elem.Id);
            }
        }

    }


}
