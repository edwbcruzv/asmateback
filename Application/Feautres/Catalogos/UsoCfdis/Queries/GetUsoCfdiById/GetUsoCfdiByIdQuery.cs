using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.UsoCfdis.Queries.GetUsoCfdiById
{
    public class GetUsoCfdiByIdQuery : IRequest<Response<UsoCfdiDto>>
    {   
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetUsoCfdiByIdQuery, Response<UsoCfdiDto>>
        {
            private readonly IRepositoryAsync<UsoCfdi> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<UsoCfdi> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<UsoCfdiDto>> Handle(GetUsoCfdiByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);
                
                var dto = _mapper.Map<UsoCfdiDto>(item);

                return new Response<UsoCfdiDto>(dto);                

            }
        }
    }
}
