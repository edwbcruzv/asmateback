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

namespace Application.Feautres.Administracion.SubMenus.Queries.GetSubMenuById
{
    public class GetSubMenuByIdQuery : IRequest<Response<SubMenuDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetSubMenuByIdQuery, Response<SubMenuDto>>
        {
            private readonly IRepositoryAsync<SubMenu> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<SubMenu> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<SubMenuDto>> Handle(GetSubMenuByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                if (item == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<SubMenuDto>(item);
                    return new Response<SubMenuDto>(dto);
                }

            }
        }
    }
}
