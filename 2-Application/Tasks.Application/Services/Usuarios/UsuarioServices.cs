using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt;
using System;
using System.Linq;
using Tasks.Application.Generic;
using Tasks.Application.Services.Agendamentos.Dtos;
using Tasks.Application.Services.Usuarios.Dtos;
using Tasks.Data.Context;
using Tasks.Domain.Domain;
using Tasks.Domain.Entidades.Usuarios.Builder;

namespace Tasks.Application.Services.Usuarios
{
    public class UsuarioServices : IBaseEntityService<UsuarioDto, Usuario>
    {
        private readonly TasksContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UsuarioDto> _usuarioValidator;

        public UsuarioServices(TasksContext context,
            IMapper mapper,
            IValidator<UsuarioDto> usuarioValidator)
        {
            _context = context;
            _mapper = mapper;
            _usuarioValidator = usuarioValidator;
        }

        public UsuarioDto Add(UsuarioDto dto)
        {
            if (!dto.Validar(_usuarioValidator).IsValid) return dto;

            dto.Senha = EncryptProvider.Md5(dto.Senha);
            _context.Add(MontarEntidade(dto));
            _context.SaveChanges();
            return dto;
        }

        public bool Delete(Guid id, Guid usuarioDeExclusaoId)
        {
            var usuario = _context
                .Usuarios
                .FirstOrDefault(x => x.Id == id && x.Id == usuarioDeExclusaoId);

            if (usuario == null) throw new Exception("Usuário não inválido ou de outra conta");

            usuario.SetExcluido(true);
            usuario.SetDataDeExclusao(DateTime.Now);

            return _context.SaveChanges() > 0;
        }

        public bool Delete(Guid id) => Delete(id, Guid.Empty);

        public UsuarioDto Edit(UsuarioDto dto)
        {
            if (!dto.Validar(_usuarioValidator).IsValid) return dto;

            var entidade = _context
                .Usuarios
                .FirstOrDefault(x => x.Id == dto.Id);

            entidade.SetApelido(dto.Apelido);
            entidade.SetEmail(dto.Email);
            if (dto.Senha != entidade.Senha) entidade.SetSenha(EncryptProvider.Md5(dto.Senha));

            _context.Usuarios.Update(entidade);
            _context.SaveChanges();

            return dto;
        }

        public UsuarioDto GetByEmail(string email)
        {
            var user = _context
                .Usuarios
                .FirstOrDefault(c => c.Email == email);

            if (user == null) return new UsuarioDto();

            return _mapper.Map<Usuario, UsuarioDto>(user);
        }

        public UsuarioDto GetById(Guid id)
        {
            var usuario = _context
                .Usuarios
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<Usuario, UsuarioDto>(usuario);
        }

        public GridViewDto<UsuarioGridDto> GetGridView(BasicFilterGridViewDto basicFilterGridViewDto)
        {
            var consulta = _context
                .Usuarios
                .AsNoTracking()
                .Where(x => x.Id == basicFilterGridViewDto.UsuarioId);

            var usuario = consulta
                .Select(x => new UsuarioGridDto
                {
                    Id = x.Id,
                    Nome = x.Apelido,
                    Email = x.Email
                })
                .ToList();

            var totalPaginas = (decimal)1;

            var gridView = new GridViewDto<UsuarioGridDto>
            {
                Paginas = int.Parse(totalPaginas.ToString()),
                Listagem = usuario
            };

            return gridView;
        }

        public Usuario MontarEntidade(UsuarioDto dto)
        {
            return new UsuarioBuilder()
                .WithApelido(dto.Apelido)
                .WithDataDeExclusao(dto.DataDeExclusao)
                .WithEmail(dto.Email)
                .WithExcluido(dto.Excluido)
                .WithId(dto.Id)
                .WithSenha(dto.Senha)
                .Build();
        }
    }
}