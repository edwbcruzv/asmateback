using Application.DTOs.ReembolsosOperativos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.ReembolsosOperativos.MovimientoReembolsos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading.Tasks.Dataflow;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Queries.GetAllMovimientosReembolso
{
    public class GetAllMovimientosReembolsoByReembolsoId : IRequest<Response<List<MovimientoReembolsoDTO>>>
    {
        public int ReembolsoId { get; set; }

        public class Handler : IRequestHandler<GetAllMovimientosReembolsoByReembolsoId, Response<List<MovimientoReembolsoDTO>>>
        {
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IRepositoryAsync<TipoReembolso> _repositoryAsyncTipoReembolso;
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncTipoMoneda;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<TipoImpuesto> _repositoryAsyncTipoImpuesto;

            public Handler(
                    IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
                    IRepositoryAsync<TipoReembolso> repositoryAsyncTipoReembolso,
                    IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
                    IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda,
                    IMapper mapper
,
                    IRepositoryAsync<TipoImpuesto> repositoryAsyncTipoImpuesto)
            {
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
                _repositoryAsyncTipoReembolso = repositoryAsyncTipoReembolso;
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _repositoryAsyncTipoMoneda = repositoryAsyncTipoMoneda;
                _mapper = mapper;
                _repositoryAsyncTipoImpuesto = repositoryAsyncTipoImpuesto;
            }

            // ... (código existente)

            public async Task<Response<List<MovimientoReembolsoDTO>>> Handle(GetAllMovimientosReembolsoByReembolsoId request, CancellationToken cancellationToken)
            {
                var reembolso = await _repositoryAsyncReembolso.GetByIdAsync(request.ReembolsoId);

                if (reembolso == null)
                {
                    throw new ApplicationException($"No se encontro el reembolso con el Id {request.ReembolsoId}.");
                }
                else
                {
                    var list_mov_reembolso = await _repositoryAsyncMovimientoReembolso.ListAsync(new MovimientoReembolsoByReembolsoIdSpecification(request.ReembolsoId));

                    var list_mov_reembolso_dto = new List<MovimientoReembolsoDTO>();

                    var list_tipo_moneda = await _repositoryAsyncTipoMoneda.ListAsync();

                    Dictionary <int,string> diccionarioTipoMoneda = list_tipo_moneda.ToDictionary(c => c.Id, c => c.CodigoIso);

                    var list_tipo_reembolso = await _repositoryAsyncTipoReembolso.ListAsync();

                    Dictionary<int, string> diccionarioTipoReembolso = list_tipo_reembolso.ToDictionary(c => c.Id, c => c.Descripcion);

                    var list_tipo_impuesto = await _repositoryAsyncTipoImpuesto.ListAsync();

                    Dictionary<int, string> diccionarioImpuesto = list_tipo_impuesto.ToDictionary(c => c.Id, c => c.Descripcion);


                    foreach (var elem in list_mov_reembolso)
                    {
                        var dto = _mapper.Map<MovimientoReembolsoDTO>(elem);
                        dto.TipoReembolso = diccionarioTipoReembolso[(int)elem.TipoReembolsoId];
                        if (elem.TipoMonedaId != null)
                        { 
                            dto.TipoMoneda = diccionarioTipoMoneda[(int)elem.TipoMonedaId];
                        }
                        if(elem.TipoImpuestoId != null)
                        {
                            dto.TipoImpuesto = diccionarioImpuesto[(int)elem.TipoImpuestoId];
                        }
                        else
                        {
                            dto.TipoImpuesto = "";
                        }
                        list_mov_reembolso_dto.Add(dto);
                    }

                    return new Response<List<MovimientoReembolsoDTO>>(list_mov_reembolso_dto);
                }
            }
        }
    }
}

