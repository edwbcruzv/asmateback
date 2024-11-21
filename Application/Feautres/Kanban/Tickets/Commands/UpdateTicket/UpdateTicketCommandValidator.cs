using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketCommandValidator()
        {
            RuleFor(f => f.Estatus)
                .Must(Estatus => Estatus >= 0 && Estatus <= 2)
                .WithMessage("El estatus recibido no está en el rango [0, 2]");

        }
    }
}
