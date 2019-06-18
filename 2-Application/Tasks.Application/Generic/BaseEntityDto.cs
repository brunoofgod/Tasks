using Tasks.Application.Services.Usuarios.Dtos;
using System;

namespace Tasks.Application.Dtos_Genericos
{
    public abstract class BaseEntityDto : BaseDto
    {
        public DateTime? DataDeAlteracao { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime? DataDeExclusao { get; set; }
        public bool Excluido { get; set; }
        public Guid Id { get; set; }
    }
}