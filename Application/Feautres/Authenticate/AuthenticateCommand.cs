using Application.DTOs;
using Application.Exceptions;
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

namespace Application.Feautres.Authtenticate.Commands.AuthenticateCommand
{

    public class AuthenticateCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string NickName { get; set; }
        public string PasswordAux { get; set; }

    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponse>>
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IRepositoryAsync<User> _repositoryAsync;

        public AuthenticateCommandHandler(IAuthenticateService authenticateService, IRepositoryAsync<User> repositoryAsync)
        {
            _authenticateService = authenticateService;
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _authenticateService.AuthenticateAsync(request.NickName, request.PasswordAux);

        }
    }
}
