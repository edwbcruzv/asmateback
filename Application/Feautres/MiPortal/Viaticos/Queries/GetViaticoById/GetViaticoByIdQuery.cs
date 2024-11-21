using Application.DTOs.MiPortal.Viaticos;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Viaticos.Queries.GetViaticoById
{
    public class GetViaticoByIdQuery : IRequest<Response<ViaticoDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetViaticoByIdQuery, Response<ViaticoDTO>>
        {
            private readonly IRepositoryAsync<Viatico> _repositoryAsync;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;

            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Viatico> repositoryAsync, IMapper mapper, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Banco> repositoryAsyncBanco)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _repositoryAsyncBanco = repositoryAsyncBanco;
            }

            public async Task<Response<ViaticoDTO>> Handle(GetViaticoByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                var employee = await _repositoryAsyncEmployee.GetByIdAsync(elem.EmployeeId);
                var employee_pago = await _repositoryAsyncEmployee.GetByIdAsync(elem.EmployeePagoId);
                var company = await _repositoryAsyncCompany.GetByIdAsync(elem.CompanyId);
                var banco = await _repositoryAsyncBanco.GetByIdAsync(elem.BancoId);


                var dto = _mapper.Map<ViaticoDTO>(elem);
                dto.EstatusId = (int)elem.Estatus;
                //dto.Employee = employee.Nombre;
                //dto.EmployeePago = employee_pago.Nombre;
                //dto.Company = company.Name;
                //dto.Banco = banco.Nombre;

                return new Response<ViaticoDTO>(dto,"Viatico encontrado con exito.");

            }
        }

    }
}
