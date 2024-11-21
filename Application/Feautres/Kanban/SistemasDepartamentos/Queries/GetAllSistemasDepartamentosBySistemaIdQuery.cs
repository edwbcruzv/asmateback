using Application.DTOs.Administracion;
using Application.DTOs.Kanban.SistemasDepartamentos;
using Application.Interfaces;
using Application.Specifications.Kanban.SistemasDepartamentos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.SistemasDepartamentos.Queries
{
     public class GetAllSistemasDepartamentosBySistemaIdQuery : IRequest<Response<List<DepartamentoDto>>>
    {

        public int SistemaId { get; set; }

        public class Handler : IRequestHandler<GetAllSistemasDepartamentosBySistemaIdQuery, Response<List<DepartamentoDto>>>
        {
            private readonly IRepositoryAsync<SistemaDepartamento> _repositoryAsync;
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;

            public Handler(IRepositoryAsync<SistemaDepartamento> repositoryAsync, IMapper mapper, 
                IRepositoryAsync<Departamento> repositoryAsyncDepartamento, IRepositoryAsync<Company> repositoryAsyncCompany)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
                _repositoryAsyncCompany = repositoryAsyncCompany;
            }

            public async Task<Response<List<DepartamentoDto>>> Handle(GetAllSistemasDepartamentosBySistemaIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new SistemaDepartamentoBySistemaIdSpecification(request.SistemaId));
                var companies = await _repositoryAsyncCompany.ListAsync();
                Dictionary<int, string> diccionarioCompanias = companies.ToDictionary(c => c.Id, c => c.Name);
                List<Departamento> departamentos = new List<Departamento>();
                foreach (var item in list)
                {
                    departamentos.Add(await _repositoryAsyncDepartamento.GetByIdAsync(item.DepartamentoId));
                }

                var list_dto = _mapper.Map<List<DepartamentoDto>>(departamentos);

                foreach (var item in list_dto)
                {
                    item.Company = diccionarioCompanias[item.CompanyId];
                }

                return new Response<List<DepartamentoDto>>(list_dto);
            }
        }
    }
}
