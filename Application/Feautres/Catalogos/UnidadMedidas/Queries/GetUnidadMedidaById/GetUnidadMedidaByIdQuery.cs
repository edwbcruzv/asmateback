using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.UnidadMedidas.Queries.GetUnidadMedidaById
{
    public class GetUnidadMedidaByIdQuery : IRequest<Response<UnidadMedidaDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetUnidadMedidaByIdQuery, Response<UnidadMedidaDto>>
        {
            private readonly IRepositoryAsync<UnidadMedida> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<UnidadMedida> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<UnidadMedidaDto>> Handle(GetUnidadMedidaByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<UnidadMedidaDto>(item);

                return new Response<UnidadMedidaDto>(dto);

            }
        }
    }
}
