using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Companies.Commands.UpdateCompanyCommand
{
    public class UpdateCompanyCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SalaryDays { get; set; }
        public string CompanyStatus { get; set; }
        public string? CompanyProfile { get; set; }
        public string? RegistroPatronal { get; set; }
        public string PostalCode { get; set; }
        public int RegimenFiscalId { get; set; }
        public string? RepresentanteLegal { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string? Certificado { get; set; }
        public string? PrivateKeyFile { get; set; }
        public string? PassPrivateKey { get; set; }
        public string? Calle { get; set; }
        public string? NoExt { get; set; }
        public string? NoInt { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
        public IFormFile? File { get; set; }
        public IFormFile? FileCer { get; set; }
        public IFormFile? FileKey { get; set; }
    }
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Company> _repositoryAsync;
        private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
        private readonly IMapper _mapper;
        private readonly IFilesManagerService _filesManagerService;


        public UpdateCompanyCommandHandler(IRepositoryAsync<Company> repositoryAsync, IMapper mapper, 
            IFilesManagerService filesManagerService, IRepositoryAsync<Factura> repositoryAsyncFactura)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _filesManagerService = filesManagerService;
            _repositoryAsyncFactura = repositoryAsyncFactura;
        }

        public async Task<Response<int>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _repositoryAsync.GetByIdAsync(request.Id);

            if (company == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                
                if (request.FileCer?.Length > 0)
                    company.Certificado = _filesManagerService.saveCompanyCer(request.FileCer, request.Rfc);

                if (request.FileKey?.Length > 0)
                    company.PrivateKeyFile = _filesManagerService.saveCompanyKey(request.FileKey, request.Rfc);

                if (request.File?.Length > 0)
                    company.CompanyProfile = _filesManagerService.saveCompanyPhoto(request.File, request.Rfc);

                company.Name = request.Name;
                company.Description = request.Description;
                company.SalaryDays = request.SalaryDays;
                company.CompanyStatus = request.CompanyStatus; 
                company.RegistroPatronal = request.RegistroPatronal;
                company.PostalCode = request.PostalCode;
                company.RegimenFiscalId = request.RegimenFiscalId;
                company.RepresentanteLegal = request.RepresentanteLegal;
                company.Rfc = request.Rfc;
                company.RazonSocial = request.RazonSocial;           
                company.PassPrivateKey = request.PassPrivateKey;
                company.Calle = request.Calle;
                company.NoExt = request.NoExt;
                company.NoInt = request.NoInt;
                company.Colonia = request.Colonia;
                company.Municipio = request.Municipio;
                company.Estado = request.Estado;
                company.Pais = request.Pais;

                await _repositoryAsync.UpdateAsync(company);

                var facturas = await _repositoryAsyncFactura.ListAsync(new FacturaByCompanyAndTrueSpecification(request.Id));

                foreach (Factura factura in facturas)
                {
                    factura.EmisorRfc = company.Rfc;
                    factura.EmisorRazonSocial = company.RazonSocial;
                    factura.LugarExpedicion = company.PostalCode;
                    factura.EmisorRegimenFiscalId = company.RegimenFiscalId;
                    factura.LogoSrcCompany = company.CompanyProfile;

                    await _repositoryAsyncFactura.UpdateAsync(factura);
                }

                return new Response<int>(company.Id);
            }
        }
    }
}
