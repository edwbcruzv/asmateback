using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Clientes.Queries.GetClienteByCompany
{
    public class GetAllClientByCompanyQuery : IRequest<Response<List<ClientDto>>>
    {
        public int Id { get; set; }
        public class GetAllClientByCompanyQueryHandler : IRequestHandler<GetAllClientByCompanyQuery, Response<List<ClientDto>>>
        {
            private readonly IRepositoryAsync<Client> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllClientByCompanyQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<ClientDto>>> Handle(GetAllClientByCompanyQuery request, CancellationToken cancellationToken)
            {
                var clients = await _repositoryAsync.ListAsync(new ClientByUserSpecification(request.Id));

                var clientsDto = _mapper.Map<List<ClientDto>>(clients);

                return new Response<List<ClientDto>>(clientsDto);
            }
        }
    }
}
