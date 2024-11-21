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

namespace Application.Feautres.Administracion.Menus.Queries.GetMenuById
{
    public class GetMenuByIdQuery : IRequest<Response<MenuDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetMenuByIdQuery, Response<MenuDto>>
        {
            private readonly IRepositoryAsync<Menu> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Menu> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<MenuDto>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                if (item == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<MenuDto>(item);
                    return new Response<MenuDto>(dto);
                }

            }
        }
    }
}
