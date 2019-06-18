namespace Tasks.Domain.Entidades.Usuarios
{
    public interface IUsuario
    {
        string Apelido { get; }
        string Email { get; }
        string Senha { get; }

        void SetApelido(string apelido);

        void SetEmail(string email);

        void SetSenha(string senha);
    }
}