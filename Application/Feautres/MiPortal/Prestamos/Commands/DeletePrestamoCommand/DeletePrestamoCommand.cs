using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Prestamos.Commands.DeletePrestamoCommand
{
    public class DeletePrestamoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeletePrestamoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsync;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeletePrestamoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Prestamo no encontrado con el id {request.Id}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id, "Prestamo eliminado");
            }
        }
    }
}
