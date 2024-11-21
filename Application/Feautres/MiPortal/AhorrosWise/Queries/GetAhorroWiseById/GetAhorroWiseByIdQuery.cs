using Application.DTOs.MiPortal.Ahorros;
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

namespace Application.Feautres.MiPortal.AhorrosWise.Queries.GetAhorroWiseById
{
    public class GetAhorroWiseByIdQuery : IRequest<Response<AhorroWiseDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAhorroWiseByIdQuery, Response<AhorroWiseDTO>>
        {
            private readonly IRepositoryAsync<AhorroWise> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroWise> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<AhorroWiseDTO>> Handle(GetAhorroWiseByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var dto = _mapper.Map<AhorroWiseDTO>(elem);
                return new Response<AhorroWiseDTO>(dto, "AhorroWise encontrado con exito.");
            }
        }
    }
}
