using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.Bancos.Queries.GetBancoById
{
    public class GetBancoByIdQuery : IRequest<Response<BancoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetBancoByIdQuery, Response<BancoDto>>
        {
            private readonly IRepositoryAsync<Banco> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Banco> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<BancoDto>> Handle(GetBancoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<BancoDto>(item);

                return new Response<BancoDto>(dto);

            }
        }
    }
}
