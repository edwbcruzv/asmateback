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

namespace Application.Feautres.Kanban.SistemasDepartamentos.Commands.Create
{
    public class CreateSistemaDepartamentoCommand : IRequest<Response<Boolean>>
    {
        public int SistemaId { get; set; }
        public int DepartamentoId { get; set; }

        public class Handler : IRequestHandler<CreateSistemaDepartamentoCommand, Response<Boolean>>
        {
            private readonly IRepositoryAsync<SistemaDepartamento> _repositoryAsyncSistemaDepartamento;

            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<SistemaDepartamento> repositoryAsyncSistemaDepartamento,
                        IMapper mapper)
            {
                _repositoryAsyncSistemaDepartamento = repositoryAsyncSistemaDepartamento;
                _mapper = mapper;
            }

            public async Task<Response<bool>> Handle(CreateSistemaDepartamentoCommand request, CancellationToken cancellationToken)
            {
                
                var sistema_departamento = _mapper.Map<SistemaDepartamento>(request);

                var data = await _repositoryAsyncSistemaDepartamento.AddAsync(sistema_departamento);

                if (data != null)
                {
                    return new Response<Boolean>(true);
                }
                else
                {
                    return new Response<Boolean>(false,"Error en la asignacion de departamento al sistema.");
                }
            }
            
        }

    }
}
