using Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.DeleteComplementoPagoFacturaCommand;
using Application.Wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.DeleteReembolsoCommand
{
    public class DeleteReembolsoCommandValidator : AbstractValidator<DeleteReembolsoCommand>
    {
        public DeleteReembolsoCommandValidator()
        {
            
        }

    }
}
