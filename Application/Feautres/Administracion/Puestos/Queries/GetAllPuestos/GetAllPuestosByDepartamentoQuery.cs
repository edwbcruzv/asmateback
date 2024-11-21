using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Administracion;
using Application.Specifications.Administracion;

namespace Application.Feautres.Administracion.Puestos.Queries.GetAllPuestos
{
    public class GetAllPuestosByDepartamentoQuery : IRequest<Response<List<PuestoDto>>>
    {
        public int id { get; set; }
        public class Handler : IRequestHandler<GetAllPuestosByDepartamentoQuery, Response<List<PuestoDto>>>
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

            public async Task<Response<List<PuestoDto>>> Handle(GetAllPuestosByDepartamentoQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new PuestoByDepartamentoSpecification(request.id));

                var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(request.id);

                var list_dto = _mapper.Map<List<PuestoDto>>(list);

                foreach (var elem in list_dto)
                {
                    var dto = _mapper.Map<PuestoDto>(elem);
                    dto.Departamento = departamento.Descripcion;
                }

                return new Response<List<PuestoDto>>(list_dto);

            }
        }
    }
}
