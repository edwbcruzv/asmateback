using Application.DTOs.MiPortal.ComprobantesSinXML;
using Application.Interfaces;
using Application.Specifications.MiPortal.ComprobantesSinXML;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.ComprobantesSinXML.Queries.GetAllComprobantesSinXML
{
    public class GetComprobantesSinXMLByViaticoIdQuery : IRequest<Response<List<ComprobanteSinXMLDTO>>>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetComprobantesSinXMLByViaticoIdQuery, Response<List<ComprobanteSinXMLDTO>>>
        {
            private readonly IRepositoryAsync<ComprobanteSinXML> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ComprobanteSinXML> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<ComprobanteSinXMLDTO>>> Handle(GetComprobantesSinXMLByViaticoIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new ComprobanteSinXMLByViaticoIdSpecification(request.Id));

                var list_dto = _mapper.Map<List<ComprobanteSinXMLDTO>>(list);

                return new Response<List<ComprobanteSinXMLDTO>>(list_dto);
            }
        }
    }
}
