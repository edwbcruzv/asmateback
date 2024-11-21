using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.UpdateReembolsoCommand;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Viaticos.Commands.UpdateEstatusViatico
{
    public class UpdateEstatusViaticoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public EstatusViatico Estatus {  get; set; }

        public class Handler : IRequestHandler<UpdateEstatusViaticoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
            private readonly IMapper _mapper;
            private readonly IViaticoService _viaticoService;

            public Handler(IRepositoryAsync<Viatico> repositoryAsyncViatico, IMapper mapper, IViaticoService viaticoService)
            {
                _repositoryAsyncViatico = repositoryAsyncViatico;
                _mapper = mapper;
                _viaticoService = viaticoService;
            }

            public async Task<Response<int>> Handle(UpdateEstatusViaticoCommand request, CancellationToken cancellationToken)
            {
                var viatico = await _repositoryAsyncViatico.GetByIdAsync(request.Id);
                if (viatico == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    viatico.Descripcion = request.Descripcion;
                    if (viatico.Estatus != request.Estatus && request.Estatus == EstatusViatico.Cerrado)
                    {
                        await _viaticoService.EnviarCorreoViatico(viatico);
                    }
                    viatico.Estatus = request.Estatus;
                    await _repositoryAsyncViatico.UpdateAsync(viatico);
                }

                return new Response<int>(viatico.Id);
            }
        }
    }
}
