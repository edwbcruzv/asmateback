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

namespace Application.Feautres.MiPortal.Incidencias.Queries.GetIncidenciasByCompanyCommand
{
    public class GetIncidenciasByCompanyCommand : IRequest<Response<List<IncidenciaDTO>>>
    {
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<GetIncidenciasByCompanyCommand, Response<List<IncidenciaDTO>>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencia;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<TipoIncidencia> _repositoryAsyncTipoIncidencia;
            private readonly IRepositoryAsync<TipoEstatusIncidencia> _repositoryAsyncTipoEstatusIncidencia;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;


            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencia, IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<TipoIncidencia> repositoryAsyncTipoIncidencia, IRepositoryAsync<TipoEstatusIncidencia> repositoryAsyncTipoEstatusIncidencia, IMapper mapper, IRepositoryAsync<Employee> repositoryAsyncEmployee)
            {
                _repositoryAsyncIncidencia = repositoryAsyncIncidencia;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _repositoryAsyncTipoIncidencia = repositoryAsyncTipoIncidencia;
                _repositoryAsyncTipoEstatusIncidencia = repositoryAsyncTipoEstatusIncidencia;
                _mapper = mapper;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
            }
            public async Task<Response<List<IncidenciaDTO>>> Handle(GetIncidenciasByCompanyCommand request, CancellationToken cancellationToken)
            {
                Company company = await _repositoryAsyncCompany.GetByIdAsync(request.CompanyId);
                if (company == null)
                {
                    throw new KeyNotFoundException($"No se encontró la compañia con Id {request.CompanyId}");
                }
                else
                {
                    List<Incidencia> lista_incidencias_company = await _repositoryAsyncIncidencia.ListAsync(new IncidenciasByCompanyIdSpecification(request.CompanyId));

                    if (lista_incidencias_company.Count == 0)
                    {
                        throw new ApiException($"La compañia con Id {request.CompanyId} no tiene incidencias registradas");
                    }
                    else
                    {
                        List<IncidenciaDTO> lista_incidencias_dto_company = new List<IncidenciaDTO>();
                        var lista_tipo_incidencias = await _repositoryAsyncTipoIncidencia.ListAsync();
                        var lista_tipo_estatus_incidencias = await _repositoryAsyncTipoEstatusIncidencia.ListAsync();

                        Dictionary<int, string> diccionarioTipoIncidencias = new Dictionary<int, string>();
                        Dictionary<int, string> diccionarioTipoEstatusIncidencias = new Dictionary<int, string>();

                        diccionarioTipoIncidencias = lista_tipo_incidencias.ToDictionary(x => x.Id, x => x.Descripcion);
                        diccionarioTipoEstatusIncidencias = lista_tipo_estatus_incidencias.ToDictionary(x => x.Id, x => x.Descripcion);
                        foreach (Incidencia incidencia in lista_incidencias_company)
                        {
                            IncidenciaDTO incidencia_dto = _mapper.Map<IncidenciaDTO>(incidencia);
                            var employee = await _repositoryAsyncEmployee.GetByIdAsync(incidencia.EmpleadoId);
                            incidencia_dto.EmpleadoNombre = employee.NombreCompleto();
                            incidencia_dto.Tipo = diccionarioTipoIncidencias[incidencia.TipoId];
                            incidencia_dto.Estatus = diccionarioTipoEstatusIncidencias[incidencia.EstatusId];

                            if (incidencia_dto.ArchivoSrc != null)
                            {
                                incidencia_dto.ArchivoSrc = incidencia_dto.ArchivoSrc.Split(@"C:\").Last();
                            }

                            lista_incidencias_dto_company.Add(incidencia_dto);
                        }

                        Response<List<IncidenciaDTO>> respuesta = new Response<List<IncidenciaDTO>>();
                        respuesta.Succeeded = true;
                        respuesta.Data = lista_incidencias_dto_company;

                        return respuesta;

                    }
                }
            }
        }
    }
}
