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

namespace Application.Feautres.MiPortal.Prestamos.Commands.UpdatePrestamoCommand
{
    public class UpdatePrestamoFechaTransferenciaCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public DateTime FechaTransferencia { get; set; }


        public class Handler : IRequestHandler<UpdatePrestamoFechaTransferenciaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsyncPrestamo, IMapper mapper)
            {
                _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdatePrestamoFechaTransferenciaCommand request, CancellationToken cancellationToken)
            {
                var prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(request.Id);

                if (prestamo == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    try
                    {

                        prestamo.FechaTransferencia = request.FechaTransferencia;

                        await _repositoryAsyncPrestamo.UpdateAsync(prestamo);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar Solicitud." + ex.ToString());
                    }

                    return new Response<int>(prestamo.Id);

                }
            }
        }

    }
}
