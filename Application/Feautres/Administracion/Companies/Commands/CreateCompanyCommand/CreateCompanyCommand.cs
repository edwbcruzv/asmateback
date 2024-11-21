using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Companies.Commands.CreateCompanyCommand
{
    public class CreateCompanyCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SalaryDays { get; set; }
        public string? CompanyProfile { get; set; }
        public string CompanyStatus { get; set; }
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
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Company> _repositoryAsync;
        private readonly IMapper _mapper;
        private readonly IFilesManagerService _filesManagerService;

        public CreateCompanyCommandHandler(IRepositoryAsync<Company> repositoryAsync, IMapper mapper, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _filesManagerService = filesManagerService;
        }

        public async Task<Response<int>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {

            if (request.FileCer?.Length > 0)
                request.Certificado = _filesManagerService.saveCompanyCer(request.FileCer, request.Rfc);

            if (request.FileKey?.Length > 0)
                request.PrivateKeyFile = _filesManagerService.saveCompanyKey(request.FileKey, request.Rfc);

            if (request.File?.Length > 0)
                request.CompanyProfile = _filesManagerService.saveCompanyPhoto(request.File, request.Rfc);

            var nuevoregistro = _mapper.Map<Company>(request);

            var data = await _repositoryAsync.AddAsync(nuevoregistro);

            return new Response<int>(data.Id);

        }
    }
}
