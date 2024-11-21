using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Usuarios.ContractsUserCompanies.Commands.CreateContractsUserCompanies
{
    public class CreateContractsUserCompanyCommand : IRequest<Response<int>>
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }

    }
    public class CreateCompanyCommandHandler : IRequestHandler<CreateContractsUserCompanyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ContractsUserCompany> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandler(IRepositoryAsync<ContractsUserCompany> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateContractsUserCompanyCommand request, CancellationToken cancellationToken)
        {
            var nuevoregistro = _mapper.Map<ContractsUserCompany>(request);
            var data = await _repositoryAsync.AddAsync(nuevoregistro);

            return new Response<int>(data.Id);
        }
    }
}
