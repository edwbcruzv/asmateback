using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolso
{
    public class CreateMovimientoReembolsoFacturaCommandValidator : AbstractValidator<CreateMovimientoReembolsoFacturaCommand>
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
        private readonly IRepositoryAsync<TipoReembolso> _repositoryAsyncTipoReembolso;

        public CreateMovimientoReembolsoFacturaCommandValidator(
            IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
            IRepositoryAsync<TipoReembolso> repositoryAsyncTipoReembolso
            )
        {
            _repositoryAsyncReembolso = repositoryAsyncReembolso;
            _repositoryAsyncTipoReembolso = repositoryAsyncTipoReembolso;

            RuleFor(x => x.ReembolsoId)
               .MustAsync(async (ReembolsoId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncReembolso.GetByIdAsync(ReembolsoId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage("El campo ReembolsoId no es valido.");

            RuleFor(r => r.Concepto)
                .NotEmpty()
                .WithMessage("El concepto es obligatorio.")
                .MaximumLength(200)
                .WithMessage("Se supero los 200 caracteres.");

            RuleFor(x => x.PDFMovReembolso)
                .NotNull()
                .WithMessage("El archivo PDF de reembolso es requerido.");

            RuleFor(x => x.XMLMovReembolso)
                .NotNull()
                .WithMessage("El archivo XML de reembolso es requerido.");

        }
    }
}
