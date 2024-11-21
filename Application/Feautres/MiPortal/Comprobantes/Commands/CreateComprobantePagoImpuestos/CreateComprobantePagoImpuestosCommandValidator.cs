using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobantePagoImpuestos
{
    internal class CreateComprobantePagoImpuestosCommandValidator : AbstractValidator<CreateComprobantePagoImpuestosCommand>
    {
        private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;

        public CreateComprobantePagoImpuestosCommandValidator(IRepositoryAsync<Viatico> repositoryAsyncViatico)
        {
            _repositoryAsyncViatico = repositoryAsyncViatico;

            RuleFor(x => x.ViaticoId)
                .NotEmpty().WithMessage("El id viatico es oblicatorio es obligatorio")
                .MustAsync(async (ViaticoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncViatico.GetByIdAsync(ViaticoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El viatico no existe");
        }
    }
}
