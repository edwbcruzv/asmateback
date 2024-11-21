using Application.DTOs.Administracion;
using Application.DTOs.Catalogos;
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

namespace Application.Feautres.Catalogos.TipoPeriocidadPagos.Queries.GetTipoPeriocidadPagoById
{
    public class GetTipoPeriocidadPagoByIDQuery : IRequest<Response<TipoPeriocidadPagoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoPeriocidadPagoByIDQuery, Response<TipoPeriocidadPagoDto>>
        {
            private readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoPeriocidadPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;

            }
            public async Task<Response<TipoPeriocidadPagoDto>> Handle(GetTipoPeriocidadPagoByIDQuery request, CancellationToken cancellationToken)
            {
                var registro = await _repositoryAsync.GetByIdAsync(request.Id);
                if (registro == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<TipoPeriocidadPagoDto>(registro);
                    return new Response<TipoPeriocidadPagoDto>(dto);
                }

            }
        }
    }
}
