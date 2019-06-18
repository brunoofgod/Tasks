using Tasks.Application.Dtos_Genericos;

namespace Tasks.Application.Services.Usuarios.Dtos
{
    public class UsuarioDto : BaseEntityDto
    {
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}