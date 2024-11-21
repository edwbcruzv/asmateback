using Application.DTOs.Administracion;
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

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.PdfComplementoPagoCommand
{
    public class PdfComplementoPagoCommand : IRequest<Response<SourceFileDto>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<PdfComplementoPagoCommand, Response<SourceFileDto>>
    {
        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsync;
        private readonly IPdfService _pdfService;



        public Handler(IRepositoryAsync<ComplementoPago> repositoryAsync, IPdfService pdfService)
        {
            _repositoryAsync = repositoryAsync;
            _pdfService = pdfService;
        }

        public IRepositoryAsync<ComplementoPago> RepositoryAsync => _repositoryAsync;

        public async Task<Response<SourceFileDto>> Handle(PdfComplementoPagoCommand request, CancellationToken cancellationToken)
        {
            return await _pdfService.PdfComplementoPago(request.Id);

        }
    }
}
