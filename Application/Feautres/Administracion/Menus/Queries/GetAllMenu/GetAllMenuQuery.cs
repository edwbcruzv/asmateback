using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications;
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

namespace Application.Feautres.Administracion.Menus.Queries.GetAllMenu
{
    public class GetAllMenuQuery : IRequest<Response<List<MenuDto>>>
    {
        public class Handler : IRequestHandler<GetAllMenuQuery, Response<List<MenuDto>>>
        {
            private readonly IRepositoryAsync<Menu> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Menu> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<MenuDto>>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
            {
                var Menus = await _repositoryAsync.ListAsync();

                var MenusDto = _mapper.Map<List<MenuDto>>(Menus);

                return new Response<List<MenuDto>>(MenusDto);
            }
        }
    }
}
