using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Feautres.Kanban.Sistemas.Commands.CreateSistema
{
    public class CreateSistemaCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string? Color { get; set; }
        public int EstadoId { get; set; }

    }

    public class Handler : IRequestHandler<CreateSistemaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;

        private readonly IMapper _mapper;

        public Handler(IRepositoryAsync<Sistema> repositoryAsyncSistema,
                    IMapper mapper)
        {
            _repositoryAsyncSistema = repositoryAsyncSistema;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateSistemaCommand request, CancellationToken cancellationToken)
        {
            var sistema = _mapper.Map<Sistema>(request);

            var data = await _repositoryAsyncSistema.AddAsync(sistema);
            return new Response<int>(data.Id);
        }
    }
}
