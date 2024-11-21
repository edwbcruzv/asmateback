using Application.DTOs.Facturas;
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

namespace Application.Feautres.Facturacion.Facturas.Commands.PdfFacturaCommand
{
    public class PdfFacturaCommand : IRequest<Response<FacturaPdfDto>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<PdfFacturaCommand, Response<FacturaPdfDto>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;
        private readonly IPdfService _pdfService;



        public Handler(IRepositoryAsync<Factura> repositoryAsync, IPdfService pdfService)
        {
            _repositoryAsync = repositoryAsync;
            _pdfService = pdfService;
        }

        public IRepositoryAsync<Factura> RepositoryAsync => _repositoryAsync;

        public async Task<Response<FacturaPdfDto>> Handle(PdfFacturaCommand request, CancellationToken cancellationToken)
        {
            return await _pdfService.PdfFactura(request.Id);

        }
    }
}
