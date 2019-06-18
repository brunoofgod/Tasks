using System;
using Tasks.Domain.Entidades.Base.Interfaces;

namespace Tasks.Domain.Entidades.Tasks.Builder
{
    public class TaskBuilder : IBaseBuilder<TaskBuilder, Task>
    {
        public DateTime? Conclusao { get; private set; }
        public DateTime? DataDeExclusao { get; private set; }
        public string Descricao { get; private set; }
        public bool Excluido { get; private set; }
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public Guid UsuarioId { get; private set; }

        public Task Build()
        {
            return new Task(Id, Conclusao, Descricao, Titulo, DataDeExclusao, UsuarioId);
        }

        public TaskBuilder WithConclusao(DateTime? conclusao)
        {
            Conclusao = conclusao;
            return this;
        }

        public TaskBuilder WithDataDeExclusao(DateTime? dataDeExclusao)
        {
            DataDeExclusao = dataDeExclusao;
            return this;
        }

        public TaskBuilder WithDescricao(string descricao)
        {
            Descricao = descricao;
            return this;
        }

        public TaskBuilder WithExcluido(bool excluido)
        {
            Excluido = excluido;
            return this;
        }

        public TaskBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public TaskBuilder WithTitulo(string titulo)
        {
            Titulo = titulo;
            return this;
        }

        public TaskBuilder WithUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;
            return this;
        }
    }
}