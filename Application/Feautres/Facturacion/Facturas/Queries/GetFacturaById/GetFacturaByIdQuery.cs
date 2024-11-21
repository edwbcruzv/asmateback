using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications.Facturas;
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

namespace Application.Feautres.Facturacion.Facturas.Queries.GetFacturaById
{
    public class GetFacturaByIdQuery : IRequest<Response<FacturaDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetFacturaByIdQuery, Response<FacturaDto>>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsync;
            private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsyncFacturaMovimiento;
            private readonly ITotalesMovsService _totalesMovsService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Factura> repositoryAsync, IMapper mapper, 
                IRepositoryAsync<FacturaMovimiento> repositoryAsyncFacturaMovimiento, ITotalesMovsService totalesMovsService)
            {
                _repositoryAsync = repositoryAsync;
                _totalesMovsService = totalesMovsService;
                _repositoryAsyncFacturaMovimiento = repositoryAsyncFacturaMovimiento;
                _mapper = mapper;
            }

            public async Task<Response<FacturaDto>> Handle(GetFacturaByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                if (item == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<FacturaDto>(item);

                    var movs = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(item.Id));
                    var tm = _totalesMovsService.getTotalesFormMovs(movs);

                    dto.MontoTotal = (double)tm.total;

                    return new Response<FacturaDto>(dto);
                }

            }
        }
    }
}
