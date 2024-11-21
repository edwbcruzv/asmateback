﻿using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Departamentos.Commands.UpdateDepartamento
{
    public class UpdateDepartamentoCommandValidator : AbstractValidator<UpdateDepartamentoCommand>
    {
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        public UpdateDepartamentoCommandValidator(IRepositoryAsync<Company> repositoryAsyncDepartamento)
        {
            _repositoryAsyncCompany = repositoryAsyncDepartamento;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("El CompanyId es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncCompany.GetByIdAsync(CompanyId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El CompanyId no existe");
        }
    }
}
