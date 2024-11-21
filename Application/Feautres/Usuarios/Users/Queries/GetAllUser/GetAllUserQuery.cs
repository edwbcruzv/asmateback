using Application.DTOs.Usuarios;
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

namespace Application.Feautres.Usuarios.Users.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<Response<List<UserDto>>>
    {
        public class Handler : IRequestHandler<GetAllUserQuery, Response<List<UserDto>>>
        {
            private readonly IRepositoryAsync<User> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<User> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                var users = await _repositoryAsync.ListAsync();

                var usersDto = _mapper.Map<List<UserDto>>(users);

                return new Response<List<UserDto>>(usersDto);
            }
        }
    }
}
