using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Usuarios.MenuUserSelectors.Commands.CreateMenuUserSelectorCommand
{
    public class CreateMenuUserSelectorCommand : IRequest<Response<int>>
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
    }
    public class Handler : IRequestHandler<CreateMenuUserSelectorCommand, Response<int>>
    {
        private readonly IRepositoryAsync<MenuUserSelector> _repositoryAsync;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<MenuUserSelector> repositoryAsync, IMapper mapper, IRsa rsa, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;


        }

        public async Task<Response<int>> Handle(CreateMenuUserSelectorCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<MenuUserSelector>(request);

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
