using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Administracion;

namespace Application.Feautres.Administracion.Departamentos.Queries.GetDepartamentoById
{
    public class GetDepartamentoByIdQuery : IRequest<Response<DepartamentoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetDepartamentoByIdQuery, Response<DepartamentoDto>>
        {
            private readonly IRepositoryAsync<Departamento> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Departamento> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<DepartamentoDto>> Handle(GetDepartamentoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<DepartamentoDto>(item);

                return new Response<DepartamentoDto>(dto);

            }
        }
    }
}
