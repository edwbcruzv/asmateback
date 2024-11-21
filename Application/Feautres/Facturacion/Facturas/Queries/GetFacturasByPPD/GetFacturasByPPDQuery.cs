using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications;
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

namespace Application.Feautres.Facturacion.Facturas.Queries.GetAllFactura
{
    public class GetFacturasByPPDQuery : IRequest<Response<List<FacturaPDDDto>>>
    {

        public int CompanyId { set; get; }
        public int ClientId { set; get; }

        public class Handler : IRequestHandler<GetFacturasByPPDQuery, Response<List<FacturaPDDDto>>>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsync;
            private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsyncFacturaMovimiento;
            private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsyncComplementoPagoFactura;
            private readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
            private readonly ITotalesMovsService _totalesMovsService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Factura> repositoryAsync, IMapper mapper, 
                IRepositoryAsync<FacturaMovimiento> repositoryAsyncFacturaMovimiento, 
                ITotalesMovsService totalesMovsService, 
                IRepositoryAsync<ComplementoPagoFactura> repositoryAsyncComplementoPagoFactura,
                IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncFacturaMovimiento = repositoryAsyncFacturaMovimiento;
                _totalesMovsService = totalesMovsService;
                _repositoryAsyncComplementoPagoFactura = repositoryAsyncComplementoPagoFactura;
                _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
            }

            public async Task<Response<List<FacturaPDDDto>>> Handle(GetFacturasByPPDQuery request, CancellationToken cancellationToken)
            {
                var Facturas = await _repositoryAsync.ListAsync(new FacturaByCompanyAndPPDAndTimbradoSpecification(request.CompanyId, request.ClientId));

                var facturasDto = new List<FacturaPDDDto>();

                foreach (var item in Facturas)
                {
                    var movs = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(item.Id));
                    var tm = _totalesMovsService.getTotalesFormMovs(movs);

                    var temp = _mapper.Map<FacturaPDDDto>(item);

                    temp.MontoTotal = (double)tm.total;

                    var asociados = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByFacturaSpecification(item.Id));

                    var total = 0.0;
                    foreach(var cpf in asociados)
                    {
                        var complemento = await _repositoryAsyncComplementoPago.GetByIdAsync(cpf.ComplementoPagoId);
                        if(complemento.Estatus != 3) 
                            total += cpf.Monto;
                    }

                    temp.SaldoIsoluto = (double)tm.total - (double)total;

                    facturasDto.Add(temp);

                }

                return new Response<List<FacturaPDDDto>>(facturasDto);
            }
        }
    }
}
