using FluentValidation;

namespace Application.Feautres.Administracion.SubMenus.Commands.DeleteSubMenuCommand
{
    public class DeleteSubMenuCommandValidator : AbstractValidator<DeleteSubMenuCommand>
    {
        public DeleteSubMenuCommandValidator()
        {
            RuleFor(p => p.Id)
                 .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
