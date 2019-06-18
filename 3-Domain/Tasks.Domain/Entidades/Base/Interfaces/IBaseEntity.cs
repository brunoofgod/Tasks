using System;

namespace Tasks.Domain.Entidades.Base.Interfaces
{
    public interface IBaseEntity
    {
        DateTime? DataDeAlteracao { get; }
        DateTime DataDeCadastro { get; }
        DateTime? DataDeExclusao { get; }
        bool Excluido { get; }
    }
}