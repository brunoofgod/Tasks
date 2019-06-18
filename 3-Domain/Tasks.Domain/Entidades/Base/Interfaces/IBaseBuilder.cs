using System;

namespace Tasks.Domain.Entidades.Base.Interfaces
{
    public interface IBaseBuilder<Builder, Entidade>
    {
        DateTime? DataDeExclusao { get; }
        bool Excluido { get; }
        Guid Id { get; }

        Entidade Build();

        Builder WithDataDeExclusao(DateTime? dataDeExclusao);

        Builder WithExcluido(bool excluido);

        Builder WithId(Guid id);
    }
}