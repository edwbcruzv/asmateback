using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.FormaPagos.Queries.GetAllFormaPago
{
    public class GetAllFormaPagoQuery : IRequest<Response<List<FormaPagoDto>>>
    {
        public class GetAllFormaPagoHandler : IRequestHandler<GetAllFormaPagoQuery, Response<List<FormaPagoDto>>>
        {
            private readonly IRepositoryAsync<FormaPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllFormaPagoHandler(IRepositoryAsync<FormaPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<FormaPagoDto>>> Handle(GetAllFormaPagoQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<FormaPagoDto>>(list);

                return new Response<List<FormaPagoDto>>(dto);

            }
        }
    }
}
