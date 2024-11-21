using Application.DTOs;
using Application.DTOs.Administracion;
using Application.DTOs.Catalogos;
using Application.DTOs.Facturas;
using Application.DTOs.Kanban.Sistemas;
using Application.DTOs.Kanban.SistemasDepartamentos;
using Application.DTOs.Kanban.Tickets;
using Application.DTOs.MiPortal.Ahorros;
using Application.DTOs.MiPortal.Comprobantes;
using Application.DTOs.MiPortal.ComprobantesSinXML;
using Application.DTOs.MiPortal.Incidencias;
using Application.DTOs.MiPortal.Prestamos;
using Application.DTOs.MiPortal.Viaticos;
using Application.DTOs.ReembolsosOperativos;
using Application.DTOs.Usuarios;
using Application.Feautres.Administracion.Clientes.Commands.CreateClienteCommand;
using Application.Feautres.Administracion.Companies.Commands.CreateCompanyCommand;
using Application.Feautres.Administracion.Departamentos.Commands.CreateDepartamento;
using Application.Feautres.Administracion.Employees.Commands.CreateEmployeeCommand;
using Application.Feautres.Administracion.Employees.Commands.UpdateEmployeeCommand;
using Application.Feautres.Administracion.Menus.Commands.CreateMenuCommand;
using Application.Feautres.Administracion.Puestos.Commands.CreatePuesto;
using Application.Feautres.Administracion.SubMenus.Commands.CreateSubMenuCommand;
using Application.Feautres.Catalogos.TipoPeriocidadPagos.Queries.GetTipoPeriocidadPagoById;
using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.CreateComplementoPagoFacturaCommand;
using Application.Feautres.Facturacion.ComplementoPagos.Commands.CreateComplementoPagoCommand;
using Application.Feautres.Facturacion.FacturaMovimientos.Commands.CreateFacturaMovimientoCommand;
using Application.Feautres.Facturacion.Facturas.Commands.CreateFacturaCommand;
using Application.Feautres.Facturacion.Nominas.Commands.GeneratePeriodoExtraordinarioCommand;
using Application.Feautres.Kanban.Sistemas.Commands.CreateSistema;
using Application.Feautres.Kanban.SistemasDepartamentos.Commands.Create;
using Application.Feautres.Kanban.Tickets.Commands.CreateTicket;
using Application.Feautres.MiPortal.AhorrosVoluntario.Commands.CreateAhorroVoluntarioCommand;
using Application.Feautres.MiPortal.AhorrosWise.Commands.CreateAhorroWiseCommand;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteCommand;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteFacturaExtrangera;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobantePagoImpuestos;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteSinXML;
using Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteValeAzulCommand;
//using Application.Feautres.MiPortal.ComprobantesSinXML.Commands.CreateComprobanteSinXML;
using Application.Feautres.MiPortal.Incidencias.Commands.CreateIncidenciaCommand;
using Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.CreateMovimientoAhorroVoluntario;
using Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.CreateMovimientoAhorroWise;
using Application.Feautres.MiPortal.MovimientosPrestamo.Commands.CreateMovimientoPrestamo;
using Application.Feautres.MiPortal.Prestamos.Commands.CreatePrestamoCommand;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.CreateRetiroAhorroVoluntario;
using Application.Feautres.MiPortal.Viaticos.Commands.CreateViaticoCommand;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolso;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaExtrangera;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaSinXML;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByPagoImpuestos;
using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByValeAzul;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.CreateReembolsoCommand;
using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.SendReembolsoCommand;
using Application.Feautres.Usuarios.ContractsUserCompanies.Commands.CreateContractsUserCompanies;
using Application.Feautres.Usuarios.MenuUserSelectors.Commands.CreateMenuUserSelectorCommand;
using Application.Feautres.Usuarios.SubMenuUserSelectors.Commands.CreateSubMenuUserSelectorsCommand;
using Application.Feautres.Usuarios.Users.Commands.CreateUserCommand;
using AutoMapper;
using Domain.Entities;



namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Dtos
            CreateMap<Client, ClientDto>();
            CreateMap<User, AuthenticationResponse>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<CodigoPostale, CodigoPostaleDto>();
            CreateMap<CveProducto, CveProductoDto>();
            CreateMap<FormaPago, FormaPagoDto>();
            CreateMap<MetodoPago, MetodoPagoDto>();
            CreateMap<TipoMoneda, TipoMonedaDto>();
            CreateMap<TipoComprobante, TipoComprobanteDto>();
            CreateMap<ObjetoImpuesto, ObjetoImpuestoDto>();
            CreateMap<UnidadMedida, UnidadMedidaDto>();
            CreateMap<UsoCfdi, UsoCfdiDto>();
            CreateMap<UnidadMedida, UnidadMedidaDto>();
            CreateMap<User, UserDto>();
            CreateMap<Menu, MenuDto>();
            CreateMap<SubMenu, SubMenuDto>();
            CreateMap<Factura, FacturaDto>();
            CreateMap<Factura, FacturaPDDDto>();
            CreateMap<FacturaMovimiento, FacturaMovimientoDto>();
            CreateMap<RegimenFiscal, RegimenFiscalDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Banco, BancoDto>();
            CreateMap<Puesto, PuestoDto>();
            CreateMap<Departamento, DepartamentoDto>();
            CreateMap<TipoContrato, TipoContratoDto>();
            CreateMap<TipoRegimen, TipoRegimenDto>();
            CreateMap<TipoRiesgoTrabajo, TipoRiesgoTrabajoDto>();
            CreateMap<TipoIncapacidad, TipoIncapacidadDto>();
            CreateMap<TipoJornada, TipoJornadaDto>();
            CreateMap<TipoPeriocidadPago, TipoPeriocidadPagoDto>();
            CreateMap<Periodo, PeriodoDto>();
            CreateMap<ComplementoPago, ComplementoPagoDto>();
            CreateMap<ComplementoPagoFactura, ComplementoPagoFacturaDto>();
            CreateMap<Reembolso, ReembolsoDTO>();
            CreateMap<RetiroAhorroVoluntario, RetiroAhorroVoluntarioDTO>();
            CreateMap<MovimientoReembolso, MovimientoReembolsoDTO>();
            CreateMap<MovimientoReembolso, MovimientoReembolsoListDTO>();
            CreateMap<TipoImpuesto, TipoImpuestoDto>();
            CreateMap<Ticket, TicketDTO>();
            CreateMap<Sistema, SistemaDTO>();
            CreateMap<SistemaDepartamento, SistemaDepartamentoDTO>();
            CreateMap<Incidencia, IncidenciaDTO>();
            CreateMap<TipoImpuesto, TipoImpuestoDto>();
            CreateMap<Viatico, ViaticoDTO>();
            CreateMap<Comprobante, ComprobanteDTO>();
            CreateMap<ComprobanteSinXML, ComprobanteSinXMLDTO>();
            CreateMap<Nomina, NominaDTO>();
            CreateMap<Prestamo, PrestamoDTO>();
            CreateMap<AhorroVoluntario,AhorroVoluntarioDTO>().ForMember(dest => dest.EstatusString, opt => opt.MapFrom(src => src.Estatus.ToString()))
                                                            .ForMember(dest => dest.MontoTotal, opt => opt.MapFrom(src => src.SumaMontoTotalMov()))
                                                            .ForMember(dest => dest.EmployeeNombreCompleto, opt => opt.MapFrom(src => src.Employee.NombreCompletoOrdenado()))
                                                            .ForMember(dest => dest.EmployeeRFC, opt => opt.MapFrom(src => src.Employee.Rfc));
            CreateMap<AhorroWise,AhorroWiseDTO>();
            CreateMap<MovimientoAhorroVoluntario,MovimientoAhorroVoluntarioDTO>().ForMember(dest => dest.EstadoTransaccionString, opt => opt.MapFrom(src => src.EstadoTransaccion.ToString()));
            CreateMap<MovimientoAhorroWise, MovimientoAhorroWiseDTO>();
            CreateMap<MovimientoPrestamo, MovimientoPrestamoDTO>();
            CreateMap<Puesto, PuestoDto>();

            #endregion
            #region Commands
            CreateMap<CreateClientCommand, Client>();
            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<CreateContractsUserCompanyCommand, ContractsUserCompany>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<CreateMenuCommand, Menu>();
            CreateMap<CreateSubMenuCommand, SubMenu>();
            CreateMap<CreateMenuUserSelectorCommand, MenuUserSelector>();
            CreateMap<CreateSubMenuUserSelectorCommand, SubMenuUserSelector>();
            CreateMap<CreateFacturaCommand, Factura>();
            CreateMap<CreateFacturaMovimientoCommand, FacturaMovimiento>();
            CreateMap<CreateDepartamentoCommand, Departamento>();
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();
            CreateMap<CreateComplementoPagoCommand, ComplementoPago>();
            CreateMap<CreateComplementoPagoFacturaCommand, ComplementoPagoFactura>();
            CreateMap<CreateReembolsoCommand, Reembolso>();
            CreateMap<CreateMovimientoReembolsoFacturaCommand, MovimientoReembolso>();
            CreateMap<CreateMovimientoReembolsoByFacturaSinXMLCommand, MovimientoReembolso>();
            CreateMap<CreateMovimientoReembolsoByPagoImpuestosCommand, MovimientoReembolso>();
            CreateMap<CreateMovimientoReembolsoByValeAzulCommand, MovimientoReembolso>();
            CreateMap<CreateMovimientoReembolsoByFacturaExtranjeraCommand, MovimientoReembolso>();
            CreateMap<CreateTicketCommand, Ticket>();
            CreateMap<GeneratePeriodoExtraordinarioCommand, Periodo>();
            CreateMap<GetTipoPeriocidadPagoByIdQuery, TipoPeriocidadPago>();
            CreateMap<CreateSistemaCommand, Sistema>();
            CreateMap<CreateIncidenciaVacacionesCommand, Incidencia>();
            CreateMap<CreateIncidenciaCualquieraCommand, Incidencia>();
            CreateMap<CreateViaticoCommand, Viatico>();
            CreateMap<CreateComprobanteCommand, Comprobante>();
            CreateMap<CreateComprobanteValeAzulCommand, Comprobante>();
            CreateMap<CreateComprobanteSinXMLCommand, Comprobante>();
            CreateMap<CreateComprobanteFacturaExtranjeraCommand, Comprobante>();
            CreateMap<CreateComprobantePagoImpuestosCommand, Comprobante>();
            CreateMap<CreateIncidenciaIncapacidadCommand, Incidencia>();
            CreateMap<CreatePrestamoCommand, Prestamo>();
            CreateMap<CreateSistemaDepartamentoCommand, SistemaDepartamento>();
            CreateMap<CreateAhorroVoluntarioCommand, AhorroVoluntario>();
            CreateMap<CreateRetiroAhorroVoluntarioCommand, RetiroAhorroVoluntario>();
            CreateMap<CreateAhorroWiseCommand, AhorroWise>();
            CreateMap<CreateMovimientoAhorroVoluntarioCommand, MovimientoAhorroVoluntario>();
            CreateMap<CreateMovimientoAhorroWiseCommand,MovimientoAhorroWise>();
            CreateMap<CreateMovimientoPrestamoCommand,MovimientoPrestamo>();
            CreateMap<CreatePuestoCommand, Puesto>();


            #endregion
        }
    }
}
