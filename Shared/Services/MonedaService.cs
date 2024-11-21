using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Services
{
    public class MonedaService : IMonedaService
    {
        private static string[] unidades_es = { "", "UNO", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE" };
        private static string[] especiales_es = { "", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE" };
        private static string[] decenas_es = { "", "DIEZ", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };
        private static string[] centenas_es = { "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" };


        public string ConvertNumberToString(double numero)
        {
            string cantidadEnLetras = "";

            // Parte entera
            long parteEntera = (long)numero;
            cantidadEnLetras += ConvertirParteEnteraALetras(parteEntera);

            // Parte decimal (centavos)
            int parteDecimal = (int)Math.Round((numero - parteEntera) * 100);
            if (parteDecimal > 0)
            {
                cantidadEnLetras += " CON " + parteDecimal.ToString("00") + "/100 M.N.";
            }
            else
            {
                cantidadEnLetras += " CON CERO CENTAVOS M.N.";
            }

            return cantidadEnLetras.ToUpper();
        }

        private string ConvertirParteEnteraALetras(long numero)
        {
            if (numero == 0)
            {
                return "CERO";
            }

            if (numero < 0)
            {
                return "MENOS " + ConvertirParteEnteraALetras(-numero);
            }

            string cantidadEnLetras = "";

            if ((numero / 1000000) > 0)
            {
                cantidadEnLetras += ConvertirParteEnteraALetras(numero / 1000000) + " MILLONES ";
                numero %= 1000000;
            }

            if ((numero / 1000) > 0)
            {
                cantidadEnLetras += ConvertirParteEnteraALetras(numero / 1000) + " MIL ";
                numero %= 1000;
            }

            if ((numero / 100) > 0)
            {
                cantidadEnLetras += centenas_es[numero / 100] + " ";
                numero %= 100;
            }

            if (numero > 0)
            {
                if (numero >= 20)
                {
                    cantidadEnLetras += decenas_es[numero / 10] + " ";
                    numero %= 10;
                    if (numero > 0)
                    {
                        cantidadEnLetras += unidades_es[numero] + " ";
                    }
                }
                else if (numero >= 10)
                {
                    cantidadEnLetras += especiales_es[numero - 10] + " ";
                }
                else
                {
                    cantidadEnLetras += unidades_es[numero] + " ";
                }
            }

            return cantidadEnLetras;
        }
    }
}
