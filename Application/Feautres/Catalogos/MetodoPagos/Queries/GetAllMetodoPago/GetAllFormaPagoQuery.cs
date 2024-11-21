using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.MetodoPagos.Queries.GetAllMetodoPago
{
    public class GetAllMetodoPagoQuery : IRequest<Response<List<MetodoPagoDto>>>
    {
        public class Handler : IRequestHandler<GetAllMetodoPagoQuery, Response<List<MetodoPagoDto>>>
        {
            private readonly IRepositoryAsync<MetodoPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MetodoPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<MetodoPagoDto>>> Handle(GetAllMetodoPagoQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<MetodoPagoDto>>(list);

                return new Response<List<MetodoPagoDto>>(dto);

            }
        }
    }
}
