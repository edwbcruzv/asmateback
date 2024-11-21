using Application.Feautres.Facturacion.Facturas.Commands.CreateFacturaCommand;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.CreateReembolsoCommand
{
    public class CreateReembolsoCommand : IRequest<Response<int>>
    {
        
        public string Descripcion { get; set; }
        public string? Clabe { get; set; }
        public string? SrcPdfPagoComprobante { get; set; }
        public int? UsuarioIdPago { get; set; }
        public string? SrcPdfFichaPago { get; set; }
        public int CompanyId { get; set; }

    }

    public class Handler : IRequestHandler<CreateReembolsoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryAsync;
        private readonly IMapper _mapper;

        public Handler(IRepositoryAsync<Reembolso> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }



        public async Task<Response<int>> Handle(CreateReembolsoCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<Reembolso>(request);
            nuevoRegistro.EstatusId = 1;

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);
            Console.WriteLine(data);
            return new Response<int>(data.Id);
        }
    }
}
