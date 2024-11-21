using Application.DTOs.Catalogos;
using Application.Interfaces;
using Application.Specifications.Catalogos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Catalogos.CveProductos.Queries.GetCveProductoByEstatus
{
    public class GetCveProductoByEstatusQuery : IRequest<Response<List<CveProductoDto>>>
    {
        public class GetCveProductoByEstatusHandler : IRequestHandler<GetCveProductoByEstatusQuery, Response<List<CveProductoDto>>>
        {
            private readonly IRepositoryAsync<CveProducto> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCveProductoByEstatusHandler(IRepositoryAsync<CveProducto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<CveProductoDto>>> Handle(GetCveProductoByEstatusQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new CveProductoByEstatusSpecification(true));

                var dto = _mapper.Map<List<CveProductoDto>>(list);

                return new Response<List<CveProductoDto>>(dto);

            }
        }
    }
}
