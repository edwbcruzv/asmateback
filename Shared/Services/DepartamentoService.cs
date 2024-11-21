using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class DepartamentoService: IDepartamentoService
    {

        public string GenerateClave(string company_name, string depto_name, int depto_id)
        {
            string iniciales_company = ExtraerIniciales(company_name);

            while (iniciales_company.Length < 3)
            {
                iniciales_company += "0";
            }

            iniciales_company = iniciales_company.Substring(0, 3);

            string iniciales_depto = ExtraerIniciales(depto_name);

            while (iniciales_depto.Length < 2)
            {
                iniciales_depto += "0";
            }

            iniciales_depto = iniciales_depto.Substring(0, 2);

            return iniciales_company + iniciales_depto + AgregarCeros(depto_id);
        }

        private string ExtraerIniciales(string frase)
        {
            string[] palabras = frase.Split(' ');
            string iniciales = "";

            foreach (string palabra in palabras)
            {
                if (!string.IsNullOrWhiteSpace(palabra))
                {
                    iniciales += palabra.Substring(0, 1).ToUpper();
                }
            }

            return iniciales;
        }

        private string AgregarCeros(int numero)
        {
            // Convertimos el número a una cadena y luego utilizamos PadLeft para agregar ceros
            return numero.ToString().PadLeft(3, '0');
        }
    }
}
