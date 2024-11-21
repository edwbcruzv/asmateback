using Application.DTOs.ReembolsosOperativos;
using Application.Interfaces;
using Application.Specifications.ReembolsosOperativos.MovimientoReembolsos;
using Application.Specifications.ReembolsosOperativos.Reembolsos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Queries.GetReembolsoByUsername
{
    public class GetAllReembolsosByUsernameQuery : IRequest<Response<List<ReembolsoDTO>>>
    {
        public int UserId { get; set; }

        public class Handler : IRequestHandler<GetAllReembolsosByUsernameQuery, Response<List<ReembolsoDTO>>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IRepositoryAsync<User> _repositoryAsyncUser;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IMapper _mapper;
            private readonly IReembolsoService _reembolsoService;

            public Handler(
                IRepositoryAsync<Reembolso> repositoryAsync,
                IMapper mapper,
                IRepositoryAsync<User> repositoryAsyncUser,
                IRepositoryAsync<Company> repositoryAsyncCompany,
                IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
                IReembolsoService reembolsoService)
            {
                _repositoryAsyncReembolso = repositoryAsync;
                _repositoryAsyncUser = repositoryAsyncUser;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
                _mapper = mapper;
                _reembolsoService = reembolsoService;
            }

            public async Task<Response<List<ReembolsoDTO>>> Handle(GetAllReembolsosByUsernameQuery request, CancellationToken cancellationToken)
            {
                var user = await _repositoryAsyncUser.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new ApplicationException($"No se encontró el usuario con el Id {request.UserId}.");
                }
                else
                {
                    var comp = await ObtenerRFCCompania();
                    var list_reembolso = await _repositoryAsyncReembolso.ListAsync(new ReembolsoByNickNameSpecification(user.NickName));

                    // List<ReembolsoDTO> list_reembolso_dto = new List<ReembolsoDTO>();

                    var list_reembolso_dto = _mapper.Map<List<ReembolsoDTO>>(list_reembolso);

                    foreach (ReembolsoDTO reembolso_dto in list_reembolso_dto)
                    {
                        reembolso_dto.Monto = await _reembolsoService.CalcularMontoTotalReembolso(reembolso_dto.Id);

                        reembolso_dto.CompanyRFC = comp[reembolso_dto.CompanyId];

                        reembolso_dto.UsuarioName = user.NickName;

                        if (reembolso_dto.SrcPdfPagoComprobante != null)
                        {
                            reembolso_dto.SrcPdfPagoComprobante = reembolso_dto.SrcPdfPagoComprobante.Split(@"C:\").Last();
                        }
                    }

                    /*foreach (var reembolso in list_reembolso)
                    {
                        var reembolsoDto = new ReembolsoDTO
                        {
                            Id = reembolso.Id,
                            Descripcion = reembolso.Descripcion,
                            Clabe = reembolso.Clabe,
                            //Estatus = reembolso.EstatusId.ToString(),
                            Monto = await _reembolsoService.CalcularMontoTotalReembolso(reembolso.Id),
                            FechaPago = reembolso.FechaPago,
                            CompanyRFC = comp[reembolso.CompanyId],
                            UsuarioIdPago = reembolso.UsuarioIdPago,
                            CreatedBy = reembolso.CreatedBy,
                            Created = (DateTime)reembolso.Created,
                            UsuarioName = user.NickName
                        };

                        list_reembolso_dto.Add(reembolsoDto);
                    }*/

                    return new Response<List<ReembolsoDTO>>(list_reembolso_dto);
                }
            }

            private async Task<Dictionary<int, string>> ObtenerRFCCompania()
            {
                var companies = await _repositoryAsyncCompany.ListAsync();

                return companies.ToDictionary(c => c.Id, c => c.Rfc);
            }
        }
    }
}


