using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.UsoCfdis.Queries.GetAllUsoCfdi
{
    public class GetAllUsoCfdiQuery : IRequest<Response<List<UsoCfdiDto>>>
    {     
        public class Handler : IRequestHandler<GetAllUsoCfdiQuery, Response<List<UsoCfdiDto>>>
        {
            private readonly IRepositoryAsync<UsoCfdi> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<UsoCfdi> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<UsoCfdiDto>>> Handle(GetAllUsoCfdiQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();
                
                var dto = _mapper.Map<List<UsoCfdiDto>>(list);

                return new Response<List<UsoCfdiDto>>(dto);                

            }
        }
    }
}
