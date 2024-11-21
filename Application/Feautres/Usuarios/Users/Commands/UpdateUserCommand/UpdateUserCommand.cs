using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Users;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Usuarios.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string? UserProfile { get; set; }
        public string UserEmail { get; set; }
        public byte UserType { get; set; }
        public IFormFile? File { get; set; }
        public string? UserTipo { get; set; }

    }
    public class Handler : IRequestHandler<UpdateUserCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private readonly IMapper _mapper;
        private readonly IFilesManagerService _filesManagerService;


        public Handler(IRepositoryAsync<User> repositoryAsync, IMapper mapper, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _filesManagerService = filesManagerService;

        }

        public async Task<Response<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repositoryAsync.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {

                var verifyNickname = await _repositoryAsync.FirstOrDefaultAsync(new UserByNickNameAndNotIdSpecification(request.NickName, request.Id));

                if (verifyNickname != null)
                {
                    throw new KeyNotFoundException($"Nickname {request.NickName} ya se encuentra ocupado");
                }
                else
                {
                    user.UserName = request.UserName;
                    user.NickName = request.NickName;

                    if (request.File?.Length > 0)
                        user.UserProfile = _filesManagerService.saveUserPhoto(request.File, request.NickName);

                    user.UserEmail = request.UserEmail;
                    user.UserType = request.UserType;

                    await _repositoryAsync.UpdateAsync(user);

                    return new Response<int>(user.Id);

                }

            }
        }
    }
}
