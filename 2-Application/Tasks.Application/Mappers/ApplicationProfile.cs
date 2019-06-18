using AutoMapper;
using Tasks.Application.Services.Tasks.Dtos;
using Tasks.Application.Services.Usuarios.Dtos;
using Tasks.Domain.Domain;
using Tasks.Domain.Entidades.Tasks;

namespace Tasks.Application.Mappers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(x => x.IsValid, conf => conf.Ignore());

            CreateMap<Task, TaskDto>()
                .ForMember(x => x.IsValid, conf => conf.Ignore());
        }
    }
}