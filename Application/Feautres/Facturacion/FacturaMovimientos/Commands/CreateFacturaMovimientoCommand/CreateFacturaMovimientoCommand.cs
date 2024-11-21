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

namespace Application.Feautres.Facturacion.FacturaMovimientos.Commands.CreateFacturaMovimientoCommand
{
    public class CreateFacturaMovimientoCommand : IRequest<Response<int>>
    {

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
    public class Handler : IRequestHandler<CreateFacturaMovimientoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsync;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<FacturaMovimiento> repositoryAsync, IMapper mapper, IRsa rsa, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;

        }

        public async Task<Response<int>> Handle(CreateFacturaMovimientoCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<FacturaMovimiento>(request);

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
