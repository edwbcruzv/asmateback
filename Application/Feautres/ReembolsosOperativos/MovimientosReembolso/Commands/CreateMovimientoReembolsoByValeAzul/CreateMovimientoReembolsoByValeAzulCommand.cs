using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByValeAzul
{
    public class CreateMovimientoReembolsoByValeAzulCommand : IRequest<Response<int>>
    {

        public DateTime FechaMovimiento { get; set; }
        public string EmisorNombre { get; set; }
        public double Total { get; set; }
        public int MetodoPagoId { get; set; }
        public string Concepto { get; set; }

        public int ReembolsoId { get; set; }

        public class Handler : IRequestHandler<CreateMovimientoReembolsoByValeAzulCommand, Response<int>>
        {
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
                IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
                IMapper mapper)
            {
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateMovimientoReembolsoByValeAzulCommand request, CancellationToken cancellationToken)
            {
                var nuevo_mov_reembolso = _mapper.Map<MovimientoReembolso>(request);

                try
                {
                    nuevo_mov_reembolso.TipoReembolsoId = 2;


                    //Guardando en la db el nuevo movimiento
                    nuevo_mov_reembolso.TipoMonedaId = 115;
                    nuevo_mov_reembolso.TipoCambio = 1.0;
                    var data = await _repositoryAsyncMovimientoReembolso.AddAsync(nuevo_mov_reembolso);

                    
                    return new Response<int>(data.Id);
                }
                catch (Exception ex)
                {
                    return new Response<int>("Error: " + ex.Message);
                }
            }
        }

    }
}
