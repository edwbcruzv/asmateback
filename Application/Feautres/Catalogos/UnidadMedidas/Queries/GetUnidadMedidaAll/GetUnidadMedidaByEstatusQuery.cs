﻿using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Specifications.Catalogos;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.UnidadMedidas.Queries.GetUnidadMedidaAll
{
    public class GetUnidadMedidaAllQuery : IRequest<Response<List<UnidadMedidaDto>>>
    {
        public class Handler : IRequestHandler<GetUnidadMedidaAllQuery, Response<List<UnidadMedidaDto>>>
        {
            private readonly IRepositoryAsync<UnidadMedida> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<UnidadMedida> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<UnidadMedidaDto>>> Handle(GetUnidadMedidaAllQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<UnidadMedidaDto>>(list);

                return new Response<List<UnidadMedidaDto>>(dto);

            }
        }
    }
}
