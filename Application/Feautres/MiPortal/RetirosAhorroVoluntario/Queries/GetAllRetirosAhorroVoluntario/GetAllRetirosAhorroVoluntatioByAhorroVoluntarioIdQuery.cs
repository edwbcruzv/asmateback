using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetAllRetirosAhorroVoluntario
{
    public class GetAllRetirosAhorroVoluntatioByAhorroVoluntarioIdQuery : IRequest<Response<List<RetiroAhorroVoluntarioDTO>>>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAllRetirosAhorroVoluntatioByAhorroVoluntarioIdQuery, Response<List<RetiroAhorroVoluntarioDTO>>>
        {
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<RetiroAhorroVoluntarioDTO>>> Handle(GetAllRetirosAhorroVoluntatioByAhorroVoluntarioIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new RetiroAhorroVoluntarioByAhorroVoluntarioIdSpecification(request.Id));

                var list_dto = _mapper.Map<List<RetiroAhorroVoluntarioDTO>>(list);

                return new Response<List<RetiroAhorroVoluntarioDTO>>(list_dto);
            }
        }
    }
}
