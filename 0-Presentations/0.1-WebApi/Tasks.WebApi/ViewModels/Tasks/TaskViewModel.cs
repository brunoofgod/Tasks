using System;
using Tasks.WebApi.ViewModels.Generic;

namespace Tasks.WebApi.ViewModels.Tasks
{
    public class TaskViewModel : BaseEntityViewModel
    {
        public DateTime? Conclusao { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}