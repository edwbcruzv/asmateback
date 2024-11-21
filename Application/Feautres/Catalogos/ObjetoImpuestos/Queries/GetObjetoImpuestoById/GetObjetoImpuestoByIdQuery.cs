using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.ObjetoImpuestos.Queries.GetObjetoImpuestoById
{
    public class GetObjetoImpuestoByIdQuery : IRequest<Response<ObjetoImpuestoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetObjetoImpuestoByIdQuery, Response<ObjetoImpuestoDto>>
        {
            private readonly IRepositoryAsync<ObjetoImpuesto> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ObjetoImpuesto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<ObjetoImpuestoDto>> Handle(GetObjetoImpuestoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<ObjetoImpuestoDto>(item);

                return new Response<ObjetoImpuestoDto>(dto);

            }
        }
    }
}
