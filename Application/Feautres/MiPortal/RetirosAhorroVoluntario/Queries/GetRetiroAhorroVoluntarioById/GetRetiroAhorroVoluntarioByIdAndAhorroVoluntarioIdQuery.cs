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

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetRetiroAhorroVoluntarioById
{
    public class GetRetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdQuery : IRequest<Response<RetiroAhorroVoluntarioDTO>>
    {
        public int Id { get; set; }
        public int AhorroVoluntarioId { get; set; }

        public class Handler : IRequestHandler<GetRetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdQuery, Response<RetiroAhorroVoluntarioDTO>>
        {
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<RetiroAhorroVoluntarioDTO>> Handle(GetRetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetBySpecAsync(new RetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdSpecification(request.Id,request.AhorroVoluntarioId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var dto = _mapper.Map<RetiroAhorroVoluntarioDTO>(elem);
                return new Response<RetiroAhorroVoluntarioDTO>(dto, "RetiroAhorroVoluntario encontrado con exito.");
            }
        }
    }
}
