using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Administracion;
using Application.Specifications.Administracion;

namespace Application.Feautres.Administracion.Departamentos.Queries.GetAllDepartamentos
{
    public class GetAllDepartamentosByCompanyQuery : IRequest<Response<List<DepartamentoDto>>>
    {
        public int id { get; set; }
        public class Handler : IRequestHandler<GetAllDepartamentosByCompanyQuery, Response<List<DepartamentoDto>>>
        {
            private readonly IRepositoryAsync<Departamento> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Departamento> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<DepartamentoDto>>> Handle(GetAllDepartamentosByCompanyQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new DepartamentoByCompanyIdSpecification(request.id));

                var dto = _mapper.Map<List<DepartamentoDto>>(list);

                return new Response<List<DepartamentoDto>>(dto);

            }
        }
    }
}
