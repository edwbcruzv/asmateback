using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.Facturas.Queries.GetAllFacturaByCompany
{
    public class GetAllFacturaByCompanyQuery : IRequest<Response<List<FacturaDto>>>
    {
        public int CompanyId { set; get; }

        public class Handler : IRequestHandler<GetAllFacturaByCompanyQuery, Response<List<FacturaDto>>>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
            private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsyncFacturaMovimiento;
            private readonly ITotalesMovsService _totalesMovsService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Factura> repositoryAsyncFactura,
                IMapper mapper, ITotalesMovsService totalesMovsService, 
                IRepositoryAsync<FacturaMovimiento> repositoryAsyncFacturaMovimiento)
            {
                _repositoryAsyncFactura = repositoryAsyncFactura;
                _totalesMovsService = totalesMovsService;
                _repositoryAsyncFacturaMovimiento = repositoryAsyncFacturaMovimiento;
                _mapper = mapper;
            }

            public async Task<Response<List<FacturaDto>>> Handle(GetAllFacturaByCompanyQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsyncFactura.ListAsync(new FacturaByCompanySpecification(request.CompanyId));

                var facturasDto = new List<FacturaDto>();

                foreach (var item in list)
                {
                    var movs = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(item.Id));
                    var tm = _totalesMovsService.getTotalesFormMovs(movs);

                    var temp = _mapper.Map<FacturaDto>(item);

                    temp.MontoTotal = (double)tm.total;

                    facturasDto.Add(temp);

                }

                //var temp = _mapper.Map<List<FacturaDto>>(list);

                return new Response<List<FacturaDto>>(facturasDto);
            }
        }
    }
}
