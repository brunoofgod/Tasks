using FluentValidation;
using System;
using Tasks.Application.Services.Tasks.Dtos;

namespace Tasks.Application.Services.Tasks.Validation
{
    public class TaskValidator : AbstractValidator<TaskDto>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Descricao)
                .MaximumLength(500)
                .WithMessage("A descricao deve conter no maximo 500 caracteres");

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("o titulo deve ser preenchido")
                .NotNull()
                .WithMessage("o titulo deve ser preenchido")
                .MaximumLength(150)
                .WithMessage("o titulo deve conter no maximo 150 caracteres");

        }

    }
}