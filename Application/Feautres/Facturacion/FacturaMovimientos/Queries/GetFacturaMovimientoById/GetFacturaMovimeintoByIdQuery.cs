using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.FacturaMovimientos.Queries.GetFacturaMovimientoById
{
    public class GetFacturaMovimientoByIdQuery : IRequest<Response<FacturaMovimientoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetFacturaMovimientoByIdQuery, Response<FacturaMovimientoDto>>
        {
            private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<FacturaMovimiento> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<FacturaMovimientoDto>> Handle(GetFacturaMovimientoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                if (item == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<FacturaMovimientoDto>(item);
                    return new Response<FacturaMovimientoDto>(dto);
                }

            }
        }
    }
}
