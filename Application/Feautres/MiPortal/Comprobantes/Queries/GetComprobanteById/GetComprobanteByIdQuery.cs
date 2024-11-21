using Application.DTOs.MiPortal.Comprobantes;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Queries.GetComprobanteById
{
    public class GetComprobanteByIdQuery : IRequest<Response<ComprobanteDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetComprobanteByIdQuery, Response<ComprobanteDTO>>
        {
            private readonly IRepositoryAsync<Comprobante> _repositoryAsync;
            private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsync, IRepositoryAsync<Viatico> repositoryAsyncViatico, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncViatico = repositoryAsyncViatico;
                _mapper = mapper;
            }

            public async Task<Response<ComprobanteDTO>> Handle(GetComprobanteByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var dto = _mapper.Map<ComprobanteDTO>(elem);
                return new Response<ComprobanteDTO>(dto, "Comprobante encontrado con exito.");

            }
        }

    }
}
