using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.Companies;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Companies.Queries.GetAllCompaniesByEmployeeId
{
    public class GetAllCompaniesByEmployeeIdQuery : IRequest<Response<List<CompanyDTO>>>
    {

        public int EmployeeId { get; set; }

        public class GetAllCompanyByIdQueryHandler : IRequestHandler<GetAllCompaniesByEmployeeIdQuery, Response<List<CompanyDTO>>>
        {
            private readonly IRepositoryAsync<Company> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllCompanyByIdQueryHandler(IRepositoryAsync<Company> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<CompanyDTO>>> Handle(GetAllCompaniesByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new CompaniesByEmployeeIdSpecification(request.EmployeeId));

                var list_dto = _mapper.Map<List<CompanyDTO>>(list);

                return new Response<List<CompanyDTO>>(list_dto);

            }
        }

    }
}
