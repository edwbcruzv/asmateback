using Application.Interfaces;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.DeleteRetiroAhorroVoluntario
{
    public class DeleteRetiroAhorroVoluntarioCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int AhorroVoluntarioId { get; set; }

        public class Handler : IRequestHandler<DeleteRetiroAhorroVoluntarioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsync;

            public Handler(IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteRetiroAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetBySpecAsync(new RetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdSpecification(request.Id, request.AhorroVoluntarioId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id, "Registro eliminado");
            }
        }
    }
}
