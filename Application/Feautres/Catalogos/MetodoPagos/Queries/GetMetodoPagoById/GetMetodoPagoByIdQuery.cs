using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.MetodoPagos.Queries.GetMetodoPagoById
{
    public class GetMetodoPagoByIdQuery : IRequest<Response<MetodoPagoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetMetodoPagoByIdQuery, Response<MetodoPagoDto>>
        {
            private readonly IRepositoryAsync<MetodoPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MetodoPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<MetodoPagoDto>> Handle(GetMetodoPagoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<MetodoPagoDto>(item);

                return new Response<MetodoPagoDto>(dto);

            }
        }
    }
}
