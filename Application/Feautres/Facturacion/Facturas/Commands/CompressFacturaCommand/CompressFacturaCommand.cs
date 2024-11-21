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

namespace Application.Feautres.Facturacion.Facturas.Commands.CompressFacturaCommand
{
    public class CompressFacturaCommand : IRequest<Response<SourceFileDto>>
    {

        public int[] Ids { get; set; }

    }
    public class Handler : IRequestHandler<CompressFacturaCommand, Response<SourceFileDto>>
    {
        private readonly IFileToRarService _fileToRar;

        public Handler(IFileToRarService fileToRar)
        {
            _fileToRar = fileToRar;
        }

        public async Task<Response<SourceFileDto>> Handle(CompressFacturaCommand request, CancellationToken cancellationToken)
        {

            return await _fileToRar.createRarFacturas(request.Ids);

        }
    }
}
