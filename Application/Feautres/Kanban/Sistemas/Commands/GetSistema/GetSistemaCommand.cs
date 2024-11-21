using Application.DTOs.Kanban.Sistemas;
using Application.Feautres.Kanban.Sistemas.Queries.GetSistemaById;
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

namespace Application.Feautres.Kanban.Sistemas.Commands.GetSistema
{
    public class GetSistemaCommand : IRequest<Response<List<SistemaDTO>>>
    {
        public class Handler : IRequestHandler<GetSistemaCommand, Response<List<SistemaDTO>>>
        {
            private readonly IRepositoryAsync<Sistema> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;

            public Handler(IRepositoryAsync<Sistema> repositoryAsync, IMapper mapper, IRepositoryAsync<Estado> repositoryAsyncEstado)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncEstado = repositoryAsyncEstado;
            }

            public async Task<Response<List<SistemaDTO>>> Handle(GetSistemaCommand request, CancellationToken cancellationToken)
            {
                var sistemas = await _repositoryAsync.ListAsync();
                var estados = await _repositoryAsyncEstado.ListAsync();
                Dictionary<int, string> diccionarioEstados = estados.ToDictionary(c => c.Id, c => c.Nombre);
                var sistemasDTO = _mapper.Map<List<SistemaDTO>>(sistemas);

                foreach (var item in sistemasDTO)
                {
                    item.Estado = diccionarioEstados[(int)item.EstadoId];
                }
                return new Response<List<SistemaDTO>>(sistemasDTO);
            }
        }
    }
}
