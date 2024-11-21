using Application.DTOs.Administracion;
using Application.DTOs.ReembolsosOperativos;
using Application.Interfaces;
using Application.Specifications.Catalogos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetReembolsosById
{
    public class GetReembolsoByIdQuery : IRequest<Response<ReembolsoDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetReembolsoByIdQuery, Response<ReembolsoDTO>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsync;
            private readonly IRepositoryAsync<TipoEstatusReembolso> _repositoryAsyncTipoEstatusReembolso;
            private readonly IMapper _mapper;
            private readonly IReembolsoService _reembolsoService;
            private readonly IRepositoryAsync<User> _repositoryAsyncUser;

            public Handler(IRepositoryAsync<Reembolso> repositoryAsync, IMapper mapper, IRepositoryAsync<TipoEstatusReembolso> repositoryAsyncTipoEstatusReembolso, IReembolsoService reembolsoService, IRepositoryAsync<User> repositoryAsyncUser)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncTipoEstatusReembolso = repositoryAsyncTipoEstatusReembolso;
                _reembolsoService = reembolsoService;
                _repositoryAsyncUser = repositoryAsyncUser;
            }

            public async Task<Response<ReembolsoDTO>> Handle(GetReembolsoByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                var usuarios = await _repositoryAsyncUser.ListAsync();
                Dictionary<int, string> diccionarioUsuarios = usuarios.ToDictionary(u => u.Id, u => u.UserName);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var estatus = await _repositoryAsyncTipoEstatusReembolso.GetByIdAsync(elem.EstatusId);

                if (estatus == null)
                {
                    throw new KeyNotFoundException($"no se encontro el estatus con el id {elem.EstatusId}");
                }

                var dto = _mapper.Map<ReembolsoDTO>(elem);

                if (elem.EstatusId == 0)
                {
                    dto.Estatus = "Sin Estatus";
                }
                else
                {
                    dto.Estatus = estatus.Descripcion;
                }

                dto.Monto = await _reembolsoService.CalcularMontoTotalReembolso(dto.Id);
                dto.UsuarioName = diccionarioUsuarios[(int)dto.UsuarioIdPago];

                
                return new Response<ReembolsoDTO>(dto);
                
            }
        }

    }
}
