using Application.DTOs.Usuarios;
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

namespace Application.Feautres.Usuarios.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Response<UserDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetUserByIdQuery, Response<UserDto>>
        {
            private readonly IRepositoryAsync<User> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<User> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                if (item == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<UserDto>(item);
                    return new Response<UserDto>(dto);
                }

            }
        }
    }
}
