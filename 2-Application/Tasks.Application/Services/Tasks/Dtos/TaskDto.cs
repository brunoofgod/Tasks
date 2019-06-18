using System;
using Tasks.Application.Dtos_Genericos;

namespace Tasks.Application.Services.Tasks.Dtos
{
    public class TaskDto : BaseEntityDto
    {
        public DateTime? Conclusao { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}