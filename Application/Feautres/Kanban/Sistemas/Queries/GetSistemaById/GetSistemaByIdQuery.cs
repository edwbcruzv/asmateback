using Application.DTOs.Kanban.Sistemas;
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

namespace Application.Feautres.Kanban.Sistemas.Queries.GetSistemaById
{
    public class GetSistemaByIdQuery : IRequest<Response<SistemaDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetSistemaByIdQuery, Response<SistemaDTO>>
        {
            private readonly IRepositoryAsync<Sistema> _repositoryAsync;
            private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;

            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Sistema> repositoryAsync, IMapper mapper, IRepositoryAsync<Estado> repositoryAsyncEstado)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncEstado = repositoryAsyncEstado;
                _mapper = mapper;
            }

            public async Task<Response<SistemaDTO>> Handle(GetSistemaByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                var estado = await _repositoryAsyncEstado.GetByIdAsync(elem.EstadoId);

                var dto = _mapper.Map<SistemaDTO>(elem);
                dto.Estado = estado.Nombre;

                return new Response<SistemaDTO>(dto);

            }
        }

    }
}
