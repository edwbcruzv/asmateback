using Application.DTOs.Catalogos;
using Application.Interfaces;
using Application.Specifications;
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

namespace Application.Feautres.Catalogos.CveProductos.Queries.GetCveProductoById
{
    public class GetCveProductoByIdQuery : IRequest<Response<CveProductoDto>>
    {
        public int Id { get; set; }
        public class GetCveProductoByEstatusHandler : IRequestHandler<GetCveProductoByIdQuery, Response<CveProductoDto>>
        {
            private readonly IRepositoryAsync<CveProducto> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCveProductoByEstatusHandler(IRepositoryAsync<CveProducto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CveProductoDto>> Handle(GetCveProductoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<CveProductoDto>(item);

                return new Response<CveProductoDto>(dto);

            }
        }
    }
}
