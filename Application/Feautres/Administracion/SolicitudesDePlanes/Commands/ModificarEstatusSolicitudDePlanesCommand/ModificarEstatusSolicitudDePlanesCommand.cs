using Application.Interfaces;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.SolicitudesDePlanes.Commands.ModificarEstatusSolicitudDePlanesCommand
{
    public class ModificarEstatusSolicitudDePlanesCommand : IRequest<Response<int>>
    {
        public int CompanyId { get; set; }
        public string Tipo { get; set; }
        public int Id { get; set; }
        public int NuevoEstatusId { get; set; }

        public class Handler : IRequestHandler<ModificarEstatusSolicitudDePlanesCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsyncRetiroAhorroVoluntario;
            private readonly IRetiroAhorroVoluntarioService _retiroAhorroVoluntarioService;
            private readonly IAhorroVoluntarioService _ahorroVoluntarioService;
            private readonly IPrestamoService _prestamoService;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsyncPrestamo, IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario,
                IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsyncRetiroAhorroVoluntario, IRetiroAhorroVoluntarioService retiroAhorroVoluntarioService, 
                IAhorroVoluntarioService ahorroVoluntarioService, IPrestamoService prestamoService)
            {
                _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
                _repositoryAsyncRetiroAhorroVoluntario = repositoryAsyncRetiroAhorroVoluntario;
                _retiroAhorroVoluntarioService = retiroAhorroVoluntarioService;
                _ahorroVoluntarioService = ahorroVoluntarioService;
                _prestamoService = prestamoService;
            }
            public async Task<Response<int>> Handle(ModificarEstatusSolicitudDePlanesCommand request, CancellationToken cancellationToken)
            {
                Response<int> respuesta = new();
                switch (request.Tipo)
                {
                    case "Préstamo":
                        var prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(request.Id);
                        if (prestamo == null)
                        {
                            respuesta.Succeeded = false;
                            respuesta.Message = "El préstamo que intenta modificar no existe";
                            respuesta.Data = -1;
                        }
                        else
                        {
                            if (prestamo.CompanyId == request.CompanyId)
                            {
                                prestamo.Estatus = (EstatusOperacion)request.NuevoEstatusId;

                                if (prestamo.Estatus == EstatusOperacion.Activo)
                                {
                                    await _prestamoService.EnviarCorreoEstatus(prestamo);
                                }

                                await _repositoryAsyncPrestamo.UpdateAsync(prestamo);
                                respuesta.Succeeded = true;
                                respuesta.Message = "El estatus del préstamo se ha actualizado con éxito";
                                respuesta.Data = prestamo.Id;
                            }
                            else
                            {
                                respuesta.Succeeded = false;
                                respuesta.Message = "El préstamo que intenta modificar no pertenece a la compañia actual";
                                respuesta.Data = -1;
                            }
                        }
                        break;
                    case "Ahorro voluntario":
                        var ahorro = await _repositoryAsyncAhorroVoluntario.GetByIdAsync(request.Id);
                        if (ahorro == null)
                        {
                            respuesta.Succeeded = false;
                            respuesta.Message = "El ahorro que intenta modificar no existe";
                            respuesta.Data = -1;
                        }
                        else
                        {
                            if (ahorro.CompanyId == request.CompanyId)
                            {
                                ahorro.Estatus = (EstatusOperacion)request.NuevoEstatusId;
                                if (ahorro.Estatus == EstatusOperacion.Activo)
                                {
                                    await _ahorroVoluntarioService.EnviarCorreoEstatus(ahorro);
                                }
                                await _repositoryAsyncAhorroVoluntario.UpdateAsync(ahorro);
                                respuesta.Succeeded = true;
                                respuesta.Message = "El estatus del ahorro se ha actualizado con éxito";
                                respuesta.Data = ahorro.Id;
                            }
                            else
                            {
                                respuesta.Succeeded = false;
                                respuesta.Message = "El ahorro que intenta modificar no pertenece a la compañia actual";
                                respuesta.Data = -1;
                            }
                        }
                        break;
                    case "Retiro ahorro voluntario":
                        RetiroAhorroVoluntario retiro_ahorro_voluntario = await _repositoryAsyncRetiroAhorroVoluntario.GetBySpecAsync(new RetiroAhorroVoluntarioByIdSpecification(request.Id));
                        if (retiro_ahorro_voluntario == null)
                        {
                            respuesta.Succeeded = false;
                            respuesta.Message = "El retiro de ahorro que intenta modificar no existe";
                            respuesta.Data = -1;
                        }
                        else
                        {
                            AhorroVoluntario ahorro_voluntario = await _repositoryAsyncAhorroVoluntario.GetByIdAsync(retiro_ahorro_voluntario.AhorroVoluntarioId);
                            if (ahorro_voluntario == null)
                            {
                                respuesta.Succeeded = false;
                                respuesta.Message = "El ahorro que intenta modificar no existe";
                                respuesta.Data = -1;
                            }
                            else
                            { 
                                if (ahorro_voluntario.CompanyId == request.CompanyId)
                                {
                                    retiro_ahorro_voluntario.Estatus = (EstatusRetiro)request.NuevoEstatusId;

                                    if (retiro_ahorro_voluntario.Estatus == EstatusRetiro.Autorizado)
                                    {
                                        await _retiroAhorroVoluntarioService.AddMovimientoRetiroAhorroVoluntario(retiro_ahorro_voluntario, ahorro_voluntario);

                                        await _retiroAhorroVoluntarioService.EnviarCorreoEstatus(retiro_ahorro_voluntario, ahorro_voluntario);
                                    }

                                    if (retiro_ahorro_voluntario.SeguirAhorrando == false)
                                    {
                                        ahorro_voluntario.Estatus = EstatusOperacion.Finiquitado;
                                        await _repositoryAsyncAhorroVoluntario.UpdateAsync(ahorro_voluntario);
                                    }

                                    await _repositoryAsyncRetiroAhorroVoluntario.UpdateAsync(retiro_ahorro_voluntario);
                                    respuesta.Succeeded = true;
                                    respuesta.Message = "El estatus del retiro de ahorro se ha actualizado con éxito";
                                    respuesta.Data = retiro_ahorro_voluntario.Id;
                                }
                                else
                                {
                                    respuesta.Succeeded = false;
                                    respuesta.Message = "El retiro de ahorro que intenta modificar no pertenece a la compañia actual";
                                    respuesta.Data = -1;
                                }

                            }
                        }
                        break;
                    default:
                        respuesta.Succeeded = false;
                        respuesta.Message = "No se reconoce el Tipo proporcionado";
                        respuesta.Data = -1;
                        break;
                }
                return respuesta;

            }
        }
    }
}
