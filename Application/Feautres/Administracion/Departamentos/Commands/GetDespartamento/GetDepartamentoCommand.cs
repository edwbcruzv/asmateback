using Application.DTOs.Administracion;
using Application.Feautres.Administracion.Departamentos.Queries.GetAllDepartamentos;
using Application.Interfaces;
using Application.Specifications.Administracion;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Departamentos.Commands.GetDespartamento
{
    public class GetDepartamentoCommand : IRequest<Response<List<DepartamentoDto>>>
    {
        public class Handler : IRequestHandler<GetDepartamentoCommand, Response<List<DepartamentoDto>>>
        {
            private readonly IRepositoryAsync<Departamento> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;

            public Handler(IRepositoryAsync<Departamento> repositoryAsync, IMapper mapper, IRepositoryAsync<Company> repositoryAsyncCompany)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncCompany = repositoryAsyncCompany;
            }

            public async Task<Response<List<DepartamentoDto>>> Handle(GetDepartamentoCommand request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();
                var companies = await _repositoryAsyncCompany.ListAsync();
                Dictionary<int, string> diccionarioCompanias = companies.ToDictionary(c => c.Id, c => c.Name);

                var dto = _mapper.Map<List<DepartamentoDto>>(list);
                foreach (var item in dto)
                {
                    item.Company = diccionarioCompanias[item.CompanyId];
                }
                return new Response<List<DepartamentoDto>>(dto);
            }
        }
    }
}
