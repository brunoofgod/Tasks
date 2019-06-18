using System;
using Tasks.Domain.Entidades.Base.Interfaces;

namespace Tasks.Domain.Entidades.Tasks
{
    public interface ITask : IBaseEntity
    {
        DateTime? Conclusao { get; }
        string Descricao { get; }
        string Titulo { get; }
        Guid UsuarioId { get; }

        void SetConclusao(DateTime? conclusao);

        void SetDescricao(string descricao);

        void SetTitulo(string titulo);

        void SetUsuarioId(Guid usuarioId);
    }
}