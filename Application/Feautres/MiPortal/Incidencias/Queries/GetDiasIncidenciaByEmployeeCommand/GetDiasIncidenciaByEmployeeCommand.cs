using Application.DTOs.Administracion;
using Application.DTOs.MiPortal.Incidencias;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Catalogos;
using Application.Specifications.MiPortal;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Queries.GetDiasIncidenciaByEmployeeCommand
{
    public class GetDiasIncidenciaByEmployeeCommand :IRequest<Response<DiasIncidenciaDto>>
    {
        public int EmpleadoId { get; set; }

        public class Handler : IRequestHandler<GetDiasIncidenciaByEmployeeCommand, Response<DiasIncidenciaDto>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IRegistroAsistenciaServices _registroAsistenciaServices;
            private readonly INominaService _nominaService;

            public Handler(IRepositoryAsync<Employee> repositoryAsyncEmployee, IRegistroAsistenciaServices registroAsistenciaServices, INominaService nominaService)
            {
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _registroAsistenciaServices = registroAsistenciaServices;
                _nominaService = nominaService;
            }
            public async Task<Response<DiasIncidenciaDto>> Handle(GetDiasIncidenciaByEmployeeCommand request, CancellationToken cancellationToken)
            {
                var employee = await _repositoryAsyncEmployee.GetByIdAsync(request.EmpleadoId);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"No se encontró el empleado con id {request.EmpleadoId}");
                }
                else
                {
                    int aniosTrabajados = _nominaService.anioEntreDosFechas((DateTime)employee.FechaContrato, DateTime.Now);
                    DiasIncidenciaDto diasIncidenciaDTO = await _registroAsistenciaServices.getDiasIncidenciaByEmployee(employee.Id, aniosTrabajados);     
                    Response<DiasIncidenciaDto> respuesta = new Response<DiasIncidenciaDto>();
                    respuesta.Succeeded = true;
                    respuesta.Data = diasIncidenciaDTO;
                    return respuesta;
                }
                  
            }
        }
    }
}
