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

namespace Application.Feautres.Administracion.Periodos.Queries.GetAllPeriodosQuery
{
    public class GetPeriodosByCompanyAndTipoQuery : IRequest<Response<List<PeriodoDto>>>
    {
        public int CompanyId { get; set; }
        public int Tipo { get; set; }
        public class Handler : IRequestHandler<GetPeriodosByCompanyAndTipoQuery, Response<List<PeriodoDto>>>
        {
            private readonly IRepositoryAsync<Periodo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Periodo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<PeriodoDto>>> Handle(GetPeriodosByCompanyAndTipoQuery request, CancellationToken cancellationToken)
            {
                var Periodos = await _repositoryAsync.ListAsync(new PeriodosByCompanyAndTipoSpecification(request.CompanyId,request.Tipo));

                var PeriodosDto = _mapper.Map<List<PeriodoDto>>(Periodos);

                return new Response<List<PeriodoDto>>(PeriodosDto);
            }
        }
    }
}
