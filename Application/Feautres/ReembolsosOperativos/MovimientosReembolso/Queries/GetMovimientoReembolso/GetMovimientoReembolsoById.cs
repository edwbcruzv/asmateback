using Application.DTOs.ReembolsosOperativos;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetReembolsosById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Queries.GetMovimientoReembolso
{
    public class GetMovimientoReembolsoById : IRequest<Response<MovimientoReembolsoDTO>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetMovimientoReembolsoById, Response<MovimientoReembolsoDTO>>
        {
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IRepositoryAsync<TipoReembolso> _repositoryAsyncTipoReembolso;
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<TipoImpuesto> _repositoryAsyncTipoImpuesto;
            private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncMovimientoTipoMoneda;
            private readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
            private readonly IRepositoryAsync<TipoComprobante> _repositoryAsyncTipoComprobante;
            private readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
            private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolsos,
                        IRepositoryAsync<TipoReembolso> repositoryAsyncTipoReembolso,
                        IRepositoryAsync<Reembolso> repositoryAsyncReembolsos,
                        IRepositoryAsync<Company> repositoryAsyncCompany,
                        IRepositoryAsync<TipoMoneda> repositoryAsyncMovimientoTipoMoneda,
                        IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal,
                        IRepositoryAsync<TipoComprobante> repositoryAsyncTipoComprobante,
                        IRepositoryAsync<FormaPago> repositoryAsyncFormaPago,
                        IRepositoryAsync<TipoImpuesto> repositoryAsyncTipoImpuesto,
                        IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago,
                        IMapper mapper
                )
            {
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolsos;
                _repositoryAsyncTipoReembolso = repositoryAsyncTipoReembolso;
                _repositoryAsyncReembolso = repositoryAsyncReembolsos;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _repositoryAsyncTipoImpuesto = repositoryAsyncTipoImpuesto;
                _repositoryAsyncMovimientoTipoMoneda = repositoryAsyncMovimientoTipoMoneda;
                _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;
                _repositoryAsyncTipoComprobante = repositoryAsyncTipoComprobante;
                _repositoryAsyncFormaPago = repositoryAsyncFormaPago;
                _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoReembolsoDTO>> Handle(GetMovimientoReembolsoById request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsyncMovimientoReembolso.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var dto = _mapper.Map<MovimientoReembolsoDTO>(elem);

                var tipo_reembolso = await _repositoryAsyncTipoReembolso.GetByIdAsync(elem.TipoReembolsoId);
                dto.TipoReembolso = tipo_reembolso != null ? tipo_reembolso.Descripcion : null;

                var reembolso = await _repositoryAsyncReembolso.GetByIdAsync(elem.ReembolsoId);
                var company = reembolso != null ? await _repositoryAsyncCompany.GetByIdAsync(reembolso.CompanyId) : null;
                dto.Reembolso = company != null ? company.Name : null;

                var tipo_impuesto = await _repositoryAsyncTipoImpuesto.GetByIdAsync(elem.TipoImpuestoId);
                dto.TipoImpuesto = tipo_impuesto != null ? tipo_impuesto.Descripcion : null;

                var tipo_moneda = await _repositoryAsyncMovimientoTipoMoneda.GetByIdAsync(elem.TipoMonedaId);
                dto.TipoMoneda = tipo_moneda != null ? tipo_moneda.CodigoIso: null;

                var regimen_fiscal = await _repositoryAsyncRegimenFiscal.GetByIdAsync(elem.RegimenFiscalId);
                dto.RegimenFiscal = regimen_fiscal != null ? regimen_fiscal.RegimenFiscalDesc : null;

                var tipo_comprobante = await _repositoryAsyncTipoComprobante.GetByIdAsync(elem.TipoComprobanteId);
                dto.TipoComprobante = tipo_comprobante != null ? tipo_comprobante.Descripcion : null;

                var forma_pago = await _repositoryAsyncFormaPago.GetByIdAsync(elem.FormaPagoId);
                dto.FormaPago = forma_pago != null ? forma_pago.Descripcion : null;

                var metodo_pago = await _repositoryAsyncMetodoPago.GetByIdAsync(elem.MetodoPagoId);
                dto.MetodoPago = metodo_pago != null ? metodo_pago.Descripcion : null;

                return new Response<MovimientoReembolsoDTO>(dto);
            }
        }


    }
}
