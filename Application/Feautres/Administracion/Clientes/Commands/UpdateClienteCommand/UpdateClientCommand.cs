using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Rfc { get; set; }
        public string Name { get; set; }
        public string RazonSocial { get; set; }
        public string? Calle { get; set; }
        public string? NoExt { get; set; }
        public string? NoInt { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
        public string CodigoPostal { get; set; }
        public bool? Estatus { get; set; }
        public short TipoPersona { get; set; }
        public int RegimenFiscalId { get; set; }
        public string? Clabe { get; set; }
        public string Correos { get; set; }

    }
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
        private readonly IMapper _mapper;

        public UpdateClienteCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper, IRepositoryAsync<Factura> repositoryAsyncFactura)
        {
            _repositoryAsync = repositoryAsync;
            _repositoryAsyncFactura = repositoryAsyncFactura;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);

            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {

                client.CompanyId = request.CompanyId;
                client.Rfc = request.Rfc;
                client.Name = request.Name;
                client.RazonSocial = request.RazonSocial;
                client.Calle = request.Calle;
                client.NoExt = request.NoExt;
                client.NoInt = request.NoInt;
                client.Colonia = request.Colonia;
                client.Municipio = request.Municipio;
                client.Estado = request.Estado;
                client.Pais = request.Pais;
                client.CodigoPostal = request.CodigoPostal;
                client.Estatus = request.Estatus;
                client.TipoPersona = request.TipoPersona;
                client.RegimenFiscalId = request.RegimenFiscalId;
                client.Clabe = request.Clabe;
                client.Correos = request.Correos;

                await _repositoryAsync.UpdateAsync(client);

                var facturas = await _repositoryAsyncFactura.ListAsync(new FacturaByClienteAndTrueSpecification(request.Id));

                foreach (Factura factura in facturas)
                {
                    factura.ReceptorRfc = client.Rfc;
                    factura.ReceptorRazonSocial = client.RazonSocial;
                    factura.ReceptorDomicilioFiscal = client.CodigoPostal;
                    factura.ReceptorRegimenFiscalId = client.RegimenFiscalId;

                    await _repositoryAsyncFactura.UpdateAsync(factura);

                }

                return new Response<int>(client.Id);
            }
        }
    }
}
