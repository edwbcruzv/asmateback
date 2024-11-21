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

namespace Application.Feautres.Catalogos.RegimenFiscals.Queries.GetAllRegimenFiscal
{
    public class GetAllRegimenFiscalQuery : IRequest<Response<List<RegimenFiscalDto>>>
    {     
        public class GetAllRegimenFiscalHandler : IRequestHandler<GetAllRegimenFiscalQuery, Response<List<RegimenFiscalDto>>>
        {
            private readonly IRepositoryAsync<RegimenFiscal> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllRegimenFiscalHandler(IRepositoryAsync<RegimenFiscal> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<RegimenFiscalDto>>> Handle(GetAllRegimenFiscalQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();
                
                var dto = _mapper.Map<List<RegimenFiscalDto>>(list);

                return new Response<List<RegimenFiscalDto>>(dto);                

            }
        }
    }
}
