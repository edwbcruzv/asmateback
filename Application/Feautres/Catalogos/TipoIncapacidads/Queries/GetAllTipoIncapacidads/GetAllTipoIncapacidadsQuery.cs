using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoIncapacidads.Queries.GetAllTipoIncapacidad
{
    public class GetAllTipoIncapacidadQuery : IRequest<Response<List<TipoIncapacidadDto>>>
    {     
        public class Handler : IRequestHandler<GetAllTipoIncapacidadQuery, Response<List<TipoIncapacidadDto>>>
        {
            private readonly IRepositoryAsync<TipoIncapacidad> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoIncapacidad> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoIncapacidadDto>>> Handle(GetAllTipoIncapacidadQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();
                
                var dto = _mapper.Map<List<TipoIncapacidadDto>>(list);

                return new Response<List<TipoIncapacidadDto>>(dto);                

            }
        }
    }
}
