using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoRegimens.Queries.GetAllTipoRegimens
{
    public class GetAllTipoRegimensQuery : IRequest<Response<List<TipoRegimenDto>>>
    {
        public class Handler : IRequestHandler<GetAllTipoRegimensQuery, Response<List<TipoRegimenDto>>>
        {
            private readonly IRepositoryAsync<TipoRegimen> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoRegimen> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoRegimenDto>>> Handle(GetAllTipoRegimensQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<TipoRegimenDto>>(list);

                return new Response<List<TipoRegimenDto>>(dto);

            }
        }
    }
}
