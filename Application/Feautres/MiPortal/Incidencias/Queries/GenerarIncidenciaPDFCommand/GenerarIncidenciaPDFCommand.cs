using Application.DTOs.MiPortal.Incidencias;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Queries.GenerarIncidenciaPDFCommand
{
    public class GenerarIncidenciaPDFCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GenerarIncidenciaPDFCommand,Response<string>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;
            private readonly IPdfService _pdfService;
            private readonly IRegistroAsistenciaServices _registroAsistenciaService;
            private readonly INominaService _nominaService;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencias, IPdfService pdfService, IRegistroAsistenciaServices registroAsistenciaService, INominaService nominaService, IRepositoryAsync<Employee> repositoryAsyncEmployee)
            {
                _repositoryAsyncIncidencias = repositoryAsyncIncidencias;
                _pdfService = pdfService;
                _registroAsistenciaService = registroAsistenciaService;
                _nominaService = nominaService;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
            }

            public async Task<Response<string>> Handle(GenerarIncidenciaPDFCommand request, CancellationToken cancellationToken)
            {

                var incidencia = await _repositoryAsyncIncidencias.GetByIdAsync(request.Id);
                var employee = await _repositoryAsyncEmployee.GetByIdAsync(incidencia.EmpleadoId);
                string rutaArchivo = "";
                // Si es incidencia de vacaciones se genera diferente
                if (incidencia.TipoId == 1)
                {
                    /* En este caso, en lugar de que se calculen los años generados con la fecha actual, se calcula con la fecha de creación para
                       devuelva los mismos datos que cuando se creó*/
                    int aniosTrabajados = _nominaService.anioEntreDosFechas((DateTime)employee.FechaContrato, (DateTime)incidencia.Created);
                    DiasIncidenciaDto diasIncidenciaDto = await _registroAsistenciaService.getDiasIncidenciaByEmployee(employee.Id, aniosTrabajados);
                    rutaArchivo = await _pdfService.PdfVacaciones(request.Id, aniosTrabajados, diasIncidenciaDto.diasVacaciones);
                }
                else
                {
                    rutaArchivo = await _pdfService.PdfIncidencia(request.Id);
                }
                
                Response<string> respuesta = new();
                respuesta.Data = rutaArchivo;
                respuesta.Succeeded = true;
                return respuesta;
                
            }
        }
    }
}
