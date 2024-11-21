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

namespace Application.Feautres.Catalogos.RegimenFiscals.Queries.GetRegimenFiscalByEstatus
{
    public class GetRegimenFiscalByIdQuery : IRequest<Response<RegimenFiscalDto>>
    {   
        public int Id { get; set; }
        public class GetRegimenFiscalByEstatusHandler : IRequestHandler<GetRegimenFiscalByIdQuery, Response<RegimenFiscalDto>>
        {
            private readonly IRepositoryAsync<RegimenFiscal> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetRegimenFiscalByEstatusHandler(IRepositoryAsync<RegimenFiscal> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<RegimenFiscalDto>> Handle(GetRegimenFiscalByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);
                
                var dto = _mapper.Map<RegimenFiscalDto>(item);

                return new Response<RegimenFiscalDto>(dto);                

            }
        }
    }
}
