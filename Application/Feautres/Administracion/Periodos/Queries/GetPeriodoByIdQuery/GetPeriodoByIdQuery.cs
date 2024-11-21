using Application.DTOs.Administracion;
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

namespace Application.Feautres.Administracion.Periodos.Queries.GetPeriodoByIdQuery
{
    public class GetPeriodoByIdQuery : IRequest<Response<PeriodoDto>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetPeriodoByIdQuery, Response<PeriodoDto>>
        {
            private readonly IRepositoryAsync<Periodo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Periodo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }


            public async Task<Response<PeriodoDto>> Handle(GetPeriodoByIdQuery request, CancellationToken cancellationToken)
            {
                Periodo periodo = await _repositoryAsync.GetByIdAsync(request.Id);

                if (periodo == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<PeriodoDto>(periodo);
                    dto.Asistencias = true;
                    return new Response<PeriodoDto>(dto);
                }
            }
        }

    }
}
