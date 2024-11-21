using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoJornadas.Queries.GetTipoJornadaById
{
    public class GetTipoJornadaByIdQuery : IRequest<Response<TipoJornadaDto>>
    {   
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoJornadaByIdQuery, Response<TipoJornadaDto>>
        {
            private readonly IRepositoryAsync<TipoJornada> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoJornada> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoJornadaDto>> Handle(GetTipoJornadaByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);
                
                var dto = _mapper.Map<TipoJornadaDto>(item);

                return new Response<TipoJornadaDto>(dto);                

            }
        }
    }
}
