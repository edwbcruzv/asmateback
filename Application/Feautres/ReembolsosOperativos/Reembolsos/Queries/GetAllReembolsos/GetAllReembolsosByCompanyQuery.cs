using Application.DTOs.ReembolsosOperativos;
using Application.Interfaces;
using Application.Specifications.ReembolsosOperativos.MovimientoReembolsos;
using Application.Specifications.ReembolsosOperativos.Reembolsos;
using Application.Specifications.Users;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetAllReembolsos
{
    public class GetAllReembolsosByCompanyQuery : IRequest<Response<List<ReembolsoDTO>>>
    {
        public int CompanyId { set; get; }

        public class Handler : IRequestHandler<GetAllReembolsosByCompanyQuery, Response<List<ReembolsoDTO>>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsync;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<User> _repositoryAsyncUser;
            private readonly IReembolsoService _reembolsoService;


            public Handler(
                IRepositoryAsync<Reembolso> repositoryAsync,
                IRepositoryAsync<Company> repositoryAsyncCompany,
                IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
                IMapper mapper,
                IRepositoryAsync<User> repositoryAsyncUser,
                IReembolsoService reembolsoService)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
                _mapper = mapper;
                _repositoryAsyncUser = repositoryAsyncUser;
                _reembolsoService = reembolsoService;
            }

            public async Task<Response<List<ReembolsoDTO>>> Handle(GetAllReembolsosByCompanyQuery request, CancellationToken cancellationToken)
            {
                var company = await _repositoryAsyncCompany.GetByIdAsync(request.CompanyId);
                // var comp = await ObtenerRFCCompania();

                var usuarios = await _repositoryAsyncUser.ListAsync();
                Dictionary<int, string> diccionarioUsuarios = usuarios.ToDictionary(u => u.Id, u => u.UserName);

                if (company == null)
                {
                    throw new ApplicationException($"No se encontró la compañía con el Id {request.CompanyId}.");
                }
                else
                {
                    var list = await _repositoryAsync.ListAsync(new ReembolsoByCompanySpecification(request.CompanyId));
                    
                    //List<ReembolsoDTO> list_reembolso_dto = new List<ReembolsoDTO>();

                    var list_reembolso_dto = _mapper.Map<List<ReembolsoDTO>>(list);

                    foreach (ReembolsoDTO reembolso_dto in list_reembolso_dto)
                    {
                        
                        reembolso_dto.Monto = await _reembolsoService.CalcularMontoTotalReembolso(reembolso_dto.Id);
                        
                        reembolso_dto.UsuarioName = diccionarioUsuarios[(int)reembolso_dto.UsuarioIdPago];

                        reembolso_dto.CompanyRFC = company.Rfc;

                        if (reembolso_dto.SrcPdfPagoComprobante != null)
                        {
                            reembolso_dto.SrcPdfPagoComprobante = reembolso_dto.SrcPdfPagoComprobante.Split(@"C:\").Last();
                        }
                          
                    }

                    
                    /*foreach (var reembolso in list)
                    {
                        Console.WriteLine(reembolso.EstatusId);
                        var reembolsoDto = new ReembolsoDTO
                        {
                            Id = reembolso.Id,
                            Descripcion = reembolso.Descripcion,
                            Clabe = reembolso.Clabe,
                            //Estatus = reembolso.EstatusId.ToString(),
                            FechaPago = reembolso.FechaPago,
                            CompanyRFC = company.Rfc,
                            CreatedBy = reembolso.CreatedBy,
                            Created = (DateTime)reembolso.Created,
                            UsuarioIdPago = reembolso.UsuarioIdPago,
                            Monto = await _reembolsoService.CalcularMontoTotalReembolso(reembolso.Id),
                            UsuarioName = diccionarioUsuarios[(int)reembolso.UsuarioIdPago]
                        };

                        list_reembolso_dto.Add(reembolsoDto);
                    }*/

                    return new Response<List<ReembolsoDTO>>(list_reembolso_dto);
                }
            }

            /*private async Task<Dictionary<int, string>> ObtenerRFCCompania()
            {
                var companies = await _repositoryAsyncCompany.ListAsync();

                return companies.ToDictionary(c => c.Id, c => c.Rfc);
            }*/
        }

    }
}
