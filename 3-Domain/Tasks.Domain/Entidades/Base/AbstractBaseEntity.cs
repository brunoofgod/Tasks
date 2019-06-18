using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasks.Domain.Entidades.Base.Interfaces;

namespace Tasks.Domain.Entidades.Base
{
    public abstract class AbstractBaseEntity : IBaseEntity
    {
        public DateTime? DataDeAlteracao { get; private set; }
        public DateTime DataDeCadastro { get; private set; }
        public DateTime? DataDeExclusao { get; private set; }
        public bool Excluido { get; private set; }

        [Key]
        [Column("Id", Order = 1)]
        public Guid Id { get; private set; }

        public void SetDataDeAlteracao(DateTime? dataDeAlteracao)
        {
            DataDeAlteracao = dataDeAlteracao;
        }

        public void SetDataDeCadastro(DateTime dataDeCadastro)
        {
            DataDeCadastro = dataDeCadastro;
        }

        public void SetDataDeExclusao(DateTime? dataDeExclusao)
        {
            DataDeExclusao = dataDeExclusao;
        }

        public void SetExcluido(bool? excluido)
        {
            Excluido = excluido ?? DataDeExclusao.HasValue;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}