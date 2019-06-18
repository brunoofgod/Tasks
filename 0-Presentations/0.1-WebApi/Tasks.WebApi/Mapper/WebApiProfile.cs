using AutoMapper;
using FluentValidation.Results;
using Tasks.Application.Services.Logins.Dtos;
using Tasks.Application.Services.Tasks.Dtos;
using Tasks.Application.Services.Usuarios.Dtos;
using Tasks.WebApi.ViewModels.Generic;
using Tasks.WebApi.ViewModels.Login;
using Tasks.WebApi.ViewModels.Tasks;
using Tasks.WebApi.ViewModels.Usuarios;

namespace Tasks.WebApi.Mapper
{
    public class WebApiProfile : Profile
    {
        public WebApiProfile()
        {
            CreateMap<LoginViewModel, LoginDto>();

            CreateMap<ValidationFailure, ValidacaoViewModel>()
                .ForMember(x => x.Mensagem, cfg => cfg.MapFrom(c => c.ErrorMessage))
                .ForMember(x => x.Campo, cfg => cfg.MapFrom(c => c.PropertyName));

            CreateMap<UsuarioViewModel, UsuarioDto>();
            CreateMap<UsuarioDto, UsuarioViewModel>();

            CreateMap<TaskDto, TaskViewModel>();
            CreateMap<TaskViewModel, TaskDto>();
        }
    }
}