using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Companies.Commands.DeleteCompanyCommand
{
    public class DeleteCompanyCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Company> _repositoryAsync;

        public DeleteCompanyCommandHandler(IRepositoryAsync<Company> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(cliente);

                return new Response<int>(cliente.Id);
            }
        }
    }
}
