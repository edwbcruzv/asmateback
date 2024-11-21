using Application.DTOs.Administracion;
using Application.DTOs.NIF;
using Application.DTOs.ReembolsosOperativos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Asistencias;
using Application.Specifications.Employees;
using Application.Specifications.Facturas;
using Application.Specifications.Nominas;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Application.Specifications.Catalogos;
using Org.BouncyCastle.Math;
using Application.Specifications.MiPortal.Prestamos;

namespace Shared.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
        private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsyncFacturaMovimiento;
        private readonly ITotalesMovsService _totalesMovsService;

        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
        private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsyncComplementoPagoFactura;

        private readonly IRepositoryAsync<Periodo> _repositoryAsyncPeriodo;
        private readonly IRepositoryAsync<RegistroAsistencia> _repositoryAsyncRegistroAsistencia;
        private readonly IRepositoryAsync<TipoAsistencia> _repositoryAsyncTipoAsistencia;

        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
        private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;

        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly IRepositoryAsync<NominaPercepcion> _repositoryAsyncNominaPercepcion;
        private readonly IRepositoryAsync<NominaDeduccion> _repositoryAsyncNominaDeduccion;
        private readonly IRepositoryAsync<NominaOtroPago> _repositoryAsyncNominaOtroPago;
        private readonly INominaService _nominaService;
        private readonly IRepositoryAsync<Vacacion> _repositoryAsyncVacacion;
        private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;

        public ExcelService(IRepositoryAsync<Factura> repositoryAsyncFactura, IRepositoryAsync<FacturaMovimiento> repositoryAsyncFacturaMovimiento,
            ITotalesMovsService totalesMovsService, IRepositoryAsync<Periodo> repositoryAsyncPeriodo,
            IRepositoryAsync<RegistroAsistencia> repositoryAsyncRegistroAsistencia, IRepositoryAsync<TipoAsistencia> repositoryAsyncTipoAsistencia,
            IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee,
            IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago, IRepositoryAsync<ComplementoPagoFactura> repositoryAsyncComplementoPagoFactura,
            IRepositoryAsync<Reembolso> repositoryAsyncReembolso, IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
            IRepositoryAsync<Nomina> repositoryAsyncNomina,
            IRepositoryAsync<NominaPercepcion> repositoryAsyncNominaPercepcion,
            IRepositoryAsync<NominaDeduccion> repositoryAsyncNominaDeduccion,
            IRepositoryAsync<NominaOtroPago> repositoryAsyncNominaOtroPago,
            INominaService nominaService,
            IRepositoryAsync<Vacacion> repositoryAsyncVacacion, IRepositoryAsync<Prestamo> repositoryAsyncPrestamo)
        {
            _repositoryAsyncFactura = repositoryAsyncFactura;
            _repositoryAsyncFacturaMovimiento = repositoryAsyncFacturaMovimiento;
            _totalesMovsService = totalesMovsService;
            _repositoryAsyncPeriodo = repositoryAsyncPeriodo;
            _repositoryAsyncRegistroAsistencia = repositoryAsyncRegistroAsistencia;
            _repositoryAsyncTipoAsistencia = repositoryAsyncTipoAsistencia;
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
            _repositoryAsyncComplementoPagoFactura = repositoryAsyncComplementoPagoFactura;
            _repositoryAsyncReembolso = repositoryAsyncReembolso;
            _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
            _repositoryAsyncNomina = repositoryAsyncNomina;
            _repositoryAsyncNominaPercepcion = repositoryAsyncNominaPercepcion;
            _repositoryAsyncNominaDeduccion = repositoryAsyncNominaDeduccion;
            _repositoryAsyncNominaOtroPago = repositoryAsyncNominaOtroPago;
            _nominaService = nominaService;
            _repositoryAsyncVacacion = repositoryAsyncVacacion;
            _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
        }


        public async Task<Response<SourceFileDto>> CreateExcelAsistenciaPorPeriodo(int Id)
        {
            var periodo = await _repositoryAsyncPeriodo.GetByIdAsync(Id);

            if(periodo == null)
            {
                throw new ApiException($"Periodo no encontrado para Id ${Id}");
            }

            var compania = await _repositoryAsyncCompany.GetByIdAsync(periodo.CompanyId);
            var employees = await _repositoryAsyncEmployee.ListAsync(new EmployeeByCompanyAndEstatusAnTipoPeriocidadSpecification(periodo.CompanyId, 1, periodo.TipoPeriocidadPagoId));

            var sourceFileDto = new SourceFileDto();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Asistencias");

                int col = 1, row = 1;

                var colorFondo = Color.FromArgb(127, 148, 144);
                var colorBorde = Color.Black;
                var colorLetra = Color.White;

                worksheet.Cells[row, col].Value = "Compañía";
                worksheet.Column(col).AutoFit();
                var celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;

                worksheet.Cells[row, col].Value = compania.Rfc;
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                col++;

                worksheet.Cells[row, col].Value = "Periodo";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;

                worksheet.Cells[row, col].Value = periodo.Etapa;
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                col++;

                worksheet.Cells[row, col].Value = "Desde";                
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;

                worksheet.Cells[row, col].Value = periodo.Desde;
                worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                col++;

                worksheet.Cells[row, col].Value = "Hasta";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;

                worksheet.Cells[row, col].Value = periodo.Hasta;
                worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                col++;

                colorFondo = Color.FromArgb(226, 180, 52);
                colorBorde = Color.Black;
                colorLetra = Color.White;

                row++;
                col = 1;

                worksheet.Cells[row, col].Value = "No. empleado";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;
                worksheet.Cells[row, col].Value = "RFC empleado";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;

                worksheet.Cells[row, col].Value = "Nombre";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;

                for (var dateTemp = periodo.Desde; dateTemp <= periodo.Hasta; dateTemp = dateTemp.AddDays(1))
                {
                    worksheet.Cells[row, col].Value = dateTemp;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                }

                row++;

                col = 1;

                colorFondo = Color.FromArgb(255, 255, 255);
                colorBorde = Color.Black;
                colorLetra = Color.Black;

                foreach (var employee in employees)
                {
                    worksheet.Cells[row, col].Value = employee.NoEmpleado;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;

                    worksheet.Cells[row, col].Value = employee.Rfc;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;

                    worksheet.Cells[row, col].Value = $"{employee.Nombre} {employee.ApellidoPaterno} {employee.ApellidoMaterno}";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;

                    for (var dateTemp = periodo.Desde; dateTemp <= periodo.Hasta; dateTemp = dateTemp.AddDays(1))
                    {

                        var registroAsistencia = await _repositoryAsyncRegistroAsistencia.FirstOrDefaultAsync(new AsistenciaByEmployeeAndDaySpecification(employee.Id, dateTemp));

                        if (registroAsistencia != null)
                        {
                            var asistencia = await _repositoryAsyncTipoAsistencia.GetByIdAsync(registroAsistencia.TipoAsistenciaId);
                            worksheet.Cells[row, col].Value = asistencia.Clave;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "";
                        }

                        worksheet.Column(col).AutoFit();
                        celda = worksheet.Cells[row, col];
                        AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                        col++;
                    }

                    col = 1;
                    row ++;

                }

                row++;

                colorFondo = Color.FromArgb(127, 148, 144);
                colorBorde = Color.Black;
                colorLetra = Color.White;

                worksheet.Cells[row, col].Value = "Concepto";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;

                worksheet.Cells[row, col].Value = "Clave";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col = 1;
                row ++;                

                var tipoAsistencias = await _repositoryAsyncTipoAsistencia.ListAsync();

                colorFondo = Color.FromArgb(232, 232, 232);
                colorBorde = Color.Black;
                colorLetra = Color.Black;

                foreach (var tipoAsistencia in tipoAsistencias)
                {

                    worksheet.Cells[row, col].Value = tipoAsistencia.Clave;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col ++;

                    worksheet.Cells[row, col].Value = tipoAsistencia.Descripcion;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col = 1;

                    row++;

                }

                

                DateTime fechaActual = DateTime.Now;
                string generalName = $@"ExcelAsistencias-" + fechaActual.Year + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond + ".xlsx";
                string baseSrc = $@"StaticFiles\Mate\Excel\Asistencias\";
                string disk = $@"C:\";

                package.SaveAs(new FileInfo(disk + baseSrc + generalName));

                sourceFileDto.SourceFile = baseSrc + generalName;

            }

            return new Response<SourceFileDto>(sourceFileDto);

        }
        public async Task<Response<bool>> ReadExcelAsistenciaPorPeriodo(int Id, IFormFile file)
        {

            var periodo = await _repositoryAsyncPeriodo.GetByIdAsync(Id); 

            if (periodo == null)
            {
                throw new ApiException($"Periodo no encontrado para Id ${Id}");

            }

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {
                // Ahora puedes acceder a los datos en la hoja de trabajo y realizar las operaciones necesarias.

                ExcelWorksheet worksheet = package.Workbook.Worksheets["Asistencias"];

                var rfc_company = worksheet.Cells[1, 2].Value;
                var etapa = worksheet.Cells[1, 4].Value;

                DateTime fechaDesde;
                object cellValueDesde = worksheet.Cells[1, 6].Value;

                DateTime fechaHasta;
                object cellValueHasta = worksheet.Cells[1, 8].Value;


                if (cellValueDesde is double doubleValue)
                {
                    fechaDesde = DateTime.FromOADate(doubleValue);
                }
                else
                {
                    fechaDesde = (DateTime)worksheet.Cells[1, 6].Value;
                }

                if (cellValueHasta is double doubleValueH)
                {
                    fechaHasta = DateTime.FromOADate(doubleValueH);
                }
                else
                {
                    fechaHasta = (DateTime)worksheet.Cells[1, 8].Value;
                }


                var desde = fechaDesde;//(DateTime)worksheet.Cells[1, 6].Value;//DateTime.FromOADate((double)worksheet.Cells[1, 6].Value);
                var hasta = fechaHasta;//(DateTime)worksheet.Cells[1, 8].Value;//DateTime.FromOADate((double)worksheet.Cells[1, 8].Value);

                if (rfc_company != null && etapa != null && desde != null && hasta != null)
                {
                    var periodo_temp = await _repositoryAsyncPeriodo.FirstOrDefaultAsync(new PeriodosByEtapaAndDesdeAndHastaSpecification(Convert.ToInt32(etapa), desde, hasta ,periodo.CompanyId));

                    if (periodo_temp == null)
                    {
                        throw new ApiException($"El periodo con la etapa {etapa} de {desde} hasta {hasta} no se encontra registrada.");
                    }

                    var company = await _repositoryAsyncCompany.GetByIdAsync(periodo_temp.CompanyId);
                    
                    if (company == null)
                    {
                        throw new ApiException($"La compania con id {periodo_temp.CompanyId} no se encontra registrada.");
                    }

                    if( !company.Rfc.Equals(rfc_company) )
                    {
                        throw new ApiException($"El RFC de compañía no encontrado para periodo con Id {periodo.Id}");
                    }
            
                    await empleados(worksheet, desde, hasta);
                }
                else
                {
                    throw new ApiException($"Algunos Parametros en el Excel son erroneos.");
                }

                
            }

            return new Response<bool>(true);

            //throw new NotImplementedException();
        }
        public async Task<Response<string>> CreateExcelCalculoNif()
        {
            String rutaArchivo = "";
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Nif");
                
                var colorFondo = Color.FromArgb(127, 148, 144);
                var colorBorde = Color.Black;
                var colorLetra = Color.Black;

                worksheet.Cells[1, 1].Value = "n";
                worksheet.Cells[1, 2].Value = "RFC";
                worksheet.Cells[1, 3].Value = "Apellido Paterno";
                worksheet.Cells[1, 4].Value = "Apellido Materno";
                worksheet.Cells[1, 5].Value = "Nombre";
                worksheet.Cells[1, 6].Value = "Sueldo mensual";
                worksheet.Cells[1, 7].Value = "Fecha de contratación";
                worksheet.Cells[1, 8].Value = "Dias de vacaciones";
                worksheet.Cells[1, 9].Value = "Dias de aguinaldo";
                worksheet.Cells[1, 10].Value = "SBC";
                worksheet.Cells[1, 11].Value = "Porcentaje prima vacacional";
                worksheet.Cells[1, 12].Value = "Mes";
                worksheet.Cells[1, 13].Value = "Año";
                worksheet.Cells[1, 14].Value = "ISR";
                worksheet.Cells[1, 15].Value = "Subsidio";
                worksheet.Cells[1, 16].Value = "RCV";
                worksheet.Cells[1, 17].Value = "Cuotas obrero patronales";
                worksheet.Cells[1, 18].Value = "Infonavit";
                worksheet.Cells[1, 19].Value = "Prima de antiguedad";
                worksheet.Cells[1, 20].Value = "Prima vacacional";
                worksheet.Cells[1, 21].Value = "Monto por vacaciones";
                worksheet.Cells[1, 22].Value = "Aguinaldo";

                for(int column = 1; column <= 22; column++)
                {
                    worksheet.Column(column).AutoFit();
                    var celda = worksheet.Cells[1, column];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                }

                DateTime fechaActual = DateTime.Now;
                string generalName = $@"ArchivoBase.xlsx";
                string baseSrc = $@"StaticFiles\Mate\Excel\Nif\ArchivoBase\";
                string disk = $@"C:\";

                package.SaveAs(new FileInfo(disk + baseSrc + generalName));
                rutaArchivo = baseSrc + generalName;
            }
            

            Response <String> respuesta = new Response<String>();
            respuesta.Succeeded = true;
            respuesta.Message = null;
            respuesta.Data = rutaArchivo;

            return respuesta;
        }
        public async Task<List<NifResultadoDTO>> LeerExcelNif(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            List<NifResultadoDTO> lista = new List<NifResultadoDTO>();
            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {
                // Ahora puedes acceder a los datos en la hoja de trabajo y realizar las operaciones necesarias.

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int row = 2;
                int column;
                int rowCount = 2;
                while (worksheet.Cells[row, 1].Value != null) {
                    NifResultadoDTO res = new NifResultadoDTO();
                    for (column = 1; column <= 11; column ++) {
                        switch (column)
                        {
                            // A: ID
                            case 1:
                                res.IdEmpleado = Convert.ToInt32(worksheet.Cells[row, column].Value);
                                break;
                            // B: RFC
                            case 2:
                                res.Rfc = Convert.ToString(worksheet.Cells[row, column].Value);
                                break;
                            // C: Apellido paterno
                            case 3:
                                res.ApellidoPaterno = Convert.ToString(worksheet.Cells[row, column].Value);
                                break;
                            // D: Apellido materno
                            case 4:
                                res.ApellidoMaterno = Convert.ToString(worksheet.Cells[row, column].Value);
                                break;
                            // E: Nombre
                            case 5:
                                res.Nombre = Convert.ToString(worksheet.Cells[row, column].Value);
                                break;
                            // F: Sueldo mensual
                            case 6:
                                res.SueldoBase = Convert.ToDouble(worksheet.Cells[row, column].Value);
                                // Sueldo diario
                                double sueldoDiario = res.SueldoBase / 30;
                                res.SueldoDiario = sueldoDiario;
                                break;
                            // G: Fecha contratación
                            case 7:
                                res.FechaIngreso = DateTime.FromOADate((double)worksheet.Cells[row, column].Value);
                                break;
                            // H: Dias de vacaciones
                            case 8:
                                res.diasVacciones = Convert.ToInt32(worksheet.Cells[row, column].Value);
                                break;
                            // I: Dias de aguinaldo
                            case 9:
                                res.diasAguinaldo = Convert.ToInt32(worksheet.Cells[row, column].Value);
                                break;
                            // J: SBC
                             case 10:
                                res.SBC = Convert.ToDouble(worksheet.Cells[row, column].Value);
                                break;
                            // K: Prima Vacacional
                            case 11:
                                res.PorcentajePrimaVacacional = Convert.ToDouble(worksheet.Cells[row, column].Value);
                                break;  
                        }           
                    }
                    lista.Add(res);
                    row++;
                    rowCount++;
                }
            }
            return lista;
        }
        public async Task<String> EscribirResultadoNif(List<List<NifResultadoDTO>> lista)
        {
            String rutaArchivo = "";
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ResultadoNif");
                int row = 2;
                int column;

                var colorFondo = Color.FromArgb(127, 148, 144);
                var colorBorde = Color.Black;
                var colorLetra = Color.Black;

              

                foreach (List<NifResultadoDTO> employee in lista)
                {
                    // Registro que ayuda a imprimir solo una vez los datos de entrada del empleado
                    var primerRegistro = employee.First();
                    for (column = 1; column <= 11; column++)
                    {
                        switch (column)
                        {
                            // A: ID
                            case 1:
                                worksheet.Cells[row, column].Value = primerRegistro.IdEmpleado;
                                break;
                            // B: RFC
                            case 2:
                                worksheet.Cells[row, column].Value = primerRegistro.Rfc;
                                break;
                            // C: Apellido paterno
                            case 3:
                                worksheet.Cells[row, column].Value = primerRegistro.ApellidoPaterno;
                                break;
                            // D: Apellido materno
                            case 4:
                                worksheet.Cells[row, column].Value = primerRegistro.ApellidoMaterno;
                                break;
                            // E: Nombre 
                            case 5:
                                worksheet.Cells[row, column].Value = primerRegistro.Nombre;
                                break;
                            // F: Sueldo mensual
                            case 6:
                                worksheet.Cells[row, column].Value = primerRegistro.SueldoBase;
                                break;
                            // G: Fecha contratación
                            case 7:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "Date";
                                worksheet.Cells[row, column].Value = DateOnly.FromDateTime(primerRegistro.FechaIngreso);
                                break;
                            // H: Dias de vacaciones
                            case 8:
                                worksheet.Cells[row, column].Value = primerRegistro.diasVacciones;
                                break;
                            // I: Dias de aguinaldo
                            case 9:
                                worksheet.Cells[row, column].Value = primerRegistro.diasAguinaldo;
                                break;
                            // J: SBC
                            case 10:
                                worksheet.Cells[row, column].Value = primerRegistro.SBC;
                                break;
                            // K: Porcentaje prima vacacional
                            case 11:
                                worksheet.Cells[row, column].Value = primerRegistro.PorcentajePrimaVacacional;
                                break;
                        }
                    }
                    foreach (NifResultadoDTO nifPorMes in employee)
                    {
                        for (column = 12; column <= 22; column++)
                        {
                            switch (column)
                            {
                                // L: Mes 
                                case 12:
                                    worksheet.Cells[row, column].Value = nifPorMes.Mes;
                                    break;
                                // M: Año
                                case 13:
                                    worksheet.Cells[row, column].Value = nifPorMes.Año;
                                    break;
                                // N: ISR
                                case 14:
                                    worksheet.Cells[row, column].Value = nifPorMes.Isr;
                                    break;
                                // O: Subsidio
                                case 15:
                                    worksheet.Cells[row, column].Value = nifPorMes.Subsidio;
                                    break;
                                // P: RCV
                                case 16:
                                    worksheet.Cells[row, column].Value = nifPorMes.Rcv;
                                    break;
                                // Q: Cuotas obrero patronales
                                case 17:
                                    worksheet.Cells[row, column].Value = nifPorMes.CuotasPatronales;
                                    break;
                                // R: Infonavit 
                                case 18:
                                    worksheet.Cells[row, column].Value = nifPorMes.Infonavit;
                                    break;
                                // S: Prima de antiguedad
                                case 19:
                                    worksheet.Cells[row, column].Value = nifPorMes.PrimaAntiguedad;
                                    break;
                                // T: Prima vacacional
                                case 20:
                                    worksheet.Cells[row, column].Value = nifPorMes.PrimaVacacional;
                                    break;
                                // U: Monto por vacaciones
                                case 21:
                                    worksheet.Cells[row, column].Value = nifPorMes.Vacaciones;
                                    break;
                                // V: Aguinaldo 
                                case 22:
                                    worksheet.Cells[row, column].Value = nifPorMes.Aguinaldo;
                                    break;
                            }

                        }
                        row++;
                    }
                    row++;
                }

                worksheet.Cells[1, 1].Value = "n";
                worksheet.Cells[1, 2].Value = "RFC";
                worksheet.Cells[1, 3].Value = "Apellido Paterno";
                worksheet.Cells[1, 4].Value = "Apellido Materno";
                worksheet.Cells[1, 5].Value = "Nombre";
                worksheet.Cells[1, 6].Value = "Sueldo mensual";
                worksheet.Cells[1, 7].Value = "Fecha de contratación";
                worksheet.Cells[1, 8].Value = "Dias de vacaciones";
                worksheet.Cells[1, 9].Value = "Dias de aguinaldo";
                worksheet.Cells[1, 10].Value = "SBC";
                worksheet.Cells[1, 11].Value = "Porcentaje prima vacacional";
                worksheet.Cells[1, 12].Value = "Mes";
                worksheet.Cells[1, 13].Value = "Año";
                worksheet.Cells[1, 14].Value = "ISR";
                worksheet.Cells[1, 15].Value = "Subsidio";
                worksheet.Cells[1, 16].Value = "RCV";
                worksheet.Cells[1, 17].Value = "Cuotas obrero patronales";
                worksheet.Cells[1, 18].Value = "Infonavit";
                worksheet.Cells[1, 19].Value = "Prima de antiguedad";
                worksheet.Cells[1, 20].Value = "Prima vacacional";
                worksheet.Cells[1, 21].Value = "Monto por vacaciones";
                worksheet.Cells[1, 22].Value = "Aguinaldo";

                for (column = 1; column <= 22; column++)
                {
                    worksheet.Column(column).AutoFit();
                    var celda = worksheet.Cells[1, column];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                }

                DateTime fechaActual = DateTime.Now;
                string generalName = $@"NifCalculos-" + fechaActual.Year + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond + ".xlsx";
                string baseSrc = $@"StaticFiles\Mate\Excel\Nif\Calculos\";
                string disk = $@"C:\";

                package.SaveAs(new FileInfo(disk + baseSrc + generalName));
                rutaArchivo = baseSrc + generalName;

            }
            return rutaArchivo;
        }

        public async Task<String> ArchivoExcelMovimientosReembolso(List<MovimientoReembolsoDTO> listaMovimientos)
        {
            // Ruta archivo de salida
            String rutaArchivo = "";

            // Variables para los totales
            double tSubTotal = 0.0;
            double tDescuento = 0.0;
            double tIVA = 0.0;
            double tIEPS = 0.0;
            double tISH = 0.0;
            double tTotal = 0.0;

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Movimientos Reembolso");
                int row = 2;
                int column;

                var colorFondo = Color.FromArgb(0, 204, 255);
                var colorBorde = Color.Black;
                var colorLetra = Color.Black;

                foreach(MovimientoReembolsoDTO movimiento in listaMovimientos)
                {
                    for (column = 1; column <= 13; column++)
                    {
                        switch (column)
                        {
                            // Fecha 
                            case 1:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "Date";
                                worksheet.Cells[row, column].Value = DateOnly.FromDateTime(movimiento.FechaMovimiento);
                                break;
                            // Nombre
                            case 2:
                                worksheet.Cells[row, column].Value = movimiento.EmisorNombre;
                                break;
                            // RFC
                            case 3:
                                worksheet.Cells[row, column].Value = movimiento.EmisorRFC;
                                break;
                            // Concepto
                            case 4:
                                worksheet.Cells[row, column].Value = movimiento.Concepto;
                                break;
                            // Moneda
                            case 5:
                                worksheet.Cells[row, column].Value = movimiento.TipoMoneda;
                                break;
                            // Tipo de cambio
                            case 6:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[row, column].Value = movimiento.TipoCambio;
                                break;
                            // Subtotal
                            case 7: 
                                worksheet.Cells[row, column].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[row, column].Value = movimiento.Subtotal;
                                // tSubTotal += (double)movimiento.Subtotal * (double)movimiento.TipoCambio;
                                tSubTotal += (double)movimiento.Subtotal;
                                break;
                            // Descuento
                            case 8:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[row, column].Value = 0.0;
                                //Descuento total pendiente por agregar
                                break;
                            // IVA
                            case 9:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[row, column].Value = movimiento.IVATrasladados;
                                // tIVA += (double)movimiento.IVATrasladados * (double)movimiento.TipoCambio;
                                tIVA += (double)movimiento.IVATrasladados;
                                break;
                            // IEPS
                            case 10:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[row, column].Value = movimiento.IEPS;
                                // tIEPS += (double)movimiento.IEPS * (double)movimiento.TipoCambio;
                                tIEPS += (double)movimiento.IEPS;
                                break;
                            // ISH
                            case 11:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[row, column].Value = movimiento.ISH;
                                // tISH += (double)movimiento.ISH * (double)movimiento.TipoCambio;
                                tISH += (double)movimiento.ISH;
                                break;
                            // Total
                            case 12:
                                worksheet.Cells[row, column].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[row, column].Value = movimiento.Total;
                                // tTotal += movimiento.Total * (double)movimiento.TipoCambio;
                                tTotal += movimiento.Total;
                                break;
                            // UUID
                            case 13: // ------
                                worksheet.Cells[row, column].Value = movimiento.Uuid;
                                break;

                        }
                    }
                    row++;
                }

                // Escribiendo los totales y aplicando estilo
                row++;
                
                worksheet.Cells[row, 6].Value = "Totales";
                

                worksheet.Cells[row, 7].Value = tSubTotal;
                worksheet.Cells[row, 7].Style.Numberformat.Format = "0.00";

                worksheet.Cells[row, 8].Value = tDescuento;
                worksheet.Cells[row, 8].Style.Numberformat.Format = "0.00";

                worksheet.Cells[row, 9].Value = tIVA;
                worksheet.Cells[row, 9].Style.Numberformat.Format = "0.00";

                worksheet.Cells[row, 10].Value = tIEPS;
                worksheet.Cells[row, 10].Style.Numberformat.Format = "0.00";

                worksheet.Cells[row, 11].Value = tISH;
                worksheet.Cells[row, 11].Style.Numberformat.Format = "0.00";

                worksheet.Cells[row, 12].Value = tTotal;
                worksheet.Cells[row, 12].Style.Numberformat.Format = "0.00";

                // Escribiendo los títulos de las columnas
                worksheet.Cells[1, 1].Value = "Fecha";
                worksheet.Cells[1, 2].Value = "Nombre";
                worksheet.Cells[1, 3].Value = "RFC";
                worksheet.Cells[1, 4].Value = "Concepto";
                worksheet.Cells[1, 5].Value = "Moneda";
                worksheet.Cells[1, 6].Value = "Tipo de cambio";
                worksheet.Cells[1, 7].Value = "Subtotal";
                worksheet.Cells[1, 8].Value = "Descuento";
                worksheet.Cells[1, 9].Value = "IVA";    
                worksheet.Cells[1, 10].Value = "IEPS";
                worksheet.Cells[1, 11].Value = "ISH";
                worksheet.Cells[1, 12].Value = "Total";
                worksheet.Cells[1, 13].Value = "UUID";
                

                // Agregando estilo a los títulos de las columnas
                for (column = 1; column <= 13; column++)
                {
                    worksheet.Column(column).AutoFit();
                    var celda = worksheet.Cells[1, column];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                }

                // Nombre y ubicación del archivo
                DateTime fechaActual = DateTime.Now;
                string generalName = $@"MovimientosReembolso" + fechaActual.Year + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond + ".xlsx";
                string baseSrc = $@"StaticFiles\Mate\Excel\Reembolsos\MovimientosReembolso\";
                string disk = $@"C:\";

                package.SaveAs(new FileInfo(disk + baseSrc + generalName));
                rutaArchivo = baseSrc + generalName;

            }

            return rutaArchivo;
        }
        public async Task<bool> empleados(ExcelWorksheet worksheet, DateTime desde, DateTime hasta)
        {
            int row = 3;
            while (true)
            {
                var no_emp = worksheet.Cells[row, 1].Value;
                if (no_emp != null)
                {
                    await fechas(worksheet, row++, Convert.ToInt64(no_emp),desde,hasta);
                }
                else
                {
                    break;
                }
            }

            return true;
        }

        public async Task<bool> fechas(ExcelWorksheet worksheet, int emp_row, Int64 no_emp,DateTime desde, DateTime hasta)
        {
            //empleado que se esta analizando
            Employee employee = await _repositoryAsyncEmployee.FirstOrDefaultAsync(new EmployeeByNoEmpleadoSpecification(no_emp));
            
            int col = 4; // columna donde inician las fechas
            DateTime fecha;
            while (true)
            {
                DateTime fecha_var;
                object cellValue = worksheet.Cells[2, col].Value;

                if (cellValue != null)
                {
                    if (cellValue is double doubleValue)
                    {
                        fecha_var = DateTime.FromOADate(doubleValue);
                    }
                    else
                    {
                        fecha_var = (DateTime)worksheet.Cells[2, col].Value;
                        //throw new ApiException($"El valor de la fecha no es de tipo double.");
                    }
                }
                else
                {
                    fecha_var = DateTime.MinValue; 
                }

                if (fecha_var != DateTime.MinValue)
                {

                    if(!(desde <= fecha_var && fecha_var <= hasta))
                    {
                        throw new ApiException($"La fecha {fecha_var} no se encuentra dentro del periodo {desde} - {hasta}");
                    }

                    var tipo_asistencia = worksheet.Cells[emp_row, col++].Value;

                    RegistroAsistencia registroAsistencia_db = await _repositoryAsyncRegistroAsistencia.FirstOrDefaultAsync(new AsistenciaByEmployeeAndDaySpecification(employee.Id, fecha_var));
                    
                    if (tipo_asistencia != null && tipo_asistencia.ToString() != "")
                    {   
                        // se encontro una asistencia

                        TipoAsistencia tipoAsistencia = await _repositoryAsyncTipoAsistencia.FirstOrDefaultAsync(new AsistenciasByClaveSpecification(tipo_asistencia.ToString()));
                        
                        if(registroAsistencia_db == null && tipoAsistencia != null)
                        {
                            // Creando nueva asistencia con dicha asistencia valida
                            RegistroAsistencia registroAsistencia_temp = new RegistroAsistencia();
                            registroAsistencia_temp.EmployeeId = employee.Id;
                            registroAsistencia_temp.Dia = fecha_var;
                            registroAsistencia_temp.TipoAsistenciaId = tipoAsistencia.Id;
                            // guardando asistencia en la db
                            var response = await _repositoryAsyncRegistroAsistencia.AddAsync(registroAsistencia_temp);
                            
                        }
                        else if(tipoAsistencia != null )
                        {
                            // se actualiza la asistencia existente con dicha asistencia valida
                            registroAsistencia_db.TipoAsistenciaId = tipoAsistencia.Id;
                            await _repositoryAsyncRegistroAsistencia.UpdateAsync(registroAsistencia_db);
                        }
                        else
                        {
                            // se leyo una asistencia que no esta definida en la db y se detiene la lectura
                            throw new ApiException($"Asistencia '{tipo_asistencia }' no encontrada, worksheet.Cell[{emp_row},{col-1}]");
                        }
                    }
                    else
                    {   
                        // no se encontro asistencia
                        if (registroAsistencia_db != null)
                        {
                            // se elimina la asistencia de la db
                            await _repositoryAsyncRegistroAsistencia.DeleteAsync(registroAsistencia_db);

                        }
                            continue;
                    }
                }
                else
                {
                    break;
                }
            }

            return true;
        }

        public void AplicarEstiloCelda(ExcelRange celda, Color colorFondo, Color colorBorde, Color colorLetra)
        {
            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
            celda.Style.Fill.BackgroundColor.SetColor(colorFondo);

            var border = celda.Style.Border;
            border.Top.Style = ExcelBorderStyle.Thin;
            border.Top.Color.SetColor(colorBorde);
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Bottom.Color.SetColor(colorBorde);
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Left.Color.SetColor(colorBorde);
            border.Right.Style = ExcelBorderStyle.Thin;
            border.Right.Color.SetColor(colorBorde);

            celda.Style.Font.Color.SetColor(colorLetra);
        }

        /*
         * Facturas
         */
        public async Task<Response<SourceFileDto>> ExcelFacturas(int CompanyId)
        {

            SourceFileDto response = new SourceFileDto();

            var facturas = await _repositoryAsyncFactura.ListAsync(new FacturaByCompanySpecification(CompanyId));

            int col = 1, row = 1;

            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new OfficeOpenXml.ExcelPackage())
            {

                var worksheet = package.Workbook.Worksheets.Add("Facturas");

                var colorFondo = Color.FromArgb(226, 180, 52);
                var colorBorde = Color.Black;
                var colorLetra = Color.White;

                worksheet.Cells[row, col].Value = "Id"; 
                worksheet.Column(col).AutoFit();
                var celda = worksheet.Cells[row, col];                 
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;
                worksheet.Cells[row, col].Value = "Cliente";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Folio";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "IVA";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "IVA 6%";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Retención ISR";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Retención IVA";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Subtotal";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Total";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Fecha creado"; 
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Fecha timbrado";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Estatus";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Fecha de pago";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Estatus de pago";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;

                foreach (var factura in facturas)
                {
                    row++;
                    col = 1;

                    var facturaMovs = await _repositoryAsyncFacturaMovimiento.ListAsync(new FacturaMovimientoByFacturaSpecification(factura.Id));

                    var tm = _totalesMovsService.getTotalesFormMovs(facturaMovs);

                    worksheet.Cells[row, col].Value = factura.Id;
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = factura.ReceptorRazonSocial;
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = factura.Folio;
                    worksheet.Column(col).AutoFit();
                    col++;
                
                    worksheet.Cells[row, col].Value = tm.trasladadosTotal;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Column(col).AutoFit();
                    col++;

                    worksheet.Cells[row, col].Value = tm.retencionIva6Total;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Column(col).AutoFit();
                    col++;


                    worksheet.Cells[row, col].Value = tm.retencionIsrTotal;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Column(col).AutoFit();
                    col++;

                    worksheet.Cells[row, col].Value = tm.retencionIvaTotal;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = tm.subTotal;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = tm.total;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = factura.Created;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = factura.FechaTimbrado;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Column(col).AutoFit();
                    col++;
                    switch (factura.Estatus)
                    {
                        case 1:
                            worksheet.Cells[row, col].Value = "Creado";
                            break;
                        case 2:
                            worksheet.Cells[row, col].Value = "Facturado";
                            break;
                        case 3:
                            worksheet.Cells[row, col].Value = "Cancelado";
                            break;
                        default:
                            worksheet.Cells[row, col].Value = "N/A";
                            break;
                    }
                        
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = factura.FechaPago;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Column(col).AutoFit();
                    col++;
                    if(factura.FechaPago == null)
                        worksheet.Cells[row, col].Value = "No pagado";
                    else
                        worksheet.Cells[row, col].Value = "Pagado";
                    worksheet.Column(col).AutoFit();
                    col++;

    


                }

                DateTime fechaActual = DateTime.Now;
                string generalName = $@"ExcelFacturacion-" + fechaActual.Year + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond + ".xlsx";
                string baseSrc = $@"StaticFiles\Mate\Excel\Facturacion\";
                string disk = $@"C:\";

                package.SaveAs(new FileInfo(disk+baseSrc+generalName));

                response.SourceFile = baseSrc + generalName;

            }
                
            return new Response<SourceFileDto>(response);
