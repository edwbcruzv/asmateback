using FluentValidation;

namespace Application.Feautres.Facturacion.Facturas.Commands.DeleteFacturaCommand
{
    public class DeleteFacturaCommandValidator : AbstractValidator<DeleteFacturaCommand>
    {
        public DeleteFacturaCommandValidator()
        {
            RuleFor(p => p.Id)
                 .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
