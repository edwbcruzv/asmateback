using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications;
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

namespace Application.Feautres.Facturacion.Facturas.Queries.GetAllFactura
{
    public class GetAllFacturaQuery : IRequest<Response<List<FacturaDto>>>
    {
        public class Handler : IRequestHandler<GetAllFacturaQuery, Response<List<FacturaDto>>>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Factura> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<FacturaDto>>> Handle(GetAllFacturaQuery request, CancellationToken cancellationToken)
            {
                var Facturas = await _repositoryAsync.ListAsync();

                var FacturasDto = _mapper.Map<List<FacturaDto>>(Facturas);

                return new Response<List<FacturaDto>>(FacturasDto);
            }
        }
    }
}