;        }

        /*
         * Complemento pagos
         */
        public async Task<Response<SourceFileDto>> ExcelComplementoPago(int CompanyId)
        {
            SourceFileDto response = new SourceFileDto();

            var complementoPagos = await _repositoryAsyncComplementoPago.ListAsync(new ComplementoPagoByCompanySpecification(CompanyId));

            int col = 1, row = 1;

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage())
            {

                var worksheet = package.Workbook.Worksheets.Add("Complemento pago");

                var colorFondo = Color.FromArgb(226, 180, 52);
                var colorBorde = Color.Black;
                var colorLetra = Color.White;

                worksheet.Cells[row, col].Value = "Id";
                worksheet.Column(col).AutoFit();
                var celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                col++;
                worksheet.Cells[row, col].Value = "Cliente";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Folio";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();      
                col++;
                worksheet.Cells[row, col].Value = "Total";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Fecha creado";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Fecha timbrado";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Estatus";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Fecha de pago";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;
                worksheet.Cells[row, col].Value = "Estatus de pago";
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                worksheet.Column(col).AutoFit();
                col++;

                foreach (var complementoPago in complementoPagos)
                {
                    row++;
                    col = 1;

                    var facturaMovs = await _repositoryAsyncComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(complementoPago.Id));

                    var total = 0.0;
                    foreach(var item in facturaMovs) {
                        total += item.Monto;
                    }

                    worksheet.Cells[row, col].Value = complementoPago.Id;
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = complementoPago.ReceptorRazonSocial;
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = complementoPago.Folio;
                    worksheet.Column(col).AutoFit();
                    col++;                    

                    worksheet.Cells[row, col].Value = total;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = complementoPago.Created;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = complementoPago.FechaTimbrado;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Column(col).AutoFit();
                    col++;
                    switch (complementoPago.Estatus)
                    {
                        case 1:
                            worksheet.Cells[row, col].Value = "Creado";
                            break;
                        case 2:
                            worksheet.Cells[row, col].Value = "Facturado";
                            break;
                        case 3:
                            worksheet.Cells[row, col].Value = "Cancelado";
                            break;
                        default:
                            worksheet.Cells[row, col].Value = "N/A";
                            break;
                    }

                    worksheet.Column(col).AutoFit();
                    col++;
                    worksheet.Cells[row, col].Value = complementoPago.FechaPago;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Column(col).AutoFit();
                    col++;
                    if (complementoPago.FechaPago == null)
                        worksheet.Cells[row, col].Value = "No pagado";
                    else
                        worksheet.Cells[row, col].Value = "Pagado";
                    worksheet.Column(col).AutoFit();
                    col++;




                }

                DateTime fechaActual = DateTime.Now;
                string generalName = $@"ExcelComplementoPago-" + fechaActual.Year + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond + ".xlsx";
                string baseSrc = $@"StaticFiles\Mate\Excel\ComplementoPago\";
                string disk = $@"C:\";

                package.SaveAs(new FileInfo(disk + baseSrc + generalName));

                response.SourceFile = baseSrc + generalName;
            }

            return new Response<SourceFileDto>(response);
        }

        public async Task<Response<SourceFileDto>> CreateExcelReporteDeNominas(int periodoId)
        {
            var periodo = await _repositoryAsyncPeriodo.GetByIdAsync(periodoId);
            if (periodo == null)
            {
                throw new ApiException($"Periodo no encontrado para Id ${periodoId}");
            }
            var compania = await _repositoryAsyncCompany.GetByIdAsync(periodo.CompanyId);
            var nominas = await _repositoryAsyncNomina.ListAsync(new NominasByCompanyIdSpecification(periodo.CompanyId, periodoId));

            var sourceFileDto = new SourceFileDto();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte de nómina");

                int col = 1, row = 1;

                var colorFondoCompaniaPeriodo = Color.FromArgb(0, 108, 179);
                var colorFondo = Color.FromArgb(226, 180, 52);
                var colorBorde = Color.Black;
                var colorLetra = Color.White;

                // Celda A1
                worksheet.Cells[row, col].Value = "Compañía";
                worksheet.Column(col).AutoFit();
                var celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondoCompaniaPeriodo, colorBorde, colorLetra);
                col++;

                // Celda B1
                worksheet.Cells[row, col].Value = compania.Rfc;
                worksheet.Column(col).AutoFit();
                col++;

                // Celda C1
                worksheet.Cells[row, col].Value = "Periodo";
                worksheet.Column(col).AutoFit();
                celda = worksheet.Cells[row, col];
                AplicarEstiloCelda(celda, colorFondoCompaniaPeriodo, colorBorde, colorLetra);
                col++;

                // Celda D1
                worksheet.Cells[row, col].Value = periodo.Etapa;
                worksheet.Column(col).AutoFit();

                // Cambiando de fila
                row++;
                col = 1;

                string[] campos = {
                    "Tipo de nómina",
                    "No. de empleado",
                    "RFC",
                    "Nombre",
                    "Estado",
                    "Sueldo diario",
                    "Sueldo base de cotización",
                    "Sueldo diario integrado",
                    "Sueldo base",
                    "Ayuda para renta",
                    "Ayuda transporte",
                    "Prima vacacional",
                    "Otros",
                    "Seguro de retiro",
                    "Total percepciones",
                    "Ajuste ISR",
                    "ISR",
                    "Cuotas obrero patronales",
                    "Pago por crédito de vivienda",
                    "Ahorro voluntario",
                    "Ajuste en seguro de retiro exento",
                    "Capital",
                    "Interes",
                    "Fondo de garantia",
                    "Total préstamo",
                    "Total deducciones",
                    "Sueldo neto"
                    };

                // Fila 2
                foreach (var campo in campos)
                {
                    worksheet.Cells[row, col].Value = campo;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondoCompaniaPeriodo, colorBorde, colorLetra);
                    col++;
                }

                foreach (var nomina in nominas)
                {
                    colorFondo = Color.White;
                    colorBorde = Color.Black;
                    colorLetra = Color.Black;
                    row++;
                    col = 1;
                    var employee = await _repositoryAsyncEmployee.GetByIdAsync(nomina.EmployeeId);
                    var percepciones = await _repositoryAsyncNominaPercepcion.ListAsync(new PercepcionesByNominaSpecification(nomina.Id));
                    var deducciones = await _repositoryAsyncNominaDeduccion.ListAsync(new DeduccionesByNominaSpecification(nomina.Id));
                    var otrosPagos = await _repositoryAsyncNominaOtroPago.ListAsync(new OtrosPagosByNominaSpecification(nomina.Id));
                    
                    var totalPercepciones = 0.0;
                    var totalDeducciones = 0.0;
                    var prestamo = 0.0;


                    foreach (var percepcion in percepciones)
                    {
                        totalPercepciones += percepcion.ImporteGravado + percepcion.ImporteExento;
                    }
                    foreach (var deduccion in deducciones)
                    {
                        if (deduccion.Clave.Trim() == "004")
                        {
                            prestamo = deduccion.Importe;
                        }
                        totalDeducciones += deduccion.Importe;
                    }

                    var capital = 0.0;
                    var interes = 0.0;
                    var fg = 0.0;
                    if (prestamo > 0)
                    {
                        var prestamoActivo = await _repositoryAsyncPrestamo.ListAsync(new PrestamoByEmployeeIdAndIsActivoSpecification(employee.Id));
                        capital = prestamo * (prestamoActivo[0].Monto / prestamoActivo[0].Total);
                        interes = prestamo * (prestamoActivo[0].Interes / prestamoActivo[0].Total);
                        fg = prestamo * (prestamoActivo[0].FondoGarantia / prestamoActivo[0].Total);
                    }

                    // Tipo de nómina.
                    worksheet.Cells[row, col].Value = nomina.TipoNomina == 1 ? "Normal" : "Extraordinaria";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Número de empleado.
                    worksheet.Cells[row, col].Value = employee.NoEmpleado;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // RFC.
                    worksheet.Cells[row, col].Value = nomina.ReceptorRfc;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Nombre.
                    worksheet.Cells[row, col].Value = employee.ApellidoPaterno + " " + employee.ApellidoMaterno + " " + employee.Nombre;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Estado.
                    worksheet.Cells[row, col].Value = employee.Estado;
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Sueldo diario.
                    worksheet.Cells[row, col].Value = employee.SalarioDiario;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Sueldo base de cotización.
                    worksheet.Cells[row, col].Value = await CalcularSalarioBaseCotizacion(employee, nomina);
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Sueldo diario integrado.
                    worksheet.Cells[row, col].Value = employee.SalarioDiarioIntegrado;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Sueldo base.
                    worksheet.Cells[row, col].Value = percepciones.SingleOrDefault(percepcion => percepcion.Clave.Trim() == "001")?.ImporteGravado;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Ayuda para renta.
                    worksheet.Cells[row, col].Value = percepciones.SingleOrDefault(percepcion => percepcion.Clave.Trim() == "033")?.ImporteExento;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Ayuda para transporte.
                    worksheet.Cells[row, col].Value = percepciones.SingleOrDefault(percepcion => percepcion.Clave.Trim() == "036")?.ImporteExento;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Prima Vacacional.
                    worksheet.Cells[row, col].Value = percepciones.SingleOrDefault(percepcion => percepcion.Clave.Trim() == "021")?.ImporteExento;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Otros.
                    worksheet.Cells[row, col].Value = percepciones.SingleOrDefault(percepcion => percepcion.Clave.Trim() == "038")?.ImporteExento;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Seguro de retiro (WISE).
                    worksheet.Cells[row, col].Value = percepciones.SingleOrDefault(percepcion => percepcion.Clave.Trim() == "024")?.ImporteExento;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Total percepciones.
                    worksheet.Cells[row, col].Value = totalPercepciones;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Ajuste ISR.
                    worksheet.Cells[row, col].Value = employee.AjusteIsr;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // ISR.
                    worksheet.Cells[row, col].Value = deducciones.SingleOrDefault(deduccion => deduccion.Clave.Trim() == "002")?.Importe;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Cuotas obrero patronales.
                    worksheet.Cells[row, col].Value = deducciones.SingleOrDefault(deduccion => deduccion.Clave.Trim() == "021")?.Importe;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Pago por crédito de vivienda.
                    worksheet.Cells[row, col].Value = deducciones.SingleOrDefault(deduccion => deduccion.Clave.Trim() == "010")?.Importe;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Ahorro voluntario.
                    worksheet.Cells[row, col].Value = deducciones.SingleOrDefault(deduccion => deduccion.Clave.Trim() == "023")?.Importe;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Ajuste en seguro de retiro exento.
                    worksheet.Cells[row, col].Value = deducciones.SingleOrDefault(deduccion => deduccion.Clave.Trim() == "048")?.Importe;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Capital.
                    worksheet.Cells[row, col].Value = capital;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Interes.
                    worksheet.Cells[row, col].Value = interes;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Fondo de Garantia.
                    worksheet.Cells[row, col].Value = fg;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Total préstamo.
                    worksheet.Cells[row, col].Value = deducciones.SingleOrDefault(deduccion => deduccion.Clave.Trim() == "004")?.Importe;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Total deducciones.
                    worksheet.Cells[row, col].Value = totalDeducciones;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                    // Sueldo neto.
                    worksheet.Cells[row, col].Value = totalPercepciones - totalDeducciones;
                    worksheet.Cells[row, col].Style.Numberformat.Format = "0.00";
                    worksheet.Column(col).AutoFit();
                    celda = worksheet.Cells[row, col];
                    AplicarEstiloCelda(celda, colorFondo, colorBorde, colorLetra);
                    col++;
                }

                DateTime fechaActual = DateTime.Now;
                string generalName = $@"ReporteDeNomina-" + periodoId + fechaActual.Year + fechaActual.Month + fechaActual.Day + fechaActual.Hour + fechaActual.Second + fechaActual.Millisecond + ".xlsx";
                string baseSrc = $@"StaticFiles\Mate\Excel\ReporteNominas\";
                string disk = $@"C:\";

                package.SaveAs(new FileInfo(disk + baseSrc + generalName));

                sourceFileDto.SourceFile = baseSrc + generalName;

            }

            return new Response<SourceFileDto>(sourceFileDto);
        }

        public async Task<double> CalcularSalarioBaseCotizacion(Employee employee, Nomina nomina)
        {
            int aniosTrabajados = _nominaService.anioEntreDosFechas((DateTime)employee.FechaContrato, nomina.Hasta);
            var vacaciones = await _repositoryAsyncVacacion.FirstOrDefaultAsync(new VacacionByAnioSpecification(aniosTrabajados, employee.CompanyId));
            if (vacaciones == null) throw new ApiException($"La compañía con ID {employee.CompanyId} no cuenta con dias de vacaciones configurados");

            int diasVacaciones = vacaciones.Dias;
            /* Fórmula para el cálculo del salario base de cotización:
            (((Sueldo mensual / 30) * (1 + ((Días de aguinaldo + (Días de vacaciones * Prima vacacional)))/365))) * 0.51)*/
            double salarioBaseCotizacion = ((((double)employee.SalarioMensual / 30) * (1 + ((30 + ((double)diasVacaciones * ((double)employee.PorcentajePrimaVacacional / 100))) / 365))) * 0.51);
            if (salarioBaseCotizacion != null)
            {
                return salarioBaseCotizacion;
            }
            else
            {
                return 0.0;
            }
        }
    }
}
