using Application.DTOs.Administracion;
using Application.Interfaces;
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

namespace Application.Feautres.Administracion.Companies.Queries.GetAllCompanyQuery
{
    public class GetAllCompanyQuery : IRequest<Response<List<CompanyDTO>>>
    {

        public class GetAllCompanyByIdQueryHandler : IRequestHandler<GetAllCompanyQuery, Response<List<CompanyDTO>>>
        {
            private readonly IRepositoryAsync<Company> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllCompanyByIdQueryHandler(IRepositoryAsync<Company> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<CompanyDTO>>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
            {
                var cliente = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<CompanyDTO>>(cliente);
                return new Response<List<CompanyDTO>>(dto);

            }
        }
    }
}
