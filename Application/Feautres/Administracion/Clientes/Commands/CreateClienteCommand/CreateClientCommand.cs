using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Clientes.Commands.CreateClienteCommand
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
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
    public class CreateClienteCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateClienteCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<Client>(request);
            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);
        }
    }
}
