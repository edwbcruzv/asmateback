using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoRegimens.Queries.GetTipoRegimenById
{
    public class GetTipoRegimenByIdQuery : IRequest<Response<TipoRegimenDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoRegimenByIdQuery, Response<TipoRegimenDto>>
        {
            private readonly IRepositoryAsync<TipoRegimen> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoRegimen> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoRegimenDto>> Handle(GetTipoRegimenByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<TipoRegimenDto>(item);

                return new Response<TipoRegimenDto>(dto);

            }
        }
    }
}
