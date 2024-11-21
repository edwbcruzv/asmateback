using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    /*
     * DbContext es la representacion de la base de datos a nivel de .NET, via EntityFramework.
     * 
     */
    public class ApplicationDbContext : DbContext
    {

        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime,
            IAuthenticatedUserService authenticatedUserService) : base (options)
        {
            // 
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUserService = authenticatedUserService;
        }
        //public DbSet<Biometric> Biometrics { get; set; }

        public DbSet<AhorroVoluntario> AhorrosVoluntario { get; set; }
        public DbSet<AhorroWise> AhorrosWise { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CodigoPostale> CodigoPostales { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ComplementoPago> ComplementoPagos { get; set; }
        public DbSet<ComplementoPagoFactura> ComplementoPagoFacturas { get; set; }
        public DbSet<Comprobante> Comprobantes { get; set; }
        public DbSet<ComprobanteSinXML> ComprobantesSinXML { get; set; }
        public DbSet<ContractsUserCompany> ContractsUserCompanies { get; set; }
        public DbSet<CveProducto> CveProductos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Estado> Estados { get; set; }
        //public DbSet<EmployeeBiometricsDatum> EmployeeBiometricsData { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaMovimiento> FacturaMovimientos { get; set; }
        public DbSet<FormaPago> FormaPagos { get; set; }
        public DbSet<ImssDescuento> ImssDescuentos { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuUserSelector> MenuUserSelectors { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<MovimientoAhorroVoluntario> MovimientosAhorrosVoluntario { get; set; }
        public DbSet<MovimientoAhorroWise> MovimientosAhorrosWise { get; set; }
        public DbSet<MovimientoPrestamo> MovimientosPrestamo { get; set; }
        public DbSet<MovimientoReembolso> MovimientoReembolsos { get; set; }
        public DbSet<Nomina> Nominas { get; set; }
        public DbSet<NominaPercepcion> NominaPercepciones { get; set; }
        public DbSet<NominaOtroPago> NominaOtroPagos { get; set; }
        public DbSet<NominaDeduccion> NominaDeducciones { get; set; }
        //public DbSet<Nif> Nifs { get; set; }
        //public DbSet<Nifresultado> Nifresultados { get; set; }
        public DbSet<ObjetoImpuesto> ObjetoImpuestos { get; set; }
        public DbSet<RegimenFiscal> RegimenFiscals { get; set; }
        public DbSet<Reembolso> Rembolsos { get; set; }
        public DbSet<RetiroAhorroVoluntario> RetirosAhorroVoluntario { get; set; }
        public DbSet<SalarioMinimo> SalarioMinimos { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<SistemaDepartamento> SistemaDepartamento { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<SubMenuUserSelector> SubMenuUserSelectors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TipoComprobante> TipoComprobantes { get; set; }
        public DbSet<TipoMoneda> TipoMonedas { get; set; }
        public DbSet<TipoImpuesto> TipoImpuestos { get; set; }
        public DbSet<TipoContrato> TipoContratos { get; set; }
        public DbSet<TipoRegimen> TipoRegimens { get; set; }
        public DbSet<TipoRiesgoTrabajo> TipoRiesgoTrabajos { get; set; }
        public DbSet<TipoIncapacidad> TipoIncapacidads { get; set; }
        public DbSet<TipoAsistencia> TipoAsistencias { get; set; }
        public DbSet<TipoDeduccion> TipoDeducciones { get; set; }
        public DbSet<TipoPercepcion> TipoPercepciones { get; set; }
        public DbSet<TipoOtroPago> TipoOtroPagos { get; set; }
        public DbSet<TipoSolicitudTicket> TipoSolicitudTickets { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<RegistroAsistencia> RegistroAsistencias { get; set; }
        public DbSet<Uma> Umas { get; set; }
        public DbSet<UnidadMedida> UnidadMedidas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsoCfdi> UsoCfdis { get; set; }
        public DbSet<Isr> Isrs { get; set; }
        public DbSet<Subsidio> Subsidios { get; set; }
        public DbSet<Vacacion> Vacaciones { get; set; }
        public DbSet<Viatico> Viaticos { get; set; }

        /* gusarda todos los cambios en la base de datos y hace un commit
         * despues de hacer nuestras operaciones sobre la base de datos
         */
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {


            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _authenticatedUserService.UserId;
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _authenticatedUserService.UserId;
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        /*
         * 
         * 
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
