using Tasks.Domain.Domain;
using Tasks.Domain.Entidades.Base.Interfaces;

using System;

namespace Tasks.Domain.Entidades.Usuarios.Builder
{
    public class UsuarioBuilder : IBaseBuilder<UsuarioBuilder, Usuario>
    {
        public string Apelido { get; private set; }
        public DateTime DataDeCadastro { get; private set; }
        public DateTime? DataDeExclusao { get; private set; }
        public string Email { get; private set; }
        public bool Excluido { get; private set; }
        public Guid Id { get; private set; }
        public string Senha { get; private set; }

        public Usuario Build()
        {
            return new Usuario(Id,
                Apelido,
                Email,
                Senha,
                DataDeExclusao,
                Excluido);
        }

        public UsuarioBuilder WithApelido(string apelido)
        {
            Apelido = apelido;
            return this;
        }

        public UsuarioBuilder WithDataDeExclusao(DateTime? dataDeExclusao)
        {
            DataDeExclusao = dataDeExclusao;
            return this;
        }

        public UsuarioBuilder WithEmail(string email)
        {
            Email = email;
            return this;
        }

        public UsuarioBuilder WithExcluido(bool excluido)
        {
            Excluido = excluido;
            return this;
        }

        public UsuarioBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public UsuarioBuilder WithSenha(string senha)
        {
            Senha = senha;
            return this;
        }
    }
}