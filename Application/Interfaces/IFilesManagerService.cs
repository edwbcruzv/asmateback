using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFilesManagerService
    {
        public string saveCompanyCer(IFormFile fileCer, string Rfc);
        public string saveCompanyKey(IFormFile fileKey, string Rfc);
        public string saveCompanyPhoto(IFormFile file, string nombreUser);
        public string saveUserPhoto(IFormFile file, string nombreUser);

        public string saveFacturaPagoPdf(IFormFile file, int Id);
        public string saveComplementoPagoPdf(IFormFile file, int Id);
        public string saveMovimientoReembolsoPdf(IFormFile file, int Id);
        public string saveComprobanteViaticoPdf(IFormFile file, int Id);
        public string saveReembolsoPdf(IFormFile file, int Id);
        public string saveIncidenciaPDF(IFormFile file, int Id);

        public string saveComprobanteViaticoXml(IFormFile file, int Id);
        public string saveMovimientoReembolsoXml(IFormFile file, int Id);

        public string saveTicketImage(IFormFile image, int ticket_id);

        public string saveFileInTo(IFormFile file, int Id, string folder_name, string source_path, string extension);
        public bool DeleteFile(string pathFile);
        public bool UpdateFile(IFormFile newFile, string pathFileExist);
    }
}
