using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoContratos.Queries.GetTipoContratoById
{
    public class GetTipoContratoByIdQuery : IRequest<Response<TipoContratoDto>>
    {   
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoContratoByIdQuery, Response<TipoContratoDto>>
        {
            private readonly IRepositoryAsync<TipoContrato> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoContrato> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoContratoDto>> Handle(GetTipoContratoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);
                
                var dto = _mapper.Map<TipoContratoDto>(item);

                return new Response<TipoContratoDto>(dto);                

            }
        }
    }
}
