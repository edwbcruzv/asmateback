using Application.DTOs.MiPortal.Incidencias;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.MiPortal;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Queries.GetIncidenciasByEmployeeCommand
{
    public class GetIncidenciasByEmployeeCommand : IRequest<Response<List<IncidenciaDTO>>>
    {
       public int EmployeeId { get; set; }

        public class Handler : IRequestHandler<GetIncidenciasByEmployeeCommand, Response<List<IncidenciaDTO>>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployees;
            private readonly IRepositoryAsync<TipoIncidencia> _repositoryAsyncTipoIncidencia;
            private readonly IRepositoryAsync<TipoEstatusIncidencia> _repositoryAsyncTipoEstatusIncidencia;
            private readonly IMapper _mapper;
            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencias, IRepositoryAsync<Employee> repositoryAsyncEmployees, IRepositoryAsync<TipoIncidencia> repositoryAsyncTipoIncidencia, IRepositoryAsync<TipoEstatusIncidencia> repositoryAsyncTipoEstatusIncidencia, IMapper mapper)
            {
                _repositoryAsyncIncidencias = repositoryAsyncIncidencias;
                _repositoryAsyncEmployees = repositoryAsyncEmployees;
                _repositoryAsyncTipoIncidencia = repositoryAsyncTipoIncidencia;
                _repositoryAsyncTipoEstatusIncidencia = repositoryAsyncTipoEstatusIncidencia;
                _mapper = mapper;
            }

            public async Task<Response<List<IncidenciaDTO>>> Handle(GetIncidenciasByEmployeeCommand request, CancellationToken cancellationToken)
            {
                var lista_incidencias_employee = await _repositoryAsyncIncidencias.ListAsync(new IncidenciasByEmployeeIdSpecification(request.EmployeeId));
                if (lista_incidencias_employee.Count == 0)
                {
                    throw new ApiException($"El empleado con Id {request.EmployeeId} no tiene incidencias registradas");
                }
                else
                {
                    List<IncidenciaDTO> lista_incidencias_dto_employee = new List<IncidenciaDTO>();
                    var lista_tipo_incidencias = await _repositoryAsyncTipoIncidencia.ListAsync();
                    var lista_tipo_estatus_incidencias = await _repositoryAsyncTipoEstatusIncidencia.ListAsync();

                    Dictionary<int,string> diccionarioTipoIncidencias = new Dictionary<int,string>();
                    Dictionary<int,string> diccionarioTipoEstatusIncidencias = new Dictionary<int,string>();

                    diccionarioTipoIncidencias = lista_tipo_incidencias.ToDictionary(x => x.Id, x => x.Descripcion);
                    diccionarioTipoEstatusIncidencias = lista_tipo_estatus_incidencias.ToDictionary(x => x.Id, x => x.Descripcion);
                    foreach (Incidencia incidencia in lista_incidencias_employee)
                    {
                        IncidenciaDTO incidencia_dto = _mapper.Map<IncidenciaDTO>(incidencia);
                        incidencia_dto.Tipo = diccionarioTipoIncidencias[incidencia.TipoId];
                        incidencia_dto.Estatus = diccionarioTipoEstatusIncidencias[incidencia.EstatusId];

                        if (incidencia_dto.ArchivoSrc != null)
                        {
                            incidencia_dto.ArchivoSrc = incidencia_dto.ArchivoSrc.Split(@"C:\").Last();
                        }

                        lista_incidencias_dto_employee.Add(incidencia_dto);
                    }

                    Response<List<IncidenciaDTO>> respuesta = new Response<List<IncidenciaDTO>>();
                    respuesta.Succeeded = true;
                    respuesta.Data = lista_incidencias_dto_employee;

                    return respuesta;
                     
                }


            }
        }
    }
}
