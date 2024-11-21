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

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.CreateComplementoPagoCommand
{
    public class CreateComplementoPagoCommand : IRequest<Response<int>>
    {

        public int ClientId { get; set; }
        public int CompanyId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public string LugarExpedicion { get; set; }
        public string EmisorRfc { get; set; } //company
        public string EmisorRazonSocial { get; set; } //company
        public int EmisorRegimenFiscalId { get; set; } //company
        public string ReceptorRfc { get; set; } //Company
        public string ReceptorRazonSocial { get; set; } //Company
        public string ReceptorDomicilioFiscal { get; set; } //Cliente
        public int ReceptorRegimenFiscalId { get; set; } //Cliente
        public DateTime FechaPago { get; set; }
        public IFormFile FilePago { get; set; }
        public int FormaPagoId { get; set; }
        public int TipoMonedaId { get; set; }

    }
    public class Handler : IRequestHandler<CreateComplementoPagoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsync;
        private readonly IFilesManagerService _filesManagerService;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<ComplementoPago> repositoryAsync, IMapper mapper, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _filesManagerService = filesManagerService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateComplementoPagoCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<ComplementoPago>(request);

            nuevoRegistro.PagoSrcPdf = _filesManagerService.saveComplementoPagoPdf(request.FilePago, request.CompanyId);
            nuevoRegistro.Estatus = 1;

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
