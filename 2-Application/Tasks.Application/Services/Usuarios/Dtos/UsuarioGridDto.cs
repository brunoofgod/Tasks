using System;

namespace Tasks.Application.Services.Agendamentos.Dtos
{
    public class UsuarioGridDto
    {
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}