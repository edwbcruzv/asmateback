using FluentValidation;

namespace Application.Feautres.Facturacion.FacturaMovimientos.Commands.DeleteFacturaMovimientoCommand
{
    public class DeleteFacturaMovimientoCommandValidator : AbstractValidator<DeleteFacturaMovimientoCommand>
    {
        public DeleteFacturaMovimientoCommandValidator()
        {
            RuleFor(p => p.Id)
                 .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
