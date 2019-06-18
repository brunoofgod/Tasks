using Tasks.Domain.Entidades.Base;
using Tasks.Domain.Entidades.Usuarios;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain.Domain
{
    public class Usuario : AbstractBaseEntity, IUsuario
    {
        public Usuario(Guid id,
            string apelido,
            string email,
            string senha,
            DateTime? dataDeExclusao,
            bool? excluido)
        {
            SetId(id);
            SetApelido(apelido);
            SetEmail(email);
            SetSenha(senha);
            SetDataDeExclusao(dataDeExclusao);
            SetExcluido(excluido);
        }

        public Usuario()
        {
        }

        [MaxLength(80)]
        public string Apelido { get; private set; }

        [MaxLength(120)]
        public string Email { get; private set; }

        [MaxLength(35)]
        public string Senha { get; private set; }

        public void SetApelido(string apelido)
        {
            Apelido = apelido;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetSenha(string senha)
        {
            Senha = senha;
        }
    }
}