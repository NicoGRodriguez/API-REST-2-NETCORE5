using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe extender de {MaxLength} caracteres.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe extender de {MaxLength} caracteres.");

            RuleFor(p => p.birthdate)
                .NotEmpty().WithMessage("La Fecha de Nacimento no puede estar vacio.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .Matches(@"^\d{3}-\d{6}$").WithMessage("{PropertyName} debe cumplir el formato 000-000000")
                .MaximumLength(10).WithMessage("{PropertyName} no debe extender de {MaxLength} caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
                .MaximumLength(100).WithMessage("{PropertyName} no debe extender de {MaxLength} caracteres.");

            RuleFor(p => p.Adress)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(120).WithMessage("{PropertyName} no debe extender de {MaxLength} caracteres.");
        }
    }
}
