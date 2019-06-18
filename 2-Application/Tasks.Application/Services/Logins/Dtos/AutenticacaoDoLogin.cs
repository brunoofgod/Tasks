using System;

namespace Tasks.Application.Services.Logins.Dtos
{
    public class AutenticacaoDoLogin
    {
        public bool Autenticado { get; set; }
        public string DataDeCriacao { get; set; }
        public string DataDeExpiracao { get; set; }
        public string Mensagem { get; set; }
        public string TokenDeAcesso { get; set; }
        public string Usuario { get; set; }
        public Guid UsuarioId { get; set; }
    }
}