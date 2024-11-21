using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosWise.Commands.UpdateAhorroWiseCommand
{
    public class UpdateAhorroWiseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        public int PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }
        public float Rendimiento { get; set; }
        public IFormFile FileConstancia { get; set; }
        public IFormFile FilePago { get; set; }

        public class Handler : IRequestHandler<UpdateAhorroWiseCommand, Response<int>>
        {
            private readonly IRepositoryAsync<AhorroWise> _repositoryAsyncAhorroWise;
            private readonly IFilesManagerService _filesManagerService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroWise> repositoryAsyncAhorroWise, IMapper mapper, IFilesManagerService filesManagerService)
            {
                _repositoryAsyncAhorroWise = repositoryAsyncAhorroWise;
                _mapper = mapper;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<int>> Handle(UpdateAhorroWiseCommand request, CancellationToken cancellationToken)
            {
                var ahorro_wise = await _repositoryAsyncAhorroWise.GetByIdAsync(request.Id);

                if (ahorro_wise == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        ahorro_wise.EmployeeId = request.EmployeeId;
                        ahorro_wise.CompanyId = request.CompanyId;
                        ahorro_wise.FechaInicio = request.FechaInicio;
                        ahorro_wise.FechaFinal = request.FechaFinal;
                        ahorro_wise.PeriodoInicial = request.PeriodoInicial;
                        ahorro_wise.PeriodoFinal = request.PeriodoFinal;
                        ahorro_wise.Estatus = request.Estatus;
                        ahorro_wise.Rendimiento = request.Rendimiento;

                        if (request.FileConstancia != null)
                        {
                            _filesManagerService.UpdateFile(request.FileConstancia, ahorro_wise.SrcFileConstancia);
                        }

                        if (request.FilePago != null)
                        {
                            _filesManagerService.UpdateFile(request.FilePago, ahorro_wise.SrcFilePago);
                        }



                        await _repositoryAsyncAhorroWise.UpdateAsync(ahorro_wise);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }

                    return new Response<int>(ahorro_wise.Id);
                }

            }
        }
    }
}
