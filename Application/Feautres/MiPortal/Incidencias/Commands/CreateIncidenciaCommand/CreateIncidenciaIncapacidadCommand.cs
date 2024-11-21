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

namespace Application.Feautres.MiPortal.Incidencias.Commands.CreateIncidenciaCommand
{
    public class CreateIncidenciaIncapacidadCommand : IRequest<Response<int>>
    {
        public int EmpleadoId { get; set; }
        public int TipoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Motivo { get; set; }

        public class Hamdler : IRequestHandler<CreateIncidenciaIncapacidadCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IPdfService _pdfService;

            public Hamdler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencias, IMapper mapper, IRepositoryAsync<Employee> repositoryAsyncEmployee, IPdfService pdfService)
            {
                _repositoryAsyncIncidencias = repositoryAsyncIncidencias;
                _mapper = mapper;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _pdfService = pdfService;
            }
            public async Task<Response<int>> Handle(CreateIncidenciaIncapacidadCommand request, CancellationToken cancellationToken)
            {

                Employee employee = await _repositoryAsyncEmployee.GetByIdAsync(request.EmpleadoId);

                if (employee == null)
                {
                    throw new KeyNotFoundException($"No se encontró el empleado con Id {request.EmpleadoId}");
                }
                else
                {
                    Incidencia nuevaIncidencia = _mapper.Map<Incidencia>(request);
                    nuevaIncidencia.EstatusId = 1;
                    nuevaIncidencia.CompanyId = employee.CompanyId;
                    TimeSpan ts = request.FechaFin - request.FechaInicio;
                    double diasDecimales = Math.Round(ts.TotalDays, 0);
                    int dias = Convert.ToInt32(diasDecimales);
                    nuevaIncidencia.Dias = dias;

                    var incidenciaCreada = await _repositoryAsyncIncidencias.AddAsync(nuevaIncidencia);

                    await _pdfService.PdfIncidencia(incidenciaCreada.Id);
                    
                    Response<int> respuesta = new Response<int>();
                    respuesta.Succeeded = true;
                    respuesta.Message = "La incidencia se ha creado correctamente";
                    respuesta.Data = incidenciaCreada.Id;

                    return respuesta;

                }



            }
        }
    }
}
