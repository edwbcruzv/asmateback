using Application.DTOs.MiPortal.Comprobantes;
using Application.Interfaces;
using Application.Specifications.MiPortal.Comprobantes;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Queries.GetAllComprobantes
{
    public class GetComprobantesByViaticoQuery : IRequest<Response<List<ComprobanteDTO>>>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetComprobantesByViaticoQuery, Response<List<ComprobanteDTO>>>
        {
            private readonly IRepositoryAsync<Comprobante> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncTipoMoneda;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsync, IMapper mapper, IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncTipoMoneda = repositoryAsyncTipoMoneda;
            }

            public async Task<Response<List<ComprobanteDTO>>> Handle(GetComprobantesByViaticoQuery request, CancellationToken cancellationToken)
            {
                var list_tipo_moneda = await _repositoryAsyncTipoMoneda.ListAsync();

                Dictionary<int, string> diccionarioTipoMoneda = list_tipo_moneda.ToDictionary(c => c.Id, c => c.CodigoIso);

                var list = await _repositoryAsync.ListAsync(new ComprobanteByViaticoIdSpecification(request.Id));

                var list_dto = _mapper.Map<List<ComprobanteDTO>>(list);

                foreach (var item in list_dto)
                {
                    if (item.TipoMonedaId == 115 || item.TipoMonedaId == null)
                    {
                        item.Moneda = "MXN";
                    }
                    else
                    {
                        item.Moneda = diccionarioTipoMoneda[(int)item.TipoMonedaId];
                    }
                }

                return new Response<List<ComprobanteDTO>>(list_dto);
            }
        }
    }
}
