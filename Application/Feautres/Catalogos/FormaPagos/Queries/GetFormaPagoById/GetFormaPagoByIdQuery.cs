using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.FormaPagos.Queries.GetFormaPagoById
{
    public class GetFormaPagoByIdQuery : IRequest<Response<FormaPagoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetFormaPagoByIdQuery, Response<FormaPagoDto>>
        {
            private readonly IRepositoryAsync<FormaPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<FormaPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<FormaPagoDto>> Handle(GetFormaPagoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<FormaPagoDto>(item);

                return new Response<FormaPagoDto>(dto);

            }
        }
    }
}
