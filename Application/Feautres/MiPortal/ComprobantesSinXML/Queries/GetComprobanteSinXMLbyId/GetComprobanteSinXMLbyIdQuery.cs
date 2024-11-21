using Application.DTOs.MiPortal.ComprobantesSinXML;
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

namespace Application.Feautres.MiPortal.ComprobantesSinXML.Queries.GetComprobanteSinXMLbyId
{
    public class GetComprobanteSinXMLbyIdQuery : IRequest<Response<ComprobanteSinXMLDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetComprobanteSinXMLbyIdQuery, Response<ComprobanteSinXMLDTO>>
        {
            private readonly IRepositoryAsync<ComprobanteSinXML> _repositoryAsync;
            private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ComprobanteSinXML> repositoryAsync, IRepositoryAsync<Viatico> repositoryAsyncViatico, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncViatico = repositoryAsyncViatico;
                _mapper = mapper;
            }

            public async Task<Response<ComprobanteSinXMLDTO>> Handle(GetComprobanteSinXMLbyIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var dto = _mapper.Map<ComprobanteSinXMLDTO>(elem);
                return new Response<ComprobanteSinXMLDTO>(dto, "ComprobanteSinXML encontrado con exito.");

            }
        }

    }
}
