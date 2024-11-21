using Application.DTOs.MiPortal.Viaticos;
using Application.Interfaces;
using Application.Specifications.MiPortal.Viaticos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Viaticos.Queries.GetViaticosByCompanyId
{
    public class GetViaticosByCompanyIdQuery : IRequest<Response<List<ViaticoDTO>>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetViaticosByCompanyIdQuery, Response<List<ViaticoDTO>>>
        {
            private readonly IRepositoryAsync<Viatico> _repositoryAsync;
            private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IViaticoService _viaticoService;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;

            public Handler(IRepositoryAsync<Viatico> repositoryAsync, IRepositoryAsync<Banco> repositoryAsyncBanco, 
                IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee, 
                IMapper mapper, IViaticoService viaticoService, IRepositoryAsync<Estado> repositoryAsyncEstado)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncBanco = repositoryAsyncBanco;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _mapper = mapper;
                _viaticoService = viaticoService;
                _repositoryAsyncEstado = repositoryAsyncEstado;
            }

            public async Task<Response<List<ViaticoDTO>>> Handle(GetViaticosByCompanyIdQuery request, CancellationToken cancellationToken)
            {
                var list_viaticos = await _repositoryAsync.ListAsync(new ViaticoByCompanyIdSpecification(request.Id));

                //var list_employees = await _repositoryAsyncEmployee.ListAsync();
                //Dictionary<int, string> dicc_employees = list_employees.ToDictionary(x => x.Id, x => x.Nombre);

                //var list_bancos = await _repositoryAsyncBanco.ListAsync();
                //Dictionary<int, string> dicc_bancos = list_bancos.ToDictionary(x => x.Id, x => x.Nombre);

                var list_company = await _repositoryAsyncCompany.ListAsync();
                Dictionary<int, string> dicc_company = list_company.ToDictionary(x => x.Id, x => x.Rfc);

                var list_estados = await _repositoryAsyncEstado.ListAsync();
                Dictionary<int, string> dicc_estados = list_estados.ToDictionary(x => x.Id, x => x.Nombre);

                var list_dto = new List<ViaticoDTO>();

                foreach (var item in list_viaticos)
                {
                    var dto = _mapper.Map<ViaticoDTO>(item);
                    //dto.Employee = dicc_employees[(int)item.EmployeeId];
                    //dto.EmployeePago = dicc_employees[(int)item.EmployeePagoId];
                    dto.Company = dicc_company[(int)item.CompanyId];
                    //dto.Banco = dicc_bancos[(int)item.BancoId];
                    var totalexc = await _viaticoService.CalcularMontoTotalViatico(item.Id);
                    dto.MontoExcedente = item.MontoRecibido - totalexc;
                    dto.EstatusId = (int)item.Estatus;
                    dto.Estado = dicc_estados[(int)item.EstadoId];
                    list_dto.Add(dto);

                }

                return new Response<List<ViaticoDTO>>(list_dto);
            }
        }

    }
}
