using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Viaticos.Commands.UpdateViaticoCommand
{
    public class UpdateViaticoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Fecha { get; set; }
        public int CompanyId { get; set; }
        public EstatusViatico Estatus { get; set; }
        public float Monto { get; set; }
        public String Descripcion { get; set; }
        public int? BancoId { get; set; }
        public string? NoCuenta { get; set; }
        public IFormFile? PDF { get; set; }
        public int? EmployeePagoId { get; set; }
        public DateTime? FechaPago { get; set; }
        public float? MontoRecibido { get; set; }
        //public TipoViatico Tipo { get; set; }

        public class Handler : IRequestHandler<UpdateViaticoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Viatico> repositoryAsyncViatico, IMapper mapper)
            {
                _repositoryAsyncViatico = repositoryAsyncViatico;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateViaticoCommand request, CancellationToken cancellationToken)
            {
                var viatico = await _repositoryAsyncViatico.GetByIdAsync(request.Id);

                if (viatico == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        viatico.EmployeeId = request.EmployeeId;
                        viatico.Fecha = request.Fecha;
                        viatico.CompanyId = request.CompanyId;
                        viatico.Estatus = request.Estatus;
                        viatico.Descripcion = request.Descripcion;
                        viatico.BancoId = request.BancoId;
                        viatico.NoCuenta = request.NoCuenta;
                        viatico.EmployeeId = request.EmployeeId;
                        viatico.FechaPago = request.FechaPago;
                        //viatico.Tipo = request.Tipo;

                        await _repositoryAsyncViatico.UpdateAsync(viatico);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al asignar nuevo viatico." + ex.ToString());
                    }

                    return new Response<int>(viatico.Id);
                }
            }
        }
    }
}
