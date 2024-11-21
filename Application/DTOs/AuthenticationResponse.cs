using Application.DTOs.Administracion;
using Application.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int EmployeeId { get; set; }
        public string NickName { get; set; }
        public string UserProfile { get; set; }
        public string UserEmail { get; set; }

        //Elementos del inicio de sesión.
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public string Rol { get; set; }

        //Elementos de MenuUsuario
        public ICollection<MenuUserRelationDTO> MenuUserRelation { get; set; }

        ////Elementos Submenu
        public ICollection<SubMenuUserRelationDTO> SubMenuUserRelation { get; set; }

        ////Elementos de compañias Vinculadas
        public ICollection<CompanyDTO> CompanyUserRelation { get; set; }
    }
}
