using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Users;
using Application.Specifications.ContracsUsers;
using Application.Specifications.MenuUser;
using Application.Specifications.SubMenu;
using Application.Wrappers;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.DTOs.Facturas;
using Application.DTOs.Administracion;
using Application.DTOs.Usuarios;
using Application.Specifications.Employees;

namespace Identity.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IRepositoryAsync<User> _repositoryAsyncUser;
        private readonly IRepositoryAsync<MenuUserSelector> _repositoryAsyncMenuUser;
        private readonly IRepositoryAsync<Menu> _repositoryAsyncMenu;
        private readonly IRepositoryAsync<SubMenuUserSelector> _repositoryAsyncSubMenuUser;
        private readonly IRepositoryAsync<SubMenu> _repositoryAsyncSubMenu;
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<ContractsUserCompany> _repositoryAsyncCompanyUser;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
        private readonly IMapper _mapper;
        private readonly IRsa _rsa;
        private string secretKey;
        private string audienceToken;
        private string issuerToken;

        public AuthenticateService(IRepositoryAsync<User> repositoryAsyncUser, IMapper mapper, IConfiguration configuration,
            IRepositoryAsync<MenuUserSelector> repositoryAsyncMenuUser, IRepositoryAsync<Menu> repositoryAsyncMenu,
            IRepositoryAsync<SubMenuUserSelector> repositoryAsyncSubMenuUser, IRepositoryAsync<SubMenu> repositoryAsyncSubMenu,
            IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<ContractsUserCompany> repositoryAsyncCompanyUser,
            IRsa rsa,
            IRepositoryAsync<Employee> repositoryAsyncEmployee)
        {
            secretKey = configuration["JWTSettings:JWT_Secret"];
            audienceToken = configuration["JWTSettings:JWT_AUDIENCE_TOKEN"];
            issuerToken = configuration["JWTSettings:JWT_ISSUER_TOKEN"];
            _repositoryAsyncUser = repositoryAsyncUser;
            _mapper = mapper;
            _repositoryAsyncMenuUser = repositoryAsyncMenuUser;
            _repositoryAsyncMenu = repositoryAsyncMenu;
            _repositoryAsyncSubMenuUser = repositoryAsyncSubMenuUser;
            _repositoryAsyncSubMenu = repositoryAsyncSubMenu;
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncCompanyUser = repositoryAsyncCompanyUser;
            _rsa = rsa;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(string NickName, string password)
        {
            var user = await _repositoryAsyncUser.FirstOrDefaultAsync(new UserByNickNameSpecification(NickName));

            if (user == null)
            {
                throw new ApiException($"No hay una cuenta registrada con el nickname ${NickName}.");
            }

            if (!password.Equals(_rsa.Dencript(user.UserPassword))) {
                throw new ApiException($"Credenciales invalidas para ${NickName}.");
            }

            var menuUserSelector = await _repositoryAsyncMenuUser.ListAsync(new MenuUserSelectorByUserSpecification(user.Id));
            var subMenuUserSelector = await _repositoryAsyncSubMenuUser.ListAsync(new SubMenuUserSelectorByUserSpecification(user.Id));
            var companyUserSelector = await _repositoryAsyncCompanyUser.ListAsync(new ContractsUserCompanyByUserSpecification(user.Id));
            var employee = await _repositoryAsyncEmployee.FirstOrDefaultAsync(new EmployeeByUserIdSpecification(user.Id));
            var menuUserList = new List<MenuUserRelationDTO>();
            var subMenuUserList = new List<SubMenuUserRelationDTO>();
            var companyUserList = new List<CompanyDTO>();


            foreach ( var item in menuUserSelector )
            {
                var menu = _repositoryAsyncMenu.GetByIdAsync(item.MenuId);
                var tempMenuUser = new MenuUserRelationDTO();

                tempMenuUser.Id = item.Id;
                tempMenuUser.UserId = item.UserId;
                tempMenuUser.MenuId = item.MenuId;
                tempMenuUser.MenuName = menu.Result.MenuName;
                tempMenuUser.MenuSource = menu.Result.MenuSource;
                tempMenuUser.MenuIcon = menu.Result.MenuIcon;
                tempMenuUser.MenuOrder = menu.Result.MenuOrder;

                menuUserList.Add(tempMenuUser);

            }

            foreach (var item in subMenuUserSelector)
            {
                var subMenu = _repositoryAsyncSubMenu.GetByIdAsync(item.SubMenuId);
                var tempSubMenuUser = new SubMenuUserRelationDTO();

                tempSubMenuUser.Id = item.Id;
                tempSubMenuUser.UserId = item.UserId;
                tempSubMenuUser.SubMenuId = item.SubMenuId;
                tempSubMenuUser.MenuId = subMenu.Result.MenuId;
                tempSubMenuUser.SubMenuName = subMenu.Result.SubMenuName;
                tempSubMenuUser.SubMenuSource = subMenu.Result.SubMenuSource;
                tempSubMenuUser.SubMenuIcon = subMenu.Result.SubMenuIcon;
                tempSubMenuUser.SubMenuOrder = subMenu.Result.SubMenuOrder;

                subMenuUserList.Add(tempSubMenuUser);

            }

            foreach (var item in companyUserSelector)
            {
                var company = _repositoryAsyncCompany.GetByIdAsync(item.CompanyId);
                var tempCompany = new CompanyDTO();

                tempCompany.Id = company.Result.Id;
                tempCompany.Name = company.Result.Name;
                tempCompany.Description = company.Result.Description;
                tempCompany.RazonSocial = company.Result.RazonSocial;

                companyUserList.Add(tempCompany);

            }

            AuthenticationResponse response = new AuthenticationResponse();

            response.Id = user.Id;
            response.UserName = user.UserName;
            response.EmployeeId = employee.Id;
            response.NickName = NickName;
            response.UserProfile = user.UserProfile.Replace("\\", "/");
            response.UserEmail = user.UserEmail;
            response.Rol = user.UserType == 3 ? "Super" : user.UserType == 2 ? "Admin" : "Comun";
            DateTime ExpiresDate;
            response.Token = createToken(response.NickName, response.Rol, out ExpiresDate);
            response.ExpiresDate = ExpiresDate;
            response.MenuUserRelation = menuUserList;
            response.SubMenuUserRelation = subMenuUserList;
            response.CompanyUserRelation = companyUserList;


            return new Response<AuthenticationResponse>(response);

        }

        private string createToken(string nickName, string rol, out DateTime ExpirateDate)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, nickName),
                new Claim(ClaimTypes.Role, rol)
            });

            ExpirateDate = DateTime.UtcNow.AddDays(1);

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.Now,
                expires: ExpirateDate,
                signingCredentials: signingCredentials);

            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
