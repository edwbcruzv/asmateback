using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoIncapacidads.Queries.GetTipoIncapacidadById
{
    public class GetTipoIncapacidadByIdQuery : IRequest<Response<TipoIncapacidadDto>>
    {   
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoIncapacidadByIdQuery, Response<TipoIncapacidadDto>>
        {
            private readonly IRepositoryAsync<TipoIncapacidad> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoIncapacidad> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoIncapacidadDto>> Handle(GetTipoIncapacidadByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);
                
                var dto = _mapper.Map<TipoIncapacidadDto>(item);

                return new Response<TipoIncapacidadDto>(dto);                

            }
        }
    }
}
