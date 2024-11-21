using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByValeAzul;
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

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteValeAzulCommand
{
    public class CreateComprobanteValeAzulCommand : IRequest<Response<int>>
    {
        public int ViaticoId { get; set; }
        public float Total { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int? MetodoPagoId { get; set; }
        public string Concepto { get; set; }
        public string? EmisorNombre { get; set; }

        public class Handler : IRequestHandler<CreateComprobanteValeAzulCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Comprobante> _repositoryAsyncComprobante;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsyncComprobante, IMapper mapper)
            {
                _repositoryAsyncComprobante = repositoryAsyncComprobante;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateComprobanteValeAzulCommand request, CancellationToken cancellationToken)
            {
                var comprovante = _mapper.Map<Comprobante>(request);
                comprovante.TipoComprobantes = TipoComprobantes.ValeAzul;
                comprovante.TipoCambio = 1;
                var data = await _repositoryAsyncComprobante.AddAsync(comprovante);
                return new Response<int>(data.Id);
            }
        }
    }
}
