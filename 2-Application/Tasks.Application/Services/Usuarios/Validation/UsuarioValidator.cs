using Tasks.Application.Services.Usuarios.Dtos;
using Tasks.Data.Context;
using FluentValidation;
using System;
using System.Linq;

namespace Tasks.Application.Services.Usuarios.Validation
{
    public class UsuarioValidator : AbstractValidator<UsuarioDto>
    {
        private TasksContext _context;

        public UsuarioValidator(TasksContext context)
        {
            _context = context;
            RuleFor(c => c.Apelido)
                .NotNull()
                .WithMessage("O Apelido deve ser preenchido")
                .NotEmpty()
                .WithMessage("O Apelido deve ser preenchido")
                .MaximumLength(80)
                .WithMessage("O Apelido deve ter no máximo 80 caracteres");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O Email deve ser preenchido")
                .NotEmpty()
                .WithMessage("O Email deve ser preenchido")
                .MaximumLength(120)
                .WithMessage("O Email deve ter no máximo 120 caracteres")
                .Must((o, email) => EmailJaCadastrado(o.Id, email))
                .WithMessage("Esse e-mail já esta em uso.");

            RuleFor(x => x.Senha)
                .NotNull()
                .WithMessage("A Senha deve ser preenchido")
                .NotEmpty()
                .WithMessage("A Senha deve ser preenchido");
        }

        private bool EmailJaCadastrado(Guid id, string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true;
            return !_context.Usuarios.Any(x => x.Email.ToUpper() == email.ToUpper() && x.Id != id);
        }
    }
}