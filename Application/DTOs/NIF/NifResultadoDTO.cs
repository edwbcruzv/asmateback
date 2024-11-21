using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.NIF
{
    public class NifResultadoDTO
    {
        public string? Rfc { get; set; }
        public int IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public double SueldoDiario { get; set; }
        public double SueldoBase { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int diasVacciones { get; set; }
        public int diasAguinaldo { get; set; }
        public double SBC { get; set; }
        public double PorcentajePrimaVacacional { get; set; }
        public double Isr { get; set; }
        public double CuotasPatronales { get; set; }
        public double PrimaVacacional { get; set; }
        public double Aguinaldo { get; set; }
        public double Subsidio { get; set; }
        public double Vacaciones { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
        public double Rcv { get; set; }
        public double Infonavit { get; set; }
        public double PrimaAntiguedad { get; set; }
    }
}
