using Tasks.Application.Dtos_Genericos;
using System;

namespace Tasks.WebApi.ViewModels.Generic
{
    public abstract class BaseEntityViewModel : BaseDto
    {
        public DateTime? DataDeAlteracao { get; set; }
        public DateTime? DataDeCadastro { get; set; }
        public DateTime? DataDeExclusao { get; set; }
        public bool? Excluido { get; set; }
        public Guid Id { get; set; }
    }
}