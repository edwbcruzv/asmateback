using Application.DTOs.MiPortal.Incidencias;
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

namespace Application.Feautres.MiPortal.Incidencias.Queries.GetIncidenciaByIdCommand
{
    public class GetIncidenciaByIdCommand : IRequest<Response<IncidenciaDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetIncidenciaByIdCommand, Response<IncidenciaDTO>>
        {
            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencia;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<TipoIncidencia> _repositoryAsyncTipoIncidencias;
            private readonly IRepositoryAsync<TipoEstatusIncidencia> _repositoryAsyncTipoEstatusIncidencia;

            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencia, IMapper mapper, IRepositoryAsync<TipoIncidencia> repositoryAsyncTipoIncidencias, IRepositoryAsync<TipoEstatusIncidencia> repositoryAsyncTipoEstatusIncidencia)
            {
                _repositoryAsyncIncidencia = repositoryAsyncIncidencia;
                _mapper = mapper;
                _repositoryAsyncTipoIncidencias = repositoryAsyncTipoIncidencias;
                _repositoryAsyncTipoEstatusIncidencia = repositoryAsyncTipoEstatusIncidencia;
            }

            public async Task<Response<IncidenciaDTO>> Handle(GetIncidenciaByIdCommand request, CancellationToken cancellationToken)
            {
                Incidencia incidencia = await _repositoryAsyncIncidencia.GetByIdAsync(request.Id);

                if (incidencia == null)
                {
                    throw new KeyNotFoundException($"No se encontró la incidencia con Id {request.Id}");
                }
                else
                {
                    var listaTiposIncidencia = await _repositoryAsyncTipoIncidencias.ListAsync();
                    var listaEstatusIncidencia = await _repositoryAsyncTipoEstatusIncidencia.ListAsync();
                    Dictionary<int, string> diccionarioTiposIncidencia = listaTiposIncidencia.ToDictionary(x => x.Id, x => x.Descripcion);
                    Dictionary<int, string> diccionarioEstatusIncidencia = listaEstatusIncidencia.ToDictionary(x => x.Id, x => x.Descripcion);
                    string Tipo = diccionarioTiposIncidencia[incidencia.TipoId];
                    string Estatus = diccionarioEstatusIncidencia[incidencia.EstatusId];
                    IncidenciaDTO incidenciaDTO = _mapper.Map<IncidenciaDTO>(incidencia);
                    incidenciaDTO.Tipo = Tipo;
                    incidenciaDTO.Estatus = Estatus;
                    Response<IncidenciaDTO> respuesta = new Response<IncidenciaDTO>();

                    respuesta.Succeeded = true;
                    respuesta.Data = incidenciaDTO;

                    return respuesta;

                }
            }
        }
    }
    
}
