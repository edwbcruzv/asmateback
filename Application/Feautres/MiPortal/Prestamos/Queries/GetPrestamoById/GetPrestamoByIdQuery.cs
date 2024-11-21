using Application.DTOs.MiPortal.Prestamos;
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

namespace Application.Feautres.MiPortal.Prestamos.Queries.GetPrestamoById
{
    public class GetPrestamoByIdQuery : IRequest<Response<PrestamoDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetPrestamoByIdQuery, Response<PrestamoDTO>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<PrestamoDTO>> Handle(GetPrestamoByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var dto = _mapper.Map<PrestamoDTO>(elem);
                return new Response<PrestamoDTO>(dto, "Prestamo encontrado con exito.");
            }
        }
    }
}
