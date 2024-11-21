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

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.UpdateComplementoPagoCommand
{
    public class UpdateComplementoPagoCommand : IRequest<Response<int>>
    {

        public int Id { get; set; }
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
        public IFormFile? FilePago { get; set; }
        public int FormaPagoId { get; set; }
        public int TipoMonedaId { get; set; }

    }
    public class Handler : IRequestHandler<UpdateComplementoPagoCommand, Response<int>>
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

        public async Task<Response<int>> Handle(UpdateComplementoPagoCommand request, CancellationToken cancellationToken)
        {

            var cp = await _repositoryAsync.GetByIdAsync(request.Id);

            if(cp == null)
            {
                throw new KeyNotFoundException($"ComplementoPago no encontrado con el id {request.Id}");
            }

            cp.CompanyId = request.CompanyId;
            cp.ClientId = request.ClientId;
            cp.LogoSrcCompany = request.LogoSrcCompany;
            cp.LugarExpedicion = request.LugarExpedicion;
            cp.EmisorRfc = request.EmisorRfc;
            cp.EmisorRazonSocial = request.EmisorRazonSocial;
            cp.EmisorRegimenFiscalId = request.EmisorRegimenFiscalId;
            cp.ReceptorRfc = request.ReceptorRfc;
            cp.ReceptorRazonSocial = request.ReceptorRazonSocial;
            cp.ReceptorDomicilioFiscal = request.ReceptorDomicilioFiscal;
            cp.ReceptorRegimenFiscalId = request.ReceptorRegimenFiscalId;
            cp.FechaPago = request.FechaPago;
            cp.FormaPagoId = request.FormaPagoId;
            cp.TipoMonedaId = request.TipoMonedaId;


            if (request.FilePago?.Length > 0)
            {
                cp.PagoSrcPdf = _filesManagerService.saveComplementoPagoPdf(request.FilePago, request.CompanyId);
            }

            await _repositoryAsync.UpdateAsync(cp);

            return new Response<int>(cp.Id);


        }
    }
}
