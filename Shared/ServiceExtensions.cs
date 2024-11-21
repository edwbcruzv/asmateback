using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAhorroVoluntarioService, AhorroVoluntarioService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IFilesManagerService, FilesManagerService>();
            services.AddTransient<IDepartamentoService, DepartamentoService>();
            services.AddTransient<IExcelService, ExcelService>();
            services.AddTransient<IFileToRarService, FileToRarService>();
            services.AddTransient<INominaService, NominaService>();
            services.AddTransient<INifService,NifService>();
            services.AddTransient<IMonedaService,MonedaService>();
            services.AddTransient<IPdfService, PdfService>();
            services.AddTransient<IPeriodosService, PeriodosService>();
            services.AddTransient<IPuestoService, PuestoService>();
            services.AddTransient<IPrestamoService, PrestamoService>();
            services.AddTransient<IReembolsoService, ReembolsoService>();
            services.AddTransient<IRegistroAsistenciaServices, RegistroAsistenciaServices>();
            services.AddTransient<IRetiroAhorroVoluntarioService, RetiroAhorroVoluntarioService>();
            services.AddTransient<IRsa, Rsa>();
            services.AddTransient<ISendMailService, SendMailService>();
            services.AddTransient<ITimboxService, TimboxService>();
            services.AddTransient<ITotalesMovsService, TotalesMovsService>();
            services.AddTransient<IXmlService, XmlService>();
            services.AddTransient<IAhorroWiseService, AhorroWiseService>();
            services.AddTransient<IViaticoService, ViaticoService>();

            services.AddScoped<EnvironmentService>();
        }
    }
}
