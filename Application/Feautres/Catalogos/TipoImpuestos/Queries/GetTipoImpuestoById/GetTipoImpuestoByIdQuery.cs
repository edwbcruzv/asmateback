using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoImpuestos.Queries.GetTipoImpuestoById
{
    public class GetTipoImpuestoByIdQuery : IRequest<Response<TipoImpuestoDto>>
    {
        public int Id { get; set; }
        public class Handler: IRequestHandler<GetTipoImpuestoByIdQuery, Response<TipoImpuestoDto>>
        {
            private readonly IRepositoryAsync<TipoImpuesto> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoImpuesto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoImpuestoDto>> Handle(GetTipoImpuestoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);
                var dto = _mapper.Map<TipoImpuestoDto>(item);
                return new Response<TipoImpuestoDto>(dto);
            }
        }
    }
}
