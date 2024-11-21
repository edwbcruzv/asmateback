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

namespace Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.CreateComplementoPagoFacturaCommand
{
    public class CreateComplementoPagoFacturaCommand : IRequest<Response<int>>
    {

        public int ComplementoPagoId { get; set; }
        public int FacturaId { get; set; }
        public int Folio { get; set; }
        public double Monto { get; set; }
        public bool iva {  get; set; }

    }
    public class Handler : IRequestHandler<CreateComplementoPagoFacturaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsync;
        private readonly IFilesManagerService _filesManagerService;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<ComplementoPagoFactura> repositoryAsync, IMapper mapper, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _filesManagerService = filesManagerService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateComplementoPagoFacturaCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<ComplementoPagoFactura>(request);

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
