using Application.DTOs.MiPortal.Incidencias;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Commands.CreateIncidenciaCommand
{ 
    public class CreateIncidenciaVacacionesCommand : IRequest<Response<int>>
    {
        public int EmpleadoId {  get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Motivo { get; set; }

        public class Hamdler : IRequestHandler<CreateIncidenciaVacacionesCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IPdfService _pdfService;
            private readonly IRegistroAsistenciaServices _registroAsistenciaService;
            private readonly INominaService _nominaService;

            public Hamdler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencias, IMapper mapper, IRepositoryAsync<Employee> repositoryAsyncEmployee, IPdfService pdfService, IRegistroAsistenciaServices registroAsistenciaService, INominaService nominaService)
            {
                _repositoryAsyncIncidencias = repositoryAsyncIncidencias;
                _mapper = mapper;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _pdfService = pdfService;
                _registroAsistenciaService = registroAsistenciaService;
                _nominaService = nominaService;
            }
            public async Task<Response<int>> Handle(CreateIncidenciaVacacionesCommand request, CancellationToken cancellationToken)
            {

                var employee = await _repositoryAsyncEmployee.GetByIdAsync(request.EmpleadoId);

                if (employee == null)
                {
                    throw new KeyNotFoundException($"No se encontró el empleado con Id {request.EmpleadoId}");
                }
                else
                {
                    
                    int dias = await _registroAsistenciaService.calcularDiasVacaciones(request.FechaInicio, request.FechaFin);
                    int aniosTrabajados = _nominaService.anioEntreDosFechas((DateTime)employee.FechaContrato,DateTime.Now);
                    DiasIncidenciaDto diasIncidenciaDto = await _registroAsistenciaService.getDiasIncidenciaByEmployee(employee.Id, aniosTrabajados);
                    Response<int> respuesta = new Response<int>();
                   

                    if (diasIncidenciaDto.diasVacaciones < dias)
                    {
                        respuesta.Succeeded = false;
                        respuesta.Message = "No puedes registrar esas vacaciones porque no cuentas con los dias suficientes";
                        respuesta.Data = -1;
                        return respuesta;
                    }
                    else
                    {
                        Incidencia nuevaIncidencia = _mapper.Map<Incidencia>(request);
                        nuevaIncidencia.TipoId = 1;
                        nuevaIncidencia.EstatusId = 1;
                        nuevaIncidencia.CompanyId = employee.CompanyId;
                        nuevaIncidencia.Dias = dias;

                        var incidenciaCreada = await _repositoryAsyncIncidencias.AddAsync(nuevaIncidencia);

                        await _pdfService.PdfVacaciones(incidenciaCreada.Id,aniosTrabajados,diasIncidenciaDto.diasVacaciones);
                        
                        respuesta.Succeeded = true;
                        respuesta.Message = "Las vacaciones se han registrado correctamente";
                        respuesta.Data = incidenciaCreada.Id;

                        return respuesta;
                    }
                }
                  
            }
        }
    }
}
