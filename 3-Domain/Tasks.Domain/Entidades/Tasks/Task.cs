using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasks.Domain.Entidades.Base;

namespace Tasks.Domain.Entidades.Tasks
{
    public class Task : AbstractBaseEntity, ITask
    {
        public Task()
        {
        }

        public Task(Guid id,
            DateTime? conclusao,
            string descricao,
            string titulo,
            DateTime? dataDeExclusao, 
            Guid usuarioId)
        {
            SetId(id);
            SetConclusao(conclusao);
            SetDescricao(descricao);
            SetTitulo(titulo);
            SetDataDeExclusao(dataDeExclusao);
            SetUsuarioId(usuarioId);
        }

        public DateTime? Conclusao { get; private set; }
        [MaxLength(500)]
        public string Descricao { get; private set; }

        [NotMapped]
        public string Status => Conclusao.HasValue ? "Concluída" : "Aberta";

        [MaxLength(150)]
        public string Titulo { get; private set; }

        public Guid UsuarioId { get; private set; }

        public void SetConclusao(DateTime? conclusao)
        {
            Conclusao = conclusao;
        }

        public void SetDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void SetTitulo(string titulo)
        {
            Titulo = titulo;
        }

        public void SetUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}