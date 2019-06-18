using Tasks.WebApi.ViewModels.Generic;

namespace Tasks.WebApi.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseEntityViewModel
    {
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}