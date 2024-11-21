using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<Response<CompanyDTO>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetCompanyByIdQuery, Response<CompanyDTO>>
        {
            private readonly IRepositoryAsync<Company> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Company> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CompanyDTO>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
            {
                var cliente = await _repositoryAsync.GetByIdAsync(request.Id);

                if (cliente == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<CompanyDTO>(cliente);
                    return new Response<CompanyDTO>(dto);
                }

            }
        }
    }
}
