using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.Administracion;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.SolicitudesDePlanes.Queries.GetPrestamosYAhorrosPorCompania
{
    public class GetPrestamosYAhorrosPorCompaniaCommand: IRequest<Response<List<GetPrestamosYAhorrosPorCompaniaCommandDto>>>
    {
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<GetPrestamosYAhorrosPorCompaniaCommand, Response<List<GetPrestamosYAhorrosPorCompaniaCommandDto>>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsyncRetiroAhorroVoluntario;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsyncPrestamo, IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario, 
                IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsyncRetiroAhorroVoluntario)
            {
                _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _repositoryAsyncRetiroAhorroVoluntario = repositoryAsyncRetiroAhorroVoluntario;
            }

            public async Task<Response<List<GetPrestamosYAhorrosPorCompaniaCommandDto>>> Handle(GetPrestamosYAhorrosPorCompaniaCommand request, CancellationToken cancellationToken)
            {
                var listaPrestamos = await _repositoryAsyncPrestamo.ListAsync(new PrestamosByCompanyIdSpecification(request.CompanyId));
                var listaAhorros = await _repositoryAsyncAhorroVoluntario.ListAsync(new AhorrosVoluntariosByCompanyIdSpecification(request.CompanyId));

                List<GetPrestamosYAhorrosPorCompaniaCommandDto> listaCombinada = new();

                foreach (var prestamo in listaPrestamos)
                {
                    GetPrestamosYAhorrosPorCompaniaCommandDto item = new();

                    item.Tipo = "Préstamo";
                    item.Id = prestamo.Id;
                    var employee = await _repositoryAsyncEmployee.GetByIdAsync(prestamo.EmployeeId);
                    item.NombreEmpleado = employee.NombreCompleto();
                    item.Monto = prestamo.Monto;
                    item.Estatus = prestamo.Estatus.ToString();
                    item.FechaCreacion = (DateTime)prestamo.Created;
                    // Esto se moverá cuando se defina bien qué columnas de las tablas utilizar
                    item.PagareSrc = prestamo.SrcDocPagare;
                    item.AcuseSrc = prestamo.SrcDocAcuseFirmado;
                    item.EmployeeId = prestamo.EmployeeId;
                    listaCombinada.Add(item);
                }

                foreach(var ahorro in listaAhorros)
                {
                    GetPrestamosYAhorrosPorCompaniaCommandDto item = new();

                    item.Tipo = "Ahorro voluntario";
                    item.Id = ahorro.Id;
                    var employee = await _repositoryAsyncEmployee.GetByIdAsync(ahorro.EmployeeId);
                    item.NombreEmpleado = employee.NombreCompleto();
                    item.Monto = ahorro.Descuento;
                    item.Estatus = ahorro.Estatus.ToString();
                    item.FechaCreacion = (DateTime)ahorro.Created;
                    // Esto se moverá cuando se defina bien qué columnas de las tablas utilizar
                    item.PagareSrc = "";
                    item.AcuseSrc = ahorro.SrcCartaFirmada;
                    item.EmployeeId = ahorro.EmployeeId;
                    listaCombinada.Add(item);

                    var retiroAhorro = await _repositoryAsyncRetiroAhorroVoluntario.ListAsync(new RetiroAhorroVoluntarioByAhorroIdSpecification(ahorro.Id));
                    if (retiroAhorro != null)
                    {
                        foreach(var retiro in retiroAhorro)
                        {
                            GetPrestamosYAhorrosPorCompaniaCommandDto items = new();
                            items.Tipo = "Retiro ahorro voluntario";
                            items.Id = retiro.Id;
                            items.NombreEmpleado = employee.NombreCompleto();
                            items.Monto = retiro.Cantidad;
                            items.Estatus = retiro.Estatus.ToString();
                            items.FechaCreacion = (DateTime)retiro.Created;
                            // Esto se moverá cuando se defina bien qué columnas de las tablas utilizar
                            items.PagareSrc = retiro.SrcDocContanciaTransferencia;
                            items.AcuseSrc = retiro.SrcDocSolicitudFirmado;
                            items.EmployeeId = ahorro.EmployeeId;
                            items.AhorroId = retiro.AhorroVoluntarioId;
                            listaCombinada.Add(items);
                        }
                    }
                }

                List<GetPrestamosYAhorrosPorCompaniaCommandDto> listaOrdenada = new();
                listaOrdenada = listaCombinada.OrderByDescending(x => x.FechaCreacion).ToList();

                Response<List<GetPrestamosYAhorrosPorCompaniaCommandDto>> respuesta = new();

                respuesta.Succeeded = true;
                respuesta.Data = listaOrdenada;

                return respuesta;
            }
        }   


    }
}
