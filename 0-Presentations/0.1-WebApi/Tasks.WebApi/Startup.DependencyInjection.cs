using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Services.Logins;
using Tasks.Application.Services.Tasks;
using Tasks.Application.Services.Tasks.Dtos;
using Tasks.Application.Services.Tasks.Validation;
using Tasks.Application.Services.Usuarios;
using Tasks.Application.Services.Usuarios.Dtos;
using Tasks.Application.Services.Usuarios.Validation;
using Tasks.Data.Context;

namespace Tasks.WebApi
{
    public partial class Startup
    {
        public void ConfigureServicesDependencyInjection(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<TasksContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("Defa‌​ultConnection")));

            services.AddScoped<LoginServices>();
            services.AddScoped<UsuarioServices>();
            services.AddScoped<TaskServices>();
            services.AddScoped<IValidator<UsuarioDto>, UsuarioValidator>();
            services.AddScoped<IValidator<TaskDto>, TaskValidator>();
        }
    }
}