using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Facturas.Commands.CreateFacturaCommand
{
    public class CreateFacturaCommand : IRequest<Response<int>>
    {

        public int CompanyId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public int ClientId { get; set; }
        public string ReceptorRfc { get; set; }
        public string ReceptorRazonSocial { get; set; }
        public string LugarExpedicion { get; set; }
        public int UsoCfdiId { get; set; }
        public int FormaPagoId { get; set; }
        public int TipoMonedaId { get; set; }
        public int EmisorRegimenFiscalId { get; set; }
        public int MetodoPagoId { get; set; }
        public int TipoComprobanteId { get; set; }
        public string EmisorRfc { get; set; }
        public string EmisorRazonSocial { get; set; }
        public string ReceptorDomicilioFiscal { get; set; }
        public int ReceptorRegimenFiscalId { get; set; }

    }
    public class Handler : IRequestHandler<CreateFacturaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<Factura> repositoryAsync, IMapper mapper, IRepositoryAsync<Company> repositoryAsyncCompany)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _repositoryAsyncCompany = repositoryAsyncCompany;
        }

        public IRepositoryAsync<Factura> RepositoryAsync => _repositoryAsync;

        public async Task<Response<int>> Handle(CreateFacturaCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<Factura>(request);
            nuevoRegistro.Estatus = 1;

            var data = await RepositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
