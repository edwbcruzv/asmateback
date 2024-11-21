using Application.DTOs.Administracion;
using Application.DTOs.Catalogos;
using Application.Exceptions;
using Application.Feautres.Catalogos.TipoPeriocidadPagos.Queries.GetTipoPeriocidadPagoById;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Catalogos;
using Application.Specifications.Employees;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Specifications.MiPortal.Prestamos;
using Application.Specifications.Nominas;
using Application.Wrappers;
using Domain.Entities;
using Humanizer;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class NominaService : INominaService
    {
        public IRepositoryAsync<Periodo> _repositoryPeriodo;
        public IRepositoryAsync<Nomina> _repositoryNomina;
        public IRepositoryAsync<NominaPercepcion> _repositoryPercepcion;
        public IRepositoryAsync<NominaDeduccion> _repositoryDeduccion;
        public IRepositoryAsync<NominaOtroPago> _repositoryOtroPago;
        public IRepositoryAsync<Employee> _repositoryEmployee;
        public IRepositoryAsync<Company> _repositoryCompany;
        public IRepositoryAsync<RegimenFiscal> _repositoryRegimenFiscal;
        public IRepositoryAsync<TipoMoneda> _repositoryTipoMoneda;
        public IRepositoryAsync<TipoPeriocidadPago> _repositoryTipoPeriocidadPago;
        public IRepositoryAsync<MetodoPago> _repositoryMetodoPago;
        public IRepositoryAsync<UsoCfdi> _repositoryUsoCfdi;
        public IRepositoryAsync<ImssDescuento> _repositoryImssDescuento;
        public IRepositoryAsync<Isr> _repositoryIsr;
        public IRepositoryAsync<Subsidio> _repositorySubsidio;
        public IRepositoryAsync<Uma> _repositorUma;
        public IRepositoryAsync<TipoIncapacidad> _repositoryTipoIncapacidad;
        public IRepositoryAsync<Vacacion> _repositoryVacacion;
        public IRepositoryAsync<TipoPercepcion> _repositoryTipoPercepcion;
        public IRepositoryAsync<TipoDeduccion> _repositoryTipoDeduccion;
        public IRepositoryAsync<TipoOtroPago> _repositoryTipoOtroPago;
        public IRepositoryAsync<SalarioMinimo> _repositorySalarioMinimo;
        public IRegistroAsistenciaServices _registroAsistenciaServices;
        private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
        private IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;

        private double diasSubsidiados = 0;

        public NominaService(IRepositoryAsync<Periodo> repositoryPeriodo, IRepositoryAsync<Nomina> repositoryNomina,
            IRepositoryAsync<NominaPercepcion> repositoryPercepcion, IRepositoryAsync<NominaDeduccion> repositoryDeduccion,
            IRepositoryAsync<NominaOtroPago> repositoryOtroPago, IRepositoryAsync<Employee> repositoryEmployee,
            IRepositoryAsync<Company> repositoryCompany, IRepositoryAsync<RegimenFiscal> repositoryRegimenFiscal,
            IRepositoryAsync<TipoMoneda> repositoryTipoMoneda, IRepositoryAsync<TipoPeriocidadPago> repositoryTipoPeriocidadPago,
            IRepositoryAsync<MetodoPago> repositoryMetodoPago, IRepositoryAsync<UsoCfdi> repositoryUsoCfdi,
            IRepositoryAsync<ImssDescuento> repositoryImssDescuento, IRepositoryAsync<Isr> repositoryIsr,
            IRepositoryAsync<Subsidio> repositorySubsidio, IRepositoryAsync<Uma> repositorUma,
            IRepositoryAsync<TipoIncapacidad> repositoryTipoIncapacidad, IRepositoryAsync<Vacacion> repositoryVacacion,
            IRepositoryAsync<TipoPercepcion> repositoryTipoPercepcion, IRepositoryAsync<TipoDeduccion> repositoryTipoDeduccion,
            IRegistroAsistenciaServices registroAsistenciaServices, IRepositoryAsync<TipoOtroPago> repositoryTipoOtroPago, 
            IRepositoryAsync<SalarioMinimo> repositorySalarioMinimo, IRepositoryAsync<Prestamo> repositoryPrestamo,
            IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario)
        {
            _repositoryPeriodo = repositoryPeriodo;
            _repositoryNomina = repositoryNomina;
            _repositoryPercepcion = repositoryPercepcion;
            _repositoryDeduccion = repositoryDeduccion;
            _repositoryOtroPago = repositoryOtroPago;
            _repositoryEmployee = repositoryEmployee;
            _repositoryCompany = repositoryCompany;
            _repositoryRegimenFiscal = repositoryRegimenFiscal;
            _repositoryTipoMoneda = repositoryTipoMoneda;
            _repositoryTipoPeriocidadPago = repositoryTipoPeriocidadPago;
            _repositoryMetodoPago = repositoryMetodoPago;
            _repositoryUsoCfdi = repositoryUsoCfdi;
            _repositoryImssDescuento = repositoryImssDescuento;
            _repositoryIsr = repositoryIsr;
            _repositorySubsidio = repositorySubsidio;
            _repositorUma = repositorUma;
            _repositoryTipoIncapacidad = repositoryTipoIncapacidad;
            _repositoryVacacion = repositoryVacacion;
            _repositoryTipoPercepcion = repositoryTipoPercepcion;
            _repositoryTipoDeduccion = repositoryTipoDeduccion;
            _registroAsistenciaServices = registroAsistenciaServices;
            _repositoryTipoOtroPago = repositoryTipoOtroPago;
            _repositorySalarioMinimo = repositorySalarioMinimo;
            _repositoryAsyncPrestamo = repositoryPrestamo;
            _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
        }

        public async Task<Response<bool>> generateNominaByPeriodo(int PeriodoId)
        {

            var periodo = await _repositoryPeriodo.GetByIdAsync(PeriodoId);

            if (periodo == null) throw new ApiException($"Periodo no encontrado para Id {PeriodoId}");


            var company = await _repositoryCompany.GetByIdAsync(periodo.CompanyId);

            if (company == null) throw new ApiException($"Company no encontrado para Id {periodo.CompanyId}");


            var employees = await _repositoryEmployee.ListAsync(
                new EmployeeByCompanyAndEstatusAnTipoPeriocidadSpecification(company.Id, 1, periodo.TipoPeriocidadPagoId)
            );

            if (employees.Count == 0) throw new ApiException($"Company no cuenta con empleados activos y con el tipo de periocidad indicado");

            // Verificar que las nóminas no hayan sido creadas anteriormente.
            var nominas = await _repositoryNomina.ListAsync(new NominasByCompanyIdSpecification(periodo.CompanyId, periodo.Id));

            // Crear una lista para almacenar los empleados filtrados
            var filteredEmployees = new List<Employee>();

            foreach (var employee in employees)
            {
                var employeeId = employee.Id;

                var coincidencia = nominas.Any(nomina => nomina.Estatus == 1 && nomina.EmployeeId.Equals(employeeId));

                if (coincidencia)
                {
                    // Obtener la primera nómina que cumple con la condición
                    var nominaCoincidencia = nominas.FirstOrDefault(nomina => nomina.EmployeeId == employeeId && nomina.Estatus == 1);

                    // Eliminar la nómina encontrada
                    await _repositoryNomina.DeleteAsync(nominaCoincidencia);
                    filteredEmployees.Add(employee);
                }
                else
                {
                    //Si ya tiene una nomina timbrada ya no se genera la nomina de nuevo
                    var coincidenciaT = nominas.Any(nomina => nomina.Estatus == 2 && nomina.EmployeeId.Equals(employeeId));
                    if (!coincidenciaT)
                    // Si no hay coincidencia, agregar el empleado a la lista de empleados filtrados
                    filteredEmployees.Add(employee);
                }
            }
            // Filtrar empleados para los cuales las nóminas ya han sido timbradas o canceladas.
            /* var filteredEmployees = employees.Where(async employee =>
            {
                // Obtener el Id del empleado actual
                var employeeId = employee.Id;

                // Verificar si hay alguna Nomina con el mismo EmployeeId
                var coincidencia = nominas.Any(nomina => nomina.Estatus == 1 && nomina.EmployeeId.Equals(employeeId));
                if (coincidencia)
                {
                    Nomina nominaCoincidencia = nominas.FirstOrDefault(nomina => nomina.EmployeeId == employeeId && nomina.Estatus == 1);
                    await _repositoryNomina.DeleteAsync(nominaCoincidencia);
                }

                // Devolver true solo si no hay coincidencia
                return !coincidencia;
            }).ToList(); */

            foreach (var employee in filteredEmployees)
            {

                if (employee.SalarioMensual > 0)
                {
                    /*
                     * Variables globales
                     */



                    /*
                     * Inicializamos cabeceras de la nómina 
                     */

                    Nomina nomina = new Nomina();

                    nomina.CompanyId = company.Id;
                    nomina.EmisorRazonSocial = company.RazonSocial;
                    nomina.EmisorRegimenFistalId = company.RegimenFiscalId;
                    nomina.LogoSrcCompany = company.CompanyProfile;
                    nomina.LugarExpedicion = company.PostalCode;
                    nomina.RegistroPatronal = employee.RegistroPatronal;
                    nomina.EmployeeId = employee.Id;
                    nomina.ReceptorRazonSocial = employee.NombreCompletoOrdenado();
                    nomina.ReceptorRfc = employee.Rfc;
                    if (employee.RegimenFiscalId == null)
                        throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Regimen fiscal");
                    nomina.ReceptorRegimenFiscalId = employee.RegimenFiscalId;
                    var uc = await _repositoryUsoCfdi.FirstOrDefaultAsync(new UsoCfdiByClaveSpecification("CN01"));
                    nomina.ReceptorUsoCfdiId = uc.Id;
                    nomina.ReceptorDomicilioFiscal = employee.CodigoPostal;
                    var tm = await _repositoryTipoMoneda.FirstOrDefaultAsync(new TipoMonedaByClaveSpecification("MXN"));
                    nomina.TipoMonedaId = tm.Id;
                    var mp = await _repositoryMetodoPago.FirstOrDefaultAsync(new MetodoPagoByClaveSpecification("PUE"));
                    nomina.MetodoPagoId = mp.Id;
                    nomina.TipoPeriocidadPagoId = (int)employee.TipoPeriocidadPagoId;
                    if (employee.PuestoId == null)
                        throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Puesto");
                    nomina.PuestoId = employee.PuestoId;
                    if (employee.TipoContratoId == null)
                        throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Contrato");
                    nomina.TipoContratoId = employee.TipoContratoId;
                    if (employee.TipoJornadaId == null)
                        throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Jornada");
                    nomina.TipoJornadaId = employee.TipoJornadaId;
                    if (employee.TipoRegimenId == null)
                        throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Regimen");
                    nomina.TipoRegimenId = employee.TipoRegimenId;
                    if (employee.TipoRiesgoTrabajoId == null)
                        throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Riesgo trabajo");
                    nomina.TipoRiesgoTrabajoId = employee.TipoRiesgoTrabajoId;
                    nomina.PeriodoId = periodo.Id;
                    nomina.Desde = periodo.Desde;
                    nomina.Hasta = periodo.Hasta;
                    nomina.Estatus = 1;
                    nomina.TipoNomina = 1;
                    if (employee.RegistroPatronal == null)
                        throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Registro patronal");
                    nomina.RegistroPatronal = employee.RegistroPatronal;

                    /*
                     * Obtenemos las asistencias del periodo 
                     */

                    var registroAsistenias = await _registroAsistenciaServices.getAsistenciaByPeriodo(PeriodoId, employee.Id);

                    var faltas = 0;
                    foreach (var temp in registroAsistenias.Where(a => a.SePagaNomina == false && a.SePagaIncapacidad == false))
                    {
                        faltas += temp.Cantidad;
                    }

                    var incapacidad = 0;
                    foreach (var temp in registroAsistenias.Where(a => a.SePagaIncapacidad == true))
                    {
                        incapacidad += temp.Cantidad;
                    }

                    var tpp = await _repositoryTipoPeriocidadPago.GetByIdAsync(periodo.TipoPeriocidadPagoId);
                    var diasEnPeriodo = tpp.Dias;

                    double asistencias = (int)diasEnPeriodo - faltas;

                    nomina.DiasPago = (int)asistencias;

                    /*
                     * Ajuste de días trabajados si hay faltas
                     */

                    if (faltas > 0) asistencias -= faltas * 0.14;

                    /*
                     * Realizamos cálculo 
                     */




                    switch (employee.TipoNomina)
                    {
                        case 1: //Tradicional

                            var salarioDiario = employee.SalarioMensual / 30;
                            var salarioMensual = employee.SalarioMensual;
                            nomina.SalarioDiario = salarioDiario;

                            var cuotaPatronalMonto = 0.0;
                            if (employee.SBC == null) cuotaPatronalMonto = await CalcularCOP(0.0, (double)salarioDiario, (double)asistencias);
                            else cuotaPatronalMonto = await CalcularCOP((double)employee.SBC, (double)salarioDiario, (double)asistencias);

                            if (employee.TipoIncapacidadId != null)
                            {
                                //throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de incapacidad");

                                var ti = await _repositoryTipoIncapacidad.GetByIdAsync(employee.TipoIncapacidadId);

                                var incapacidadMonto = CalcularIncapacidad(ti.Clave, incapacidad, asistencias, (double)salarioDiario, (double)salarioDiario);
                            }

                            if (employee.FechaContrato == null)
                                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Fecha de contrato");

                            if (employee.PorcentajePrimaVacacional == null)
                                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Porcentaje prima vacacional");

                            int aniosTrabajados = anioEntreDosFechas((DateTime)employee.FechaContrato, nomina.Hasta);
                            var vacaciones = await _repositoryVacacion.FirstOrDefaultAsync(new VacacionByAnioSpecification(aniosTrabajados,employee.CompanyId));
                            if (vacaciones == null) throw new ApiException($"La compañía con ID {employee.CompanyId} no cuenta con dias de vacaciones configurados");
                     
                            int diasVacaciones = vacaciones.Dias;
                            var vacacionMonto = await CalcularPrimaVacacional((DateTime)employee.FechaContrato, nomina.Desde, nomina.Hasta,
                                diasVacaciones, (double)salarioDiario, (double)employee.PorcentajePrimaVacacional);

                            var vacacionExcento = 0.0;
                            var vacacionGravado = 0.0;

                            var u = await _repositorUma.FirstOrDefaultAsync(new UmaByAnioSpecification(DateTime.Now.Year));
                            if (vacacionMonto > 0)
                            {
                                var excento = 15 * u.Monto;
                                if (vacacionMonto > excento)
                                {
                                    vacacionExcento = excento;
                                    vacacionGravado = vacacionMonto - excento;
                                }
                            }

                            double baseIsr = (double)vacacionGravado + (double)salarioMensual;

                            /*
                             * Calcular ISR - Subsidio al empledo
                             * 
                             * Si resultado < 0 -> Subsidio
                             * Si resultado > 0 -> Isr
                             * Si resultado = 0 -> Nada
                             * 
                             */

                            var isrSubsidio = await CalcularIsr(baseIsr, DateTime.Now.Year, 2) - await CalcularSubsidio(baseIsr, DateTime.Now.Year, 2);

                            var subsidio = 0.0;
                            var isr = 0.0;
                            if (isrSubsidio > 0) isr = isrSubsidio / 30 * asistencias;
                            else if (isrSubsidio < 0) subsidio = (isrSubsidio * -1) / 30 * asistencias;


                            /*
                             * Descuentos de préstamo
                             */
                            var descuentoprstamo = 0.0;
                            var list = await _repositoryAsyncPrestamo.ListAsync(new PrestamoByEmployeeIdAndIsActivoSpecification(employee.Id));

                            if (list.Count != 0)
                            {
                                descuentoprstamo = list[0].Descuento;
                            }

                            /*
                             * Descuentos por fondo de ahorro 
                             */

                            var fondoAhorroEmpleado = 0.0;
                            var fondoAhorroEmpresa = 0.0;

                            if (employee.CuentaIndividual == null)
                                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Cuenta individual");

                            if (employee.CuentaIndividual == 2)
                            {
                                fondoAhorroEmpleado = (double)employee.FondoAhorroEmpleado;
                                fondoAhorroEmpresa = (double)employee.FondoAhorroEmpresa;
                            }

                            /*
                             * Descuentos de ahorro
                             */
                            var descuentoAhorro = 0.0;
                            var ahorroActivo = await _repositoryAsyncAhorroVoluntario.ListAsync(new AhorroVoluntarioByEmployeeIdAndIsActivoSpecification(employee.Id));

                            if (ahorroActivo.Count != 0)
                            {
                                descuentoAhorro = ahorroActivo[0].Descuento;
                            }

                            var otrosmonto = descuentoprstamo + fondoAhorroEmpleado;

                            /*
                             * Crear percepciones
                             */





                            /* Se crea la nomina para obtener el id */
                            var nominaCargada = await _repositoryNomina.AddAsync(nomina);

                            /*
                             * Percepcion Sueldo
                             */
                            if (((double)salarioDiario * (double)asistencias) > 0)
                            {
                                var percepcion = new NominaPercepcion();

                                var tp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("001"));
                                percepcion.NominaId = nominaCargada.Id;
                                percepcion.Tipo = tp.Clave;
                                percepcion.Clave = tp.Clave;
                                percepcion.Concepto = tp.Descripcion;
                                percepcion.ImporteGravado = (double)salarioDiario * (double)asistencias;
                                percepcion.ImporteExento = 0;

                                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                            }

                            /*
                             * Percepcion Vacaciones si es que hay
                             */
                            if (vacacionMonto > 0)
                            {
                                var percepcion = new NominaPercepcion();

                                var tp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("021"));
                                percepcion.NominaId = nominaCargada.Id;
                                percepcion.Tipo = tp.Clave;
                                percepcion.Clave = tp.Clave;
                                percepcion.Concepto = tp.Descripcion;
                                percepcion.ImporteGravado = vacacionGravado;
                                percepcion.ImporteExento = vacacionExcento;

                                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                            }

                            /*
                             * Percepcion fondo de ahorro si es que hay
                             */
                            if (fondoAhorroEmpleado > 0)
                            {
                                var percepcion = new NominaPercepcion();

                                var tp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("005"));
                                percepcion.NominaId = nominaCargada.Id;
                                percepcion.Tipo = tp.Clave;
                                percepcion.Clave = tp.Clave;
                                percepcion.Concepto = tp.Descripcion;
                                percepcion.ImporteGravado = 0;
                                percepcion.ImporteExento = fondoAhorroEmpleado;

                                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                            }



                            /*
                             * Crear deducciones
                             */

                            if (isr > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("002"));
                                deduccion.NominaId = nominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = isr;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if (otrosmonto > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("004"));
                                deduccion.NominaId = nominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = otrosmonto;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if ((cuotaPatronalMonto) > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("021"));
                                deduccion.NominaId = nominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = cuotaPatronalMonto;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if (descuentoAhorro > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("023"));
                                deduccion.NominaId = nominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = descuentoAhorro;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if (fondoAhorroEmpresa > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("029"));
                                deduccion.NominaId = nominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = fondoAhorroEmpresa;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }


                            if (employee.DescuentoCreditoHipo > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("010"));
                                deduccion.NominaId = nominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = (double)(employee.DescuentoCreditoHipo / 2);

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if (employee.AjusteIsr > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("101"));
                                deduccion.NominaId = nominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = (double)employee.AjusteIsr;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if (subsidio > 0)
                            {
                                var otroPago = new NominaOtroPago();

                                var tp = await _repositoryTipoOtroPago.FirstOrDefaultAsync(new TipoOtroPagoByClaveSpecification("002"));
                                otroPago.NominaId = nominaCargada.Id;
                                otroPago.Tipo = tp.Clave;
                                otroPago.Clave = tp.Clave;
                                otroPago.Concepto = tp.Descripcion;
                                otroPago.Importe = subsidio;

                                var deduccionCargada = await _repositoryOtroPago.AddAsync(otroPago);
                            }

                            break;

                        case 2: //Mixto (wise)

                            // 2. Porcentaje
                            if (employee.TipoPrevicionSocial == 2 && employee.Porcentaje == null)
                                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Porcentaje");


                            double salarioDiarioParcial = 0.0;
                            double limiteSalarial = 66196.40; // Límite de sueldo con MPS
                            double LimiteMPS = 18813.32; // Límite para la previcion social
                            double factorminimo = 1.0452;
                            salarioDiario = employee.SalarioMensual / 30;

                            //anios que ha trabajado el empleado
                            int aniosTrab = anioEntreDosFechas((DateTime)employee.FechaContrato, nomina.Hasta);
                            //dias de vacaciones que le corresponden por los anios trabajados
                            var vacacion = await _repositoryVacacion.FirstOrDefaultAsync(new VacacionByAnioSpecification(aniosTrab, employee.CompanyId));
                            //salario minimo del anio en curso
                            var smg = await _repositorySalarioMinimo.FirstOrDefaultAsync(new SalarioMinimoByAnioSpecification(DateTime.Now.Year));
                            //uma del anio actual
                            var uma = await _repositorUma.FirstOrDefaultAsync(new UmaByAnioSpecification(DateTime.Now.Year));

                            int diasVacacion = vacacion.Dias;

                            //Salarios minimos
                            if (employee.TipoPrevicionSocial == 1)
                            {
                                if (employee.SalarioMensual <= limiteSalarial)
                                {
                                    //var smg = await _repositorySalarioMinimo.FirstOrDefaultAsync(new SalarioMinimoByAnioSpecification(DateTime.Now.Year));
                                    var flag = true;
                                    while (flag)
                                    {
                                        if (salarioDiarioParcial < employee.SalarioMensual / 30 / 2)
                                        {
                                            salarioDiarioParcial += smg.Monto;
                                        }
                                        else
                                        {
                                            flag = false;
                                        }
                                    }
                                    //salario diario no puede exceder 25 umas, si las pasa le asignan como tope
                                    if(salarioDiarioParcial > (uma.Monto * 25)) salarioDiarioParcial = uma.Monto * 25;
                                }
                                else
                                {
                                    //throw new ApiException($"Empleado con Id {employee.Id} excede límite salarial por sueldos mínimos");
                                     salarioDiarioParcial = uma.Monto * 25;
                                }
                            }

                            //Por pocentaje
                            if (employee.TipoPrevicionSocial == 2)
                            {
                                //salarioDiarioParcial = (double)(employee.SalarioMensual / 30) * (double)(employee.Porcentaje / 100);

                                var primavacacionalporcentaje = (employee.PorcentajePrimaVacacional/100);
                                var porcentaje = (employee.Porcentaje/100);
                                

                                //Caso especial, aunque no tenga anios trabajados se consideran 12 dias de vacaciones
                                if (aniosTrab == 0)
                                {
                                    var salarioimss = (((((12 * (double)primavacacionalporcentaje) + 30) / 365) + 1) * salarioDiario) * porcentaje;
                                    if (salarioimss > smg.Monto)
                                    {
                                        if(salarioimss > (smg.Monto * 25))
                                        {
                                            salarioDiarioParcial = smg.Monto * 25;
                                        } else
                                        {
                                            salarioDiarioParcial = (double)salarioimss;
                                        }
                                    }else
                                        salarioDiarioParcial = ((((12 * (double)primavacacionalporcentaje) + 30) / 365) + 1) * smg.Monto;
                                } else
                                {
                                    var salarioimss = (((((diasVacacion * (double)primavacacionalporcentaje) + 30) / 365) + 1) * salarioDiario) * porcentaje;
                                    if (salarioimss > smg.Monto)
                                    {
                                        if (salarioimss > (smg.Monto * 25))
                                        {
                                            salarioDiarioParcial = smg.Monto * 25;
                                        }
                                        else
                                        {
                                            salarioDiarioParcial = (double)salarioimss;
                                        }
                                    } else
                                        salarioDiarioParcial = ((((diasVacacion * (double)primavacacionalporcentaje) + 30) / 365) + 1) * smg.Monto;
                                }

                            }

                            var sueldo = (double)salarioDiarioParcial * (double)asistencias;


                            //ajuste para salarios bajos
                            if (sueldo > (employee.SalarioMensual / 2))
                            {
                                sueldo = (double)(employee.SalarioMensual / 2);
                                salarioDiarioParcial = ((double)(employee.SalarioMensual / 30));
                            }

                            cuotaPatronalMonto = 0.0;
                            if (employee.SBC == null) cuotaPatronalMonto = await CalcularCOP(0.0, (double)salarioDiarioParcial, (double)asistencias);
                            else cuotaPatronalMonto = await CalcularCOP((double)employee.SBC, (double)salarioDiario, (double)asistencias);


                            var sueldoSBC = 0.0;
                            if (salarioDiarioParcial > employee.SalarioDiario)
                            {
                                sueldoSBC = (double)employee.SalarioDiario;
                                if (sueldoSBC < smg.Monto) sueldoSBC = smg.Monto;
                            } else
                            {
                                sueldoSBC = salarioDiarioParcial;
                                if (sueldoSBC < smg.Monto) sueldoSBC = smg.Monto;
                            }

                            var exedente = employee.SalarioDiario - salarioDiarioParcial;
                            var cnsbcdos = sueldoSBC * factorminimo;

                            //if (employee.SalarioImss > 0) cnsbcdos = employee.SalarioImss;

                            var cnsalariodiario = employee.SalarioDiario;
                            var cnsalariomensual = cnsalariodiario * 30;

                            //Calculamos el isr normal
                            var vacacionMont = await CalcularPrimaVacacional((DateTime)employee.FechaContrato, nomina.Desde, nomina.Hasta,
                                diasVacacion, (double)salarioDiarioParcial, (double)employee.PorcentajePrimaVacacional);

                            var vacacionesExcento = 0.0;
                            var vacacionesGravado = 0.0;

                            if (vacacionMont > 0)
                            {
                                var excento = 15 * uma.Monto;
                                if (vacacionMont > excento)
                                {
                                    vacacionesExcento = excento;
                                    vacacionesGravado = vacacionMont - excento;
                                }
                                else
                                {
                                    vacacionesExcento = vacacionMont;
                                    vacacionesGravado = 0.0;
                                }
                            }

                            //base para calculo isr tradicional
                            double BaseIsr = (double)vacacionesGravado + (double)employee.SalarioMensual;

                            //base para el calculo isr wise
                            double BaseIsrWise = (double)vacacionesGravado + ((double)salarioDiarioParcial * 30);

                            var IsrSubsidio = await CalcularIsr(BaseIsr, DateTime.Now.Year, 2) - await CalcularSubsidio(BaseIsr, DateTime.Now.Year, 2);
                            var IsrSubsidioWise = await CalcularIsr(BaseIsrWise, DateTime.Now.Year, 2) - await CalcularSubsidio(BaseIsr, DateTime.Now.Year, 2);

                            //ISR y subsidio de nomina tradicional
                            var SubsidioTradicional = 0.0;
                            var IsrTradicional = 0.0;
                            if (IsrSubsidio > 0) IsrTradicional = IsrSubsidio / 30 * asistencias;
                            else if (IsrSubsidio < 0) SubsidioTradicional = (IsrSubsidio * -1) / 30 * asistencias;

                            //ISR y subsidio de nomina wise
                            var SubsidioWise = 0.0;
                            var IsrWise = 0.0;
                            if (IsrSubsidioWise > 0) IsrWise = IsrSubsidioWise / 30 * asistencias;
                            else if (IsrSubsidioWise < 0) SubsidioWise = (IsrSubsidioWise * -1) / 30 * asistencias;

                            //seguro retiro wise (isr tradicional - isr wise)
                            var seguroretiro = (double)IsrTradicional - (double)IsrWise;

                            //monto minimo de seguro de retiro
                            if (seguroretiro < 25)
                            {
                                seguroretiro = 25;
                            }

                            //ayuda de transporte
                            var ayudatransporte = ((double)employee.SalarioMensual / 2) - ((double)seguroretiro + ((double)salarioDiarioParcial * (double)asistencias));


                            nomina.SalarioDiario = salarioDiarioParcial;


                            //solo se puden excentar 22k al mes, 11k a la quincena
                            if (ayudatransporte > 11000)
                            {
                                //ajustamos el salario
                                sueldo = ((double)employee.SalarioMensual / 2) - 11000;
                                IsrSubsidioWise = await CalcularIsr(sueldo, DateTime.Now.Year, 2) - await CalcularSubsidio(sueldo, DateTime.Now.Year, 2);
                                seguroretiro = (double)IsrTradicional - (Double)IsrSubsidioWise;
                                ayudatransporte = 11000 - seguroretiro;
                                nomina.SalarioDiario = sueldo / 15;
                            }

                            /*
                             * Descuentos de préstamo
                             */
                            var descuentoprestamo = 0.0;
                            var prestamoActivo = await _repositoryAsyncPrestamo.ListAsync(new PrestamoByEmployeeIdAndIsActivoSpecification(employee.Id));

                            if (prestamoActivo.Count != 0)
                            {
                                descuentoprestamo = prestamoActivo[0].Descuento;
                            }

                            /*
                             * Descuentos de ahorro
                             */
                            var descuentoahorro = 0.0;
                            var ahorroactivo = await _repositoryAsyncAhorroVoluntario.ListAsync(new AhorroVoluntarioByEmployeeIdAndIsActivoSpecification(employee.Id));

                            if (ahorroactivo.Count != 0)
                            {
                                descuentoahorro = ahorroactivo[0].Descuento;
                            }

                            /*
                             * Crear percepciones
                             */


                            /* Se crea la nomina para obtener el id */
                            var NominaCargada = await _repositoryNomina.AddAsync(nomina);

                            /*
                             * Percepcion Sueldo wise
                             */
                            if (((double)salarioDiarioParcial * (double)asistencias) > 0)
                            {
                                var percepcion = new NominaPercepcion();

                                var tp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("001"));
                                percepcion.NominaId = NominaCargada.Id;
                                percepcion.Tipo = tp.Clave;
                                percepcion.Clave = tp.Clave;
                                percepcion.Concepto = tp.Descripcion;
                                percepcion.ImporteGravado = sueldo;
                                percepcion.ImporteExento = 0;

                                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                            }

                            /*
                             * Percepcion prima vacacional
                             */
                            if (vacacionMont > 0)
                            {
                                var percepcion = new NominaPercepcion();

                                var tp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("021"));
                                percepcion.NominaId = NominaCargada.Id;
                                percepcion.Tipo = tp.Clave;
                                percepcion.Clave = tp.Clave;
                                percepcion.Concepto = tp.Descripcion;
                                percepcion.ImporteGravado = vacacionesGravado;
                                percepcion.ImporteExento = vacacionesExcento;

                                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                            }

                            /*
                            * Percepcion seguro de retiro
                            */
                            if (((double)salarioDiario * (double)asistencias) > 0)
                            {
                                var percepcion = new NominaPercepcion();

                                var tp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("024"));
                                percepcion.NominaId = NominaCargada.Id;
                                percepcion.Tipo = tp.Clave;
                                percepcion.Clave = tp.Clave;
                                percepcion.Concepto = tp.Descripcion;
                                percepcion.ImporteGravado = 0;
                                percepcion.ImporteExento = (double)seguroretiro;

                                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                            }

                            /*
                             * Percepcion ayuda de transporte 
                             */
                            if (ayudatransporte > 0)
                            {
                                var percepcion = new NominaPercepcion();

                                var tp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("036"));
                                percepcion.NominaId = NominaCargada.Id;
                                percepcion.Tipo = tp.Clave;
                                percepcion.Clave = tp.Clave;
                                percepcion.Concepto = tp.Descripcion;
                                percepcion.ImporteGravado = 0;
                                percepcion.ImporteExento = ayudatransporte;

                                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                            }



                            /*
                             * Crear deducciones
                             */


                            /*
                             * Isr wise
                             */
                            if (IsrWise > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("002"));
                                deduccion.NominaId = NominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = IsrWise;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            /*
                             * Descuento del prestamo activo
                             */
                            if (descuentoprestamo > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("004"));
                                deduccion.NominaId = NominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = descuentoprestamo;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }



                            /*
                             * Cuota obrero patronal
                             */
                            if ((cuotaPatronalMonto) > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("021"));
                                deduccion.NominaId = NominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = cuotaPatronalMonto;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if (descuentoahorro > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("023"));
                                deduccion.NominaId = NominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = descuentoahorro;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            /*
                             * Ajuste seguro de retiro
                             */
                            if ((double)seguroretiro > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("048"));
                                deduccion.NominaId = NominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = (double)seguroretiro;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }

                            if (employee.AjusteIsr > 0)
                            {
                                var deduccion = new NominaDeduccion();

                                var tp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("101"));
                                deduccion.NominaId = NominaCargada.Id;
                                deduccion.Tipo = tp.Clave;
                                deduccion.Clave = tp.Clave;
                                deduccion.Concepto = tp.Descripcion;
                                deduccion.Importe = (double)employee.AjusteIsr;

                                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                            }


                            /*
                             * Cuota obrero patronal
                             */

                            if (SubsidioWise > 0)
                            {
                                var otroPago = new NominaOtroPago();

                                var tp = await _repositoryTipoOtroPago.FirstOrDefaultAsync(new TipoOtroPagoByClaveSpecification("002"));
                                otroPago.NominaId = NominaCargada.Id;
                                otroPago.Tipo = tp.Clave;
                                otroPago.Clave = tp.Clave;
                                otroPago.Concepto = tp.Descripcion;
                                otroPago.Importe = SubsidioWise;

                                var deduccionCargada = await _repositoryOtroPago.AddAsync(otroPago);
                            }

                            break;
                        case 3: //GastoOperativo
                            break;
                    }

                    //var percepciones = 
                    //var deducciones =
                    //var otrosPagos =
                }




            }

            return new Response<bool>(true);
        }

        public async Task<Response<bool>> generateAguinaldoByPeriodo(int PeriodoId)
        {
            var periodo = await _repositoryPeriodo.GetByIdAsync(PeriodoId);

            if (periodo == null) throw new ApiException($"Periodo no encontrado para Id {PeriodoId}");

            var company = await _repositoryCompany.GetByIdAsync(periodo.CompanyId);

            if (company == null) throw new ApiException($"La compañía con Id {periodo.CompanyId} no tiene registrado un periodo extraordinario con el Id {periodo.CompanyId}");

            //var employees = await _repositoryEmployee.ListAsync(
            //    new EmployeeByCompanySpecification(company.Id)
            //);

            
            var employees = await _repositoryEmployee.ListAsync(
                new EmployeeByCompanyAndEstatusAnTipoPeriocidadSpecification(company.Id, 1, periodo.TipoPeriocidadPagoId)
            );

            if (employees.Count == 0) throw new ApiException($"Company no cuenta con empleados");

            // Verificar que las nóminas no hayan sido creadas anteriormente.
            var nominas = await _repositoryNomina.ListAsync(new NominasByCompanyIdSpecification(periodo.CompanyId, periodo.Id));

            // Crear una lista para almacenar los empleados filtrados
            var filteredEmployees = new List<Employee>();

            foreach (var employee in employees)
            {
                var employeeId = employee.Id;

                var coincidencia = nominas.Any(nomina => nomina.Estatus == 1 && nomina.EmployeeId.Equals(employeeId));

                if (coincidencia)
                {
                    // Obtener la primera nómina que cumple con la condición
                    var nominaCoincidencia = nominas.FirstOrDefault(nomina => nomina.EmployeeId == employeeId && nomina.Estatus == 1);

                    // Eliminar la nómina encontrada
                    await _repositoryNomina.DeleteAsync(nominaCoincidencia);
                    filteredEmployees.Add(employee);
                }
                else
                {
                    // Si no hay coincidencia, agregar el empleado a la lista de empleados filtrados
                    filteredEmployees.Add(employee);
                }
            }

            foreach (var employee in filteredEmployees)
            {
                Nomina nomina = new Nomina();

                nomina.CompanyId = company.Id;
                nomina.EmisorRazonSocial = company.RazonSocial;
                nomina.EmisorRegimenFistalId = company.RegimenFiscalId;
                nomina.LogoSrcCompany = company.CompanyProfile;
                nomina.LugarExpedicion = company.PostalCode;
                nomina.RegistroPatronal = employee.RegistroPatronal;
                nomina.EmployeeId = employee.Id;
                nomina.ReceptorRazonSocial = employee.NombreCompleto();
                nomina.ReceptorRfc = employee.Rfc;
                if (employee.RegimenFiscalId == null)
                    throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Regimen fiscal");
                nomina.ReceptorRegimenFiscalId = employee.RegimenFiscalId;
                var uc = await _repositoryUsoCfdi.FirstOrDefaultAsync(new UsoCfdiByClaveSpecification("CN01"));
                nomina.ReceptorUsoCfdiId = uc.Id;
                nomina.ReceptorDomicilioFiscal = employee.CodigoPostal;
                var tm = await _repositoryTipoMoneda.FirstOrDefaultAsync(new TipoMonedaByClaveSpecification("MXN"));
                nomina.TipoMonedaId = tm.Id;
                var mp = await _repositoryMetodoPago.FirstOrDefaultAsync(new MetodoPagoByClaveSpecification("PUE"));
                nomina.MetodoPagoId = mp.Id;
                //Verificar contra DB master
                var tp = await _repositoryTipoPeriocidadPago.FirstOrDefaultAsync(new TipoPeriocidadByIdOrDescriptionSpecification("Anual"));
                nomina.TipoPeriocidadPagoId = tp.Id;
                if (employee.PuestoId == null)
                    throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Puesto");
                nomina.PuestoId = employee.PuestoId;
                if (employee.TipoContratoId == null)
                    throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Contrato");
                nomina.TipoContratoId = employee.TipoContratoId;
                if (employee.TipoJornadaId == null)
                    throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Jornada");
                nomina.TipoJornadaId = employee.TipoJornadaId;
                if (employee.TipoRegimenId == null)
                    throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Regimen");
                nomina.TipoRegimenId = employee.TipoRegimenId;
                if (employee.TipoRiesgoTrabajoId == null)
                    throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Riesgo trabajo");
                nomina.TipoRiesgoTrabajoId = employee.TipoRiesgoTrabajoId;
                nomina.PeriodoId = periodo.Id;
                nomina.Desde = periodo.Desde;
                nomina.Hasta = periodo.Hasta;
                nomina.Estatus = 1;
                nomina.TipoNomina = 1;
                nomina.SalarioDiario = employee.SalarioDiario;
                if (employee.RegistroPatronal == null)
                    throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Registro patronal");
                nomina.RegistroPatronal = employee.RegistroPatronal;

                List<AsistenciaResumenDto> registroAsistencias;

                DateTime inicio = periodo.Desde;
                DateTime fin = periodo.Hasta;


                // Contrato sigue vigente
                if (employee.FinContrato == null)
                {
                    // Año completo trabajado
                    if (employee.FechaContrato <= periodo.Desde)
                    {
                        inicio = periodo.Desde;
                        fin = periodo.Hasta;
                    }
                    // Empezó a trabajar después de que empezó el año
                    if (employee.FechaContrato > periodo.Desde)
                    {
                        inicio = (DateTime)employee.FechaContrato;
                        fin = periodo.Hasta;
                    }
                }
                // Fin de contrato establecido
                else
                {
                    // Empezó a trabajar antes de o a inicio de año
                    if (employee.FechaContrato <= periodo.Desde)
                    {
                        inicio = periodo.Desde;
                        fin = (DateTime)employee.FinContrato;
                    }
                    // Empezó a trabajar después de inicio de año y no lo trabajó completo
                    if (employee.FechaContrato > periodo.Desde && employee.FinContrato < periodo.Hasta)
                    {
                        inicio = (DateTime)employee.FechaContrato;
                        fin = (DateTime)employee.FinContrato;
                    }
                }

                registroAsistencias = await _registroAsistenciaServices.getAsistenciaByRango(inicio, fin, employee.Id);
                int asistencias = 0;

                var faltas = 0;
                foreach (var temp in registroAsistencias.Where(a => a.SePagaAguinaldo == false))
                {
                    faltas += temp.Cantidad;
                }
                var diasPagoAguinaldo = 0;
                foreach (var temp in registroAsistencias.Where(a => a.SePagaAguinaldo == true))
                {
                    diasPagoAguinaldo += temp.Cantidad;
                }

                int diasRestantes = 365 - diasPagoAguinaldo;
                asistencias = diasPagoAguinaldo + diasRestantes - faltas;

                nomina.DiasPago = asistencias;


                double salarioDiario = (double)employee.SalarioDiario;
                int diasAguinaldo = company.SalaryDays;
                double aguinaldo = 0.0;


                aguinaldo = CalculoAguinaldo(salarioDiario, diasAguinaldo, asistencias);

                var uma = await _repositorUma.GetByIdAsync(1);

                double aux = uma.Monto * 30;

                var nominaMasReciente = await _repositoryNomina.FirstOrDefaultAsync(new MostRecentNominaByEmployeeIDSpecification(employee.Id,(int)employee.TipoPeriocidadPagoId));
                if (nominaMasReciente == null) throw new ApiException($"Empleado con Id {employee.Id} no cuenta con nomina registrada");

                var nominaPercepcionMasReciente = await _repositoryPercepcion.FirstOrDefaultAsync(new MostRecentSalaryByNominaIDSpecification(nominaMasReciente.Id));
                if (nominaPercepcionMasReciente == null) throw new ApiException($"Empleado con Id {employee.Id} no cuenta con nomina de percepción registrada");

                var tipoPeriocidad = await _repositoryTipoPeriocidadPago.FirstOrDefaultAsync(new TipoPeriocidadByIdOrDescriptionSpecification((int)nominaMasReciente.TipoPeriocidadPagoId));
                if (tipoPeriocidad == null) throw new ApiException($"La nomina con Id {nomina.Id} no cuenta con tipo de periocidad registrada");

                double ingresoOrdinarioGrabable = (double)(30 * nominaPercepcionMasReciente.ImporteGravado / tipoPeriocidad.Dias);

                double aguinaldoGrabable = (aguinaldo - aux);

                double ingresosTotales = aguinaldoGrabable + ingresoOrdinarioGrabable;

                double isrPorIngresosTotales = await CalcularIsr(ingresosTotales, (periodo.Etapa / 100), 2);

                double isrPorIngresosOrdinarios = await CalcularIsr(ingresoOrdinarioGrabable, (periodo.Etapa / 100), 2);

                double isrAguinaldo = isrPorIngresosTotales - isrPorIngresosOrdinarios;


                var nominaCargada = await _repositoryNomina.AddAsync(nomina);

                // Se graba la percepción del aguinaldo
                if (aguinaldo > 0)
                {
                    var percepcion = new NominaPercepcion();
                    var temp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("002"));
                    percepcion.NominaId = nominaCargada.Id;
                    percepcion.Tipo = temp.Clave;
                    percepcion.Clave = temp.Clave;
                    percepcion.Concepto = temp.Descripcion;
                    if (aguinaldo > aux)
                    {
                        percepcion.ImporteGravado = aguinaldoGrabable;
                        percepcion.ImporteExento = aux;
                    }
                    else
                    {
                        percepcion.ImporteGravado = aguinaldo;
                        percepcion.ImporteExento = 0.0;
                    }
                    var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
                }

                // Se graba la deducción del isr si es que existe
                if (isrAguinaldo > 0)
                {
                    var deduccion = new NominaDeduccion();
                    var temp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("002"));
                    deduccion.NominaId = nominaCargada.Id;
                    deduccion.Tipo = temp.Clave;
                    deduccion.Clave = temp.Clave;
                    deduccion.Concepto = temp.Descripcion;
                    deduccion.Importe = isrAguinaldo;
                    var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
                }

            }

            return new Response<bool>(true);
        }

        public async Task<Response<double>> generateAguinaldoByEmployee(int EmployeeID, int PeriodoID)
        {
            var periodo = await _repositoryPeriodo.GetByIdAsync(PeriodoID);
            if (periodo == null) throw new ApiException($"El periodo con id {PeriodoID} no está registrado");
            var employee = await _repositoryEmployee.GetByIdAsync(EmployeeID);
            if (employee == null) throw new ApiException($"El empleado con id {EmployeeID} no está registrado");
            var company = await _repositoryCompany.GetByIdAsync(employee.CompanyId);
            if (company == null) throw new ApiException($"La compañia con id {employee.CompanyId} no está registrada");

            Nomina nomina = new Nomina();

            nomina.CompanyId = company.Id;
            nomina.EmisorRazonSocial = company.RazonSocial;
            nomina.EmisorRegimenFistalId = company.RegimenFiscalId;
            nomina.LogoSrcCompany = company.CompanyProfile;
            nomina.LugarExpedicion = company.PostalCode;
            nomina.RegistroPatronal = employee.RegistroPatronal;
            nomina.EmployeeId = employee.Id;
            nomina.ReceptorRazonSocial = employee.NombreCompleto();
            nomina.ReceptorRfc = employee.Rfc;
            if (employee.RegimenFiscalId == null)
                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Regimen fiscal");
            nomina.ReceptorRegimenFiscalId = employee.RegimenFiscalId;
            var uc = await _repositoryUsoCfdi.FirstOrDefaultAsync(new UsoCfdiByClaveSpecification("CN01"));
            nomina.ReceptorUsoCfdiId = uc.Id;
            nomina.ReceptorDomicilioFiscal = employee.CodigoPostal;
            var tm = await _repositoryTipoMoneda.FirstOrDefaultAsync(new TipoMonedaByClaveSpecification("MXN"));
            nomina.TipoMonedaId = tm.Id;
            var mp = await _repositoryMetodoPago.FirstOrDefaultAsync(new MetodoPagoByClaveSpecification("PUE"));
            nomina.MetodoPagoId = mp.Id;
            // verificar contra DB master
            var tp = await _repositoryTipoPeriocidadPago.FirstOrDefaultAsync(new TipoPeriocidadByIdOrDescriptionSpecification("Anual"));
            nomina.TipoPeriocidadPagoId = tp.Id;
            if (employee.PuestoId == null)
                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Puesto");
            nomina.PuestoId = employee.PuestoId;
            if (employee.TipoContratoId == null)
                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Contrato");
            nomina.TipoContratoId = employee.TipoContratoId;
            if (employee.TipoJornadaId == null)
                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Jornada");
            nomina.TipoJornadaId = employee.TipoJornadaId;
            if (employee.TipoRegimenId == null)
                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Regimen");
            nomina.TipoRegimenId = employee.TipoRegimenId;
            if (employee.TipoRiesgoTrabajoId == null)
                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Tipo de Riesgo trabajo");
            nomina.TipoRiesgoTrabajoId = employee.TipoRiesgoTrabajoId;
            nomina.PeriodoId = periodo.Id;
            nomina.Desde = periodo.Desde;
            nomina.Hasta = periodo.Hasta;
            nomina.Estatus = 1;
            nomina.TipoNomina = 1;
            if (employee.RegistroPatronal == null)
                throw new ApiException($"Empleado con Id {employee.Id} no cuenta con configuración para Registro patronal");
            nomina.RegistroPatronal = employee.RegistroPatronal;

            List<AsistenciaResumenDto> registroAsistencias;

            DateTime inicio = periodo.Desde;
            DateTime fin = periodo.Hasta;

            // Contrato sigue vigente
            if (employee.FinContrato == null)
            {
                // Año completo trabajado
                if (employee.FechaContrato <= periodo.Desde)
                {
                    inicio = periodo.Desde;
                    fin = periodo.Hasta;
                }
                // Empezó a trabajar después de que empezó el año
                if (employee.FechaContrato > periodo.Desde)
                {
                    inicio = (DateTime)employee.FechaContrato;
                    fin = periodo.Hasta;
                }
            }
            // Fin de contrato establecido
            else
            {
                // Empezó a trabajar antes de o a inicio de año
                if (employee.FechaContrato <= periodo.Desde)
                {
                    inicio = periodo.Desde;
                    fin = (DateTime)employee.FinContrato;
                }
                // Empezó a trabajar después de inicio de año y no lo trabajó completo
                if (employee.FechaContrato > periodo.Desde && employee.FinContrato < periodo.Hasta)
                {
                    inicio = (DateTime)employee.FechaContrato;
                    fin = (DateTime)employee.FinContrato;
                }
            }

            registroAsistencias = await _registroAsistenciaServices.getAsistenciaByRango(inicio, fin, employee.Id);
            int asistencias = 0;

            var faltas = 0;
            foreach (var temp in registroAsistencias.Where(a => a.SePagaAguinaldo == false))
            {
                faltas += temp.Cantidad;
            }
            var diasPagoAguinaldo = 0;
            foreach (var temp in registroAsistencias.Where(a => a.SePagaAguinaldo == true))
            {
                diasPagoAguinaldo += temp.Cantidad;
            }

            int diasRestantes = 365 - diasPagoAguinaldo;
            asistencias = diasPagoAguinaldo + diasRestantes - faltas;

            nomina.DiasPago = asistencias;

            double salarioDiario = (double)employee.SalarioDiario;
            int diasAguinaldo = company.SalaryDays;
            double aguinaldo = 0.0;

            aguinaldo = CalculoAguinaldo(salarioDiario, diasAguinaldo, asistencias);

            var uma = await _repositorUma.GetByIdAsync(1);

            double aux = uma.Monto * 30;


            var nominaMasReciente = await _repositoryNomina.FirstOrDefaultAsync(new MostRecentNominaByEmployeeIDSpecification(employee.Id,(int)employee.TipoPeriocidadPagoId));
            if (nominaMasReciente == null) throw new ApiException($"Empleado con Id {employee.Id} no cuenta con nomina registrada");

            var nominaPercepcionMasReciente = await _repositoryPercepcion.FirstOrDefaultAsync(new MostRecentSalaryByNominaIDSpecification(nominaMasReciente.Id));
            if (nominaPercepcionMasReciente == null) throw new ApiException($"Empleado con Id {employee.Id} no cuenta con nomina de percepción registrada");

            var tipoPeriocidad = await _repositoryTipoPeriocidadPago.FirstOrDefaultAsync(new TipoPeriocidadByIdOrDescriptionSpecification((int)nominaMasReciente.TipoPeriocidadPagoId));
            if (tipoPeriocidad == null) throw new ApiException($"La nomina con Id {nomina.Id} no cuenta con tipo de periocidad registrada");

            double ingresoOrdinarioGrabable = (double)(30 * nominaPercepcionMasReciente.ImporteGravado / tipoPeriocidad.Dias);

            double aguinaldoGrabable = (aguinaldo - aux);


            double ingresosTotales = aguinaldoGrabable + ingresoOrdinarioGrabable;

            double isrPorIngresosTotales = await CalcularIsr(ingresosTotales, (periodo.Etapa / 100), 2);

            double isrPorIngresosOrdinarios = await CalcularIsr(ingresoOrdinarioGrabable, (periodo.Etapa / 100), 2);

            double isrAguinaldo = isrPorIngresosTotales - isrPorIngresosOrdinarios;


            var nominaCargada = await _repositoryNomina.AddAsync(nomina);

            // Se graba la percepción del aguinaldo
            if (aguinaldo > 0)
            {
                var percepcion = new NominaPercepcion();
                var temp = await _repositoryTipoPercepcion.FirstOrDefaultAsync(new TipoPercepcionByClaveSpecification("002"));
                percepcion.NominaId = nominaCargada.Id;
                percepcion.Tipo = temp.Clave;
                percepcion.Clave = temp.Clave;
                percepcion.Concepto = temp.Descripcion;
                if (aguinaldo > aux)
                {
                    percepcion.ImporteGravado = aguinaldoGrabable;
                    percepcion.ImporteExento = aux;
                }
                else
                {
                    percepcion.ImporteGravado = aguinaldo;
                    percepcion.ImporteExento = 0.0;
                }
                var percepcionCargada = await _repositoryPercepcion.AddAsync(percepcion);
            }

            // Se graba la deducción del isr si es que existe
            if (isrAguinaldo > 0)
            {
                var deduccion = new NominaDeduccion();
                var temp = await _repositoryTipoDeduccion.FirstOrDefaultAsync(new TipoDeduccionByClaveSpecification("002"));
                deduccion.NominaId = nominaCargada.Id;
                deduccion.Tipo = temp.Clave;
                deduccion.Clave = temp.Clave;
                deduccion.Concepto = temp.Descripcion;
                deduccion.Importe = isrAguinaldo;
                var deduccionCargada = await _repositoryDeduccion.AddAsync(deduccion);
            }

            return new Response<double>(aguinaldo);
        }

        /*
         * Calculos generales
         */
        public async Task<double> CalcularPrimaVacacional(DateTime fechaIngreso, DateTime desdeNomina, DateTime hastaNomina,
            int diasVacaciones, double SueldoDiario, double porcentajePrimaVacacional)
        {
            int aniosTrabajados = anioEntreDosFechas(fechaIngreso, hastaNomina);
            double montoVacacion = 0.0;

            if (aniosTrabajados > 0)
            {
                var diaCumple = new DateTime(hastaNomina.Year, fechaIngreso.Month, fechaIngreso.Day);

                if (diaCumple >= desdeNomina && diaCumple <= hastaNomina)
                {
                    
                    if (porcentajePrimaVacacional == 0.0 || porcentajePrimaVacacional == null)
                    {
                        montoVacacion = SueldoDiario * diasVacaciones * .25;

                    }
                    else
                    {
                        montoVacacion = SueldoDiario * diasVacaciones * (porcentajePrimaVacacional / 100);
                    }
                }
            }

            return montoVacacion;

        }

        public double CalcularIncapacidad(string tipoIncapacidad, int diasIncapacidad,
            double diasAsistecia, double salarioDiario, double CNSalarioDiario)
        {
            if (diasIncapacidad > 0)
            {
                if (diasAsistecia > 0)
                {
                    /*Días susidiados*/
                    if (diasAsistecia <= diasSubsidiados)
                    {
                        if (tipoIncapacidad.Equals("01"))
                        {
                            return diasIncapacidad * salarioDiario;
                        }
                    }
                    else
                    {
                        if (tipoIncapacidad.Equals("01"))
                        {
                            var Incapacidad = diasSubsidiados * salarioDiario;
                            var RestoIncapa = salarioDiario - CNSalarioDiario;
                            var vecesIncapa = diasIncapacidad - diasSubsidiados;

                            var cantIncapa = RestoIncapa * vecesIncapa;

                            return Incapacidad + cantIncapa;
                        }
                        else
                        {
                            var RestoIncapa = salarioDiario - CNSalarioDiario;
                            var vecesIncapa = diasIncapacidad - diasSubsidiados;
                            return RestoIncapa * vecesIncapa;
                        }
                    }
                }
                else
                {
                    return (salarioDiario - CNSalarioDiario) * diasIncapacidad;
                }
            }


            return 0.0;



        }

        public async Task<double> CalcularCOP(double sbcEmpleado, double sueldoDiario, double asistencias)
        {

            double factorMinimo = 1.0452;

            var sbc = 0.0;

            if (sbcEmpleado <= 0 || sbcEmpleado == null)
                sbc = (double)sueldoDiario * (double)factorMinimo;
            else
                sbc = (double)sbcEmpleado;

            var anio = DateTime.Now.Year;

            var imssPatronal = 0.0;
            var imssTrabajador = 0.0;
            var u = await _repositorUma.FirstOrDefaultAsync(new UmaByAnioSpecification(anio));
            var unidadesMinimas = u.Monto * 3;

            var imssDescuentos = await _repositoryImssDescuento.ListAsync();
            foreach (var temp in imssDescuentos)
            {
                switch (temp.Exc)
                {

                    case 1:

                        imssPatronal += unidadesMinimas * (temp.Patron / 100) * 10;
                        break;

                    case 2:

                        var excedenteImss = 0.0;
                        var excedenteImssPatronal = 0.0;

                        if (sbc > unidadesMinimas)
                        {
                            double excedenteTemp = sbc - unidadesMinimas;
                            excedenteImss = excedenteTemp * (temp.Trabajador / 100) * asistencias;
                            excedenteImssPatronal = excedenteTemp * (temp.Patron / 100) * asistencias;

                        }
                        else
                        {
                            excedenteImss = 0;
                            excedenteImssPatronal = 0;
                        }
                        imssPatronal += excedenteImssPatronal;
                        imssTrabajador += excedenteImss;

                        break;

                    case 3:

                        imssPatronal += sbc * (temp.Patron / 100) * asistencias;
                        imssTrabajador += sbc * (temp.Trabajador / 100) * asistencias;

                        break;

                    default:
                        break;

                }
            }

            return imssTrabajador;
        }

        public async Task<double> CalcularIsr(double monto, int anio, int tipo)
        {
            var isr = await _repositoryIsr.FirstOrDefaultAsync(new IsrByAnioAndTipoAndMontoSpecification(anio, tipo, monto));
            return ((isr.Porcentaje / 100) * (monto - isr.LimiteInferior)) + isr.CuotaFija;

        }

        public async Task<double> CalcularSubsidio(double monto, int anio, int tipo)
        {
            var subsidio = await _repositorySubsidio.FirstOrDefaultAsync(new SubsidioByAnioAndTipoAndMontoSpecification(anio, tipo, monto));
            return subsidio.CuotaFija;

        }

        public int anioEntreDosFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            int años = fechaFin.Year - fechaInicio.Year;

            if (fechaFin.Month < fechaInicio.Month || (fechaFin.Month == fechaInicio.Month && fechaFin.Day < fechaInicio.Day))
            {
                años--;
            }

            return años;
        }

        public double CalculoAguinaldo(double salarioDiario, int diasAguinaldo, int asistencias) 
        {
            double monto = salarioDiario * diasAguinaldo;
            double aguinaldoDia = (double)monto / 365;
            double aguinaldo = aguinaldoDia * asistencias;

            return aguinaldo;
        }
        //Falta crear una base para que estos sean dinámicos dependiendo del año
        public double CalculoRCV(double salarioDiario)
        {
            double rcv = salarioDiario * (5.15 / 100);
            return rcv;
        }
        public double CalculoInfonavit(double salarioDiario)
        {
            double infonavit = salarioDiario * 30 * 0.05;
            return infonavit;
        }
        public double CalculoPrimaAntiguedad(double salarioDiario, DateTime fechaContrato, DateTime fechaFin)
        {
            var date = fechaFin.Subtract(fechaContrato);
            var years = (int)(date.Days / 365);

            double SM = 207.44; //Salario minimo 2023
            if (salarioDiario <= 2 * SM)
            { //en caso de que el salario sea menor a dos salarios minimos
                if (years < 1)
                {
                    DateTime year = new DateTime(fechaContrato.Year + 1, fechaContrato.Month, fechaContrato.Day);
                    TimeSpan diferencia = year - fechaContrato;
                    int months = (int)diferencia.TotalDays / 30;
                    if (months >= 6)
                    {
                        return ((salarioDiario * 12) / 12) * months;
                    }
                    else
                    {
                        return 0.00;
                    }
                }
                else
                {
                    return salarioDiario * 12 * years;
                }
            }
            else
            {
                if (years < 1)
                {
                    DateTime year = new DateTime(fechaContrato.Year + 1, fechaContrato.Month, fechaContrato.Day);
                    TimeSpan diferencia = year - fechaContrato;
                    int months = (int)diferencia.TotalDays / 30;
                    if (months >= 6)
                    {
                        return ((2 * SM * 12) / 12) * months;
                    }
                    else
                    {
                        return 0.00;
                    }
                }
                else
                {
                    return 2 * SM * 12 * years;
                }
            }

        }
        public double CalculoVacaciones(double salarioDiario,int diasVacaciones)
        {
            double vacaciones = salarioDiario * diasVacaciones;
            return vacaciones;
        }

        public async Task<double> CalcularTotal(Nomina nomina)
        {
            var nominaPercepciones = await _repositoryPercepcion.ListAsync(new PercepcionesByNominaSpecification(nomina.Id));
            var nominaDeducciones = await _repositoryDeduccion.ListAsync(new DeduccionesByNominaSpecification(nomina.Id));
            var totalPercepciones = 0.0;
            var totalDeducciones = 0.0;
            foreach (var percepcion in nominaPercepciones)
            {
                totalPercepciones += percepcion.ImporteGravado + percepcion.ImporteExento;
            }
            foreach (var deduccion in nominaDeducciones)
            {
                totalDeducciones += deduccion.Importe;
            }
            return totalPercepciones - totalDeducciones;
        }

    }
}
