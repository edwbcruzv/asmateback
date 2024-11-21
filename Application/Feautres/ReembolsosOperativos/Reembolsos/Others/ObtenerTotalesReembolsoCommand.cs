using Application.DTOs.ReembolsosOperativos;
using Application.Interfaces;
using Application.Specifications.Employees;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Others
{
    public class ObtenerTotalesReembolsoCommand : IRequest<Response<TotalesReembolsoDto>>
    {
        public int ReembolsoId {  get; set; }
        

        public class Handler: IRequestHandler<ObtenerTotalesReembolsoCommand, Response<TotalesReembolsoDto>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IReembolsoService _reembolsoService;
            private readonly IRepositoryAsync<User> _repositoryAsyncUser;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;

            public Handler(IRepositoryAsync<Reembolso> repositoryAsyncReembolso, IReembolsoService reembolsoService, IRepositoryAsync<User> repositoryAsyncUser, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Banco> repositoryAsyncBanco)
            {
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _reembolsoService = reembolsoService;
                _repositoryAsyncUser = repositoryAsyncUser;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _repositoryAsyncBanco = repositoryAsyncBanco;
            }

            public async Task<Response<TotalesReembolsoDto>> Handle(ObtenerTotalesReembolsoCommand request, CancellationToken cancellationToken)
            {
                var reembolso = await _repositoryAsyncReembolso.GetByIdAsync(request.ReembolsoId);

                if (reembolso == null)
                {
                    throw new KeyNotFoundException($"No se encontró el reembolso con Id {request.ReembolsoId}");
                }
                else
                {
                    var user = await _repositoryAsyncUser.GetByIdAsync((int)reembolso.UsuarioIdPago);
                    if (user == null)
                    {
                        throw new KeyNotFoundException($"No se encontró el usuario con Id {reembolso.UsuarioIdPago}");
                    }
                    else
                    {
                        var employee = await _repositoryAsyncEmployee.FirstOrDefaultAsync(new EmployeeByUserIdSpecification(reembolso.UsuarioIdPago));
                        if (employee == null)
                        {
                            throw new KeyNotFoundException($"No se encontró el empleado con asociado al usuario {reembolso.UsuarioIdPago}");
                        }
                        else
                        {

                            var banco = await _repositoryAsyncBanco.GetByIdAsync(employee.BancoId);

                            // Llaves para obtener valores del diccionario de totales
                            string subTotal = "SubTotal";
                            string descuento = "Descuento";
                            string impuestosRetenidos = "ImpuestosRetenidos";
                            string impuestosTrasladados = "ImpuestosTrasladados";
                            string ieps = "IEPS";
                            string ish = "ISH";
                            string total = "TotalPagar";

                            TotalesReembolsoDto dto = new TotalesReembolsoDto();

                            Dictionary<string, double> diccionarioTotales = await _reembolsoService.ObtenerTotalesReembolso(request.ReembolsoId);

                            if (reembolso.UsuarioIdPago != null)
                            {
                                dto.Usuario = user.NickName;
                            }

                            if (reembolso.Created != null)
                            {
                                dto.FechaCaptutra = (DateTime)reembolso.Created;
                            }

                            dto.Descripcion = reembolso.Descripcion;

                            dto.Cuenta = employee.NoCuenta;

                            if (banco != null)
                            {
                                dto.Banco = banco.Descripcion;
                            }

                            dto.Subtotal = diccionarioTotales[subTotal];
                            dto.Descuento = diccionarioTotales[descuento];
                            dto.ImpuestosRetenidos = diccionarioTotales[impuestosRetenidos];
                            dto.ImpuestosTrasladados = diccionarioTotales[impuestosTrasladados];
                            dto.IEPS = diccionarioTotales[ieps];
                            dto.ISH = diccionarioTotales[ish];
                            dto.TotalPagar = diccionarioTotales[total];
                            


                            Response<TotalesReembolsoDto> respuesta = new Response<TotalesReembolsoDto>();
                            respuesta.Succeeded = true;
                            respuesta.Data = dto;

                            return respuesta;
                        }
                    }
                   
                   
                }
            }
        }

    }
}
