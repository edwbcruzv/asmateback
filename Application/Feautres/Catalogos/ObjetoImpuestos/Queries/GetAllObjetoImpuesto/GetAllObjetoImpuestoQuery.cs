using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.ObjetoImpuestos.Queries.GetAllObjetoImpuesto
{
    public class GetAllObjetoImpuestoQuery : IRequest<Response<List<ObjetoImpuestoDto>>>
    {
        public class Handler : IRequestHandler<GetAllObjetoImpuestoQuery, Response<List<ObjetoImpuestoDto>>>
        {
            private readonly IRepositoryAsync<ObjetoImpuesto> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ObjetoImpuesto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<ObjetoImpuestoDto>>> Handle(GetAllObjetoImpuestoQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<ObjetoImpuestoDto>>(list);

                return new Response<List<ObjetoImpuestoDto>>(dto);

            }
        }
    }
}
