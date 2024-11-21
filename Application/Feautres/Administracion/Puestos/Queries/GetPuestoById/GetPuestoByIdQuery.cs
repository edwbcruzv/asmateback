using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Administracion;

namespace Application.Feautres.Administracion.Puestos.Queries.GetPuestoById
{
    public class GetPuestoByIdQuery : IRequest<Response<PuestoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetPuestoByIdQuery, Response<PuestoDto>>
        {
            private readonly IRepositoryAsync<Puesto> _repositoryAsync;
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Puesto> repositoryAsync, IMapper mapper, IRepositoryAsync<Departamento> repositoryAsyncDepartamento)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
            }

            public async Task<Response<PuestoDto>> Handle(GetPuestoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(item.DepartamentoId);

                var dto = _mapper.Map<PuestoDto>(item);
                dto.Departamento = departamento.Descripcion;

                return new Response<PuestoDto>(dto);

            }
        }
    }
}
