using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Domain.Entities;

namespace Application.Feautres.Catalogos.Estados.Queries.GetAllBancos
{
    public class GetAllEstadosQuery : IRequest<Response<IEnumerable<Estado>>>
    {
        private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;
        public class Handler : IRequestHandler<GetAllEstadosQuery, Response<IEnumerable<Estado>>>
        {
            private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;

            public Handler(IRepositoryAsync<Estado> repositoryAsyncEstado)
            {
                _repositoryAsyncEstado = repositoryAsyncEstado;
            }

            public async Task<Response<IEnumerable<Estado>>> Handle(GetAllEstadosQuery request, CancellationToken cancellationToken)
            {
                var listaEstados = await _repositoryAsyncEstado.ListAsync();

                return new Response<IEnumerable<Estado>>(listaEstados);
            }
        }
    }
}
