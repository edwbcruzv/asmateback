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

namespace Application.Feautres.Facturacion.FacturaMovimientos.Commands.UpdateFacturaMovimientoCommand
{
    public class UpdateFacturaMovimientoCommand : IRequest<Response<int>>
    {

        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int Cantidad { get; set; }
        public int UnidadMedidaId { get; set; }
        public int CveProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public bool Iva { get; set; }
        public bool Iva6 { get; set; }
        public bool RetencionIva { get; set; }
        public bool RetencionIsr { get; set; }
        public int ObjetoImpuestoId { get; set; }

    }
    public class Handler : IRequestHandler<UpdateFacturaMovimientoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsync;

        public Handler(IRepositoryAsync<FacturaMovimiento> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

        }

        public async Task<Response<int>> Handle(UpdateFacturaMovimientoCommand request, CancellationToken cancellationToken)
        {
            var FacturaMovimiento = await _repositoryAsync.GetByIdAsync(request.Id);

            if (FacturaMovimiento == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {

                FacturaMovimiento.Cantidad = request.Cantidad;
                FacturaMovimiento.UnidadMedidaId = request.UnidadMedidaId;
                FacturaMovimiento.CveProductoId = request.CveProductoId;
                FacturaMovimiento.Descripcion = request.Descripcion;
                FacturaMovimiento.PrecioUnitario = request.PrecioUnitario;
                FacturaMovimiento.Descuento = request.Descuento;
                FacturaMovimiento.Iva = request.Iva;
                FacturaMovimiento.Iva6 = request.Iva6;
                FacturaMovimiento.RetencionIva = request.RetencionIva;
                FacturaMovimiento.RetencionIsr = request.RetencionIsr;
                FacturaMovimiento.ObjetoImpuestoId = request.ObjetoImpuestoId;


                await _repositoryAsync.UpdateAsync(FacturaMovimiento);

                return new Response<int>(FacturaMovimiento.Id);



            }
        }
    }
}
