using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tasks.Application.Generic;
using Tasks.Application.Services.Tasks.Dtos;
using Tasks.Data.Context;
using Tasks.Domain.Entidades.Tasks;
using Tasks.Domain.Entidades.Tasks.Builder;

namespace Tasks.Application.Services.Tasks
{
    public class TaskServices : IBaseEntityService<TaskDto, Task>
    {
        private readonly TasksContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<TaskDto> _taskValidator;

        public TaskServices(TasksContext context, IMapper mapper, IValidator<TaskDto> taskValidator)
        {
            _context = context;
            _mapper = mapper;
            _taskValidator = taskValidator;
        }

        public TaskDto Add(TaskDto dto)
        {
            if (!dto.Validar(_taskValidator).IsValid) return dto;

            _context.Tasks.Add(MontarEntidade(dto));
            _context.SaveChanges();

            return dto;
        }

        public bool Delete(Guid id)
        {
            var ent = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (ent == null) throw new Exception("Task inválida");

            ent.SetDataDeExclusao(DateTime.Now);
            ent.SetExcluido(true);

            return _context.SaveChanges() > 0;
        }

        public TaskDto Edit(TaskDto dto)
        {
            if (!dto.Validar(_taskValidator).IsValid) return dto;

            _context.Tasks.Update(MontarEntidade(dto));
            _context.SaveChanges();

            return dto;
        }

        public TaskDto GetById(Guid id)
            => _mapper.Map<Task, TaskDto>(_context.Tasks.FirstOrDefault(x => x.Id == id));

        public TaskDto GetById(Guid id, Guid usuarioId)
            => _mapper.Map<Task, TaskDto>(_context.Tasks.FirstOrDefault(x => x.Id == id && x.UsuarioId == usuarioId));

        public GridViewDto<TaskGridDto> GetGridView(BasicFilterGridViewDto filterDto)
        {
            var consulta = _context
                .Tasks
                .AsNoTracking()
                .Where(x => x.UsuarioId == filterDto.UsuarioId.Value && !x.Excluido);
            if (filterDto.Pagina <= 1)
            {
                consulta = consulta
                    .OrderByDescending(x => x.DataDeCadastro)
                    .Take(15);
            }
            else
            {
                consulta = consulta
                .OrderByDescending(x => x.DataDeCadastro)
                .Skip((filterDto.Pagina - 1) * 15)
                .Take(15);
            }

            var tasks = consulta
                .Select(x => new TaskGridDto
                {
                    Id = x.Id,
                    Titulo = x.Titulo
                })
                .ToList();

            decimal totalRegistros = _context
                .Tasks
                .Where(x => x.UsuarioId == filterDto.UsuarioId.Value && !x.Excluido)
                .Count();

            var totalPaginas = Math.Ceiling(decimal.Divide(totalRegistros, 15));

            var gridView = new GridViewDto<TaskGridDto>
            {
                Paginas = int.Parse(totalPaginas.ToString()),
                Listagem = tasks
            };

            return gridView;
        }

        public int GetTotalTasks(Guid usuarioId)
        {
            return _context
                .Tasks
                .AsNoTracking()
                .Where(x => !x.Excluido && x.UsuarioId == usuarioId)
                .Count();
        }

        public Task MontarEntidade(TaskDto dto)
                    => new TaskBuilder()
                .WithConclusao(dto.Conclusao)
                .WithUsuarioId(dto.UsuarioId)
                .WithTitulo(dto.Titulo)
                .WithId(dto.Id)
                .WithExcluido(dto.Excluido)
                .WithDescricao(dto.Descricao)
                .WithDataDeExclusao(dto.DataDeExclusao)
                .Build();
    }
}