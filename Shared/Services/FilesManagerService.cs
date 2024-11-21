using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class FilesManagerService : IFilesManagerService
    {

        /******************** Funciones Auxiliares*********************/
        private string GenerateFileName(string baseName, string extension)
        {
            var dia = DateTime.Now;
            
            var fileName = dia.Day.ToString() + dia.Month.ToString() + dia.Year.ToString()
                        + dia.Minute.ToString() + dia.Millisecond.ToString() + extension;
            return string.IsNullOrEmpty(baseName) ? fileName : $"{baseName}-{fileName}";
        }

        private string PrepareSavePath(string folderName)
        {
            var pathToSave = Path.Combine("C:", folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            return pathToSave;
        }

        private string SaveFile(IFormFile file, string pathToSave, string fileName)
        {
            if (file.Length <= 0)
            {
                return null;
            }

            var fullPath = Path.Combine(pathToSave, fileName);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fullPath.Substring(3); // quita el C: del principio ;)
        }

        public bool DeleteFile(string pathFile)
        {
            pathFile = "C:\\" + pathFile;

            try
            {
                // Verifica si el archivo existe antes de intentar eliminarlo
                if (File.Exists(pathFile))
                {
                    // Elimina el archivo
                    File.Delete(pathFile);
                    Console.WriteLine($"Archivo {pathFile} eliminado con éxito.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"El archivo {pathFile} no existe.");
                    return false;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al intentar eliminar el archivo: {ex.Message}");
                return false;
            }
        }

        public bool UpdateFile(IFormFile newFile, string pathFileExist)
        {
            pathFileExist = "C:\\" + pathFileExist;

            try
            {
                // Verifica si el archivo existente existe antes de intentar actualizarlo
                if (File.Exists(pathFileExist))
                {
                    using (var streamReader = newFile.OpenReadStream())
                    using (var streamWriter = new FileStream(pathFileExist, FileMode.Create))
                    {
                        // Copia el contenido del nuevo archivo al archivo existente
                        streamReader.CopyTo(streamWriter);
                    }

                    Console.WriteLine($"Archivo actualizado con éxito.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"El archivo existente {pathFileExist} no existe.");
                    return false;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al intentar actualizar el archivo: {ex.Message}");
                return false;
            }
        }
        /**************************************************************/

        public string saveCompanyCer(IFormFile fileCer, string Rfc)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "CSD", Rfc);
                var pathToSave = PrepareSavePath(folderName);
                var nameFile = GenerateFileName(null, ".cer");
                return SaveFile(fileCer, pathToSave, nameFile);
            }
            catch (Exception es)
            {
                return null;
            }
        }

        public string saveCompanyKey(IFormFile fileKey, string Rfc)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "CSD", Rfc);
                var pathToSave = PrepareSavePath(folderName);
                CreateDirectoryIfNeeded(pathToSave);
                var fileName = GenerateFileName(DateTime.Now.ToString("ddMMyyyymmss"), ".key");
                return SaveFile(fileKey, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        // Aquí está la nueva función auxiliar
        private void CreateDirectoryIfNeeded(string pathToSave)
        {
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
        }

        public string saveCompanyPhoto(IFormFile file, string nombreUser)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "Companies");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(nombreUser, ".png");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveUserPhoto(IFormFile file, string nombreUser)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "Users");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(nombreUser, ".png");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveFacturaPagoPdf(IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosPDF\\FacturasPago");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(Id.ToString(), ".pdf");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }  

        public string saveComplementoPagoPdf(IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosPDF\\ComplementosPago");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(Id.ToString(), ".pdf");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveComprobanteViaticoPdf(IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosPDF\\ComprobantesViatico");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(Id.ToString(), ".pdf");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveMovimientoReembolsoPdf(IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosPDF\\MovimientosReembolso");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(Id.ToString(), ".pdf");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveReembolsoPdf(IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosPDF\\Reembolsos");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName("ComprobantePagoReembolso" + Id.ToString(), ".pdf");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveMovimientoReembolsoXml(IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosXML\\MovimientosReembolso");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(Id.ToString(), ".xml");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveComprobanteViaticoXml(IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosXML\\ComprobantesViatico");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(Id.ToString(), ".xml");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        public string saveIncidenciaPDF (IFormFile file, int Id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate", "FormatosPDF\\IncidenciasFirmadas");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName("Incidencia" + Id.ToString(), ".pdf");
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }

        }


        public string saveFileInTo(IFormFile file, int Id,string folder_name,string source_path,string extension)
        {
            try
            {
                var folderName = Path.Combine(folder_name, source_path);
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(Id.ToString(), extension);
                return SaveFile(file, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }

        

        public string saveTicketImage(IFormFile image, int ticket_id)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles\\Mate\\Kanban\\Tickets", "Images");
                var pathToSave = PrepareSavePath(folderName);
                var fileName = GenerateFileName(ticket_id.ToString(), Path.GetExtension(image.FileName));
                return SaveFile(image, pathToSave, fileName);
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
                return null;
            }
        }
    }
}
