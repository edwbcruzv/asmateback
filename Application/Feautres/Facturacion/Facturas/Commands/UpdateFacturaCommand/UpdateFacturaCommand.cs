using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Facturas.Commands.UpdateFacturaCommand
{
    public class UpdateFacturaCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public int ClientId { get; set; }
        public string ReceptorRfc { get; set; }
        public string ReceptorRazonSocial { get; set; }
        public string LugarExpedicion { get; set; }
        public int UsoCfdiId { get; set; }
        public int FormaPagoId { get; set; }
        public short? Estatus { get; set; }
        public int TipoMonedaId { get; set; }
        public int EmisorRegimenFiscalId { get; set; }
        public int MetodoPagoId { get; set; }
        public int TipoComprobanteId { get; set; }
        public string EmisorRfc { get; set; }
        public string EmisorRazonSocial { get; set; }
        public string ReceptorDomicilioFiscal { get; set; }
        public int ReceptorRegimenFiscalId { get; set; }

    }
    public class Handler : IRequestHandler<UpdateFacturaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;

        public Handler(IRepositoryAsync<Factura> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

        }

        public async Task<Response<int>> Handle(UpdateFacturaCommand request, CancellationToken cancellationToken)
        {
            var Factura = await _repositoryAsync.GetByIdAsync(request.Id);

            if (Factura == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {

                if (Factura.Estatus == 1)
                {
                    Factura.CompanyId = request.CompanyId;
                    Factura.ClientId = request.ClientId;
                    Factura.ReceptorRfc = request.ReceptorRfc;
                    Factura.ReceptorRazonSocial = request.ReceptorRazonSocial;
                    Factura.LogoSrcCompany = request.LogoSrcCompany;
                    Factura.LugarExpedicion = request.LugarExpedicion;
                    Factura.UsoCfdiId = request.UsoCfdiId;
                    Factura.FormaPagoId = request.FormaPagoId;
                    Factura.Estatus = request.Estatus;
                    Factura.TipoMonedaId = request.TipoMonedaId;
                    Factura.EmisorRegimenFiscalId = request.EmisorRegimenFiscalId;
                    Factura.MetodoPagoId = request.MetodoPagoId;
                    Factura.TipoComprobanteId = request.TipoComprobanteId;
                    Factura.EmisorRfc = request.EmisorRfc;
                    Factura.EmisorRazonSocial = request.EmisorRazonSocial;
                    Factura.ReceptorDomicilioFiscal = request.ReceptorDomicilioFiscal;
                    Factura.ReceptorRegimenFiscalId = request.ReceptorRegimenFiscalId;

                    await _repositoryAsync.UpdateAsync(Factura);

                    return new Response<int>(Factura.Id);
                }
                else
                {
                    throw new KeyNotFoundException($"Factura no editable para Id {request.Id}");
                }
            }
        }
    }
}
