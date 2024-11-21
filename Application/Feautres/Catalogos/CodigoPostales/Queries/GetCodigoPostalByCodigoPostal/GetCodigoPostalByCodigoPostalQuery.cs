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

namespace Application.Feautres.Catalogos.CodigoPostales.Queries.GetCodigoPostalByCodigoPostal
{
    public class GetCodigoPostalByCodigoPostalQuery : IRequest<Response<List<CodigoPostaleDto>>>
    {

        public string codigoPostalId;

        public class GetCodigoPostalByCodigoPostalHandler : IRequestHandler<GetCodigoPostalByCodigoPostalQuery, Response<List<CodigoPostaleDto>>>
        {
            private readonly IRepositoryAsync<CodigoPostale> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCodigoPostalByCodigoPostalHandler(IRepositoryAsync<CodigoPostale> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<CodigoPostaleDto>>> Handle(GetCodigoPostalByCodigoPostalQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new CodigoPostalByCodigoPostalSpecification(request.codigoPostalId));

                var dto = _mapper.Map<List<CodigoPostaleDto>>(list);

                return new Response<List<CodigoPostaleDto>>(dto);

            }
        }
    }
}
