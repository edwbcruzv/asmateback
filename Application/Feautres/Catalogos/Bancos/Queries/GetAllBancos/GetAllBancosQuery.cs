using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.Bancos.Queries.GetAllBancos
{
    public class GetAllBancosQuery : IRequest<Response<List<BancoDto>>>
    {
        public class Handler : IRequestHandler<GetAllBancosQuery, Response<List<BancoDto>>>
        {
            private readonly IRepositoryAsync<Banco> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Banco> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<BancoDto>>> Handle(GetAllBancosQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<BancoDto>>(list);

                return new Response<List<BancoDto>>(dto);

            }
        }
    }
}
