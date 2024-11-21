using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.ContracsUsers;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Usuarios.ContractsUserCompanies.Commands.DeleteContractsUserCompanies
{
    public class DeleteContractsUserCompanyCommand : IRequest<Response<int>>
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteContractsUserCompanyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ContractsUserCompany> _repositoryAsync;

        public DeleteCompanyCommandHandler(IRepositoryAsync<ContractsUserCompany> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteContractsUserCompanyCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.FirstOrDefaultAsync(new ContractsUserCompanyByUserAndCompanySpecification(request.CompanyId, request.UserId));

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id compania {request.CompanyId} y id usuario {request.UserId}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(cliente);

                return new Response<int>(cliente.Id);
            }
        }
    }
}
