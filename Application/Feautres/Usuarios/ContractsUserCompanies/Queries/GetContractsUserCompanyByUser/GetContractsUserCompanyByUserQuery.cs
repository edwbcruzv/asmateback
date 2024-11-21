using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.ContracsUsers;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Feautres.Usuarios.ContractsUserCompanies.Queries.GetContractsUserCompanyByUser
{
    public class GetContractsUserCompanyByUserQuery : IRequest<Response<List<CompanyDTO>>>
    {
        public int UserId { get; set; }

        public class GetContractsUserCompanyByUserQueryHandler : IRequestHandler<GetContractsUserCompanyByUserQuery, Response<List<CompanyDTO>>>
        {
            private readonly IRepositoryAsync<ContractsUserCompany> _repositoryContractsUserCompanyAsync;
            private readonly IRepositoryAsync<Company> _repositoryCompanyAsync;
            private readonly IMapper _mapper;

            public GetContractsUserCompanyByUserQueryHandler(
                IRepositoryAsync<ContractsUserCompany> repositoryContractsUserCompanyAsync,
                IRepositoryAsync<Company> repositoryCompanyAsync,
                IMapper mapper)
            {
                _repositoryCompanyAsync = repositoryCompanyAsync;
                _repositoryContractsUserCompanyAsync = repositoryContractsUserCompanyAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<CompanyDTO>>> Handle(GetContractsUserCompanyByUserQuery request, CancellationToken cancellationToken)
            {
                var contractsUserCompany = await _repositoryContractsUserCompanyAsync.ListAsync(new ContractsUserCompanyByUserSpecification(request.UserId));
                var listCompanyUser = new List<Company>();

                foreach (var temp in contractsUserCompany)
                {
                    var company = await _repositoryCompanyAsync.GetByIdAsync(temp.CompanyId);
                    listCompanyUser.Add(company);
                }

                var dto = _mapper.Map<List<CompanyDTO>>(listCompanyUser);
                return new Response<List<CompanyDTO>>(dto);

            }
        }
    }
}
