using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tasks.Application.Generic;
using Tasks.Application.Services.Usuarios;
using Tasks.Application.Services.Usuarios.Dtos;
using Tasks.WebApi.Generic;
using Tasks.WebApi.ViewModels.Usuarios;

namespace Tasks.WebApi.Controllers
{
    [Authorize("Bearer")]
    public class UsuarioController : ControllerTasks
    {
        public readonly UsuarioServices _usuarioServices;

        public UsuarioController(IMapper _mapper, UsuarioServices usuarioServices) : base(_mapper)
        {
            _usuarioServices = usuarioServices;
        }

        [AllowAnonymous]
        [HttpPost("usuario/novo")]
        public ActionResult Post([FromBody]UsuarioViewModel vm)
        {
            try
            {
                var dto = _usuarioServices.Add(_mapper.Map<UsuarioViewModel, UsuarioDto>(vm));

                return Ok(TasksResult(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("usuario/editar")]
        public ActionResult Put([FromBody]UsuarioViewModel vm)
        {
            try
            {
                var dto = _usuarioServices.Edit(_mapper.Map<UsuarioViewModel, UsuarioDto>(vm));

                return Ok(TasksResult(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("usuario/editar/{usuarioId}")]
        public ActionResult GetUsuario(Guid usuarioId)
        {
            try
            {
                var vm = _mapper.Map<UsuarioDto, UsuarioViewModel>(_usuarioServices.GetById(usuarioId));

                return Ok(vm);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("usuario/excluir/{usuarioDeExclusaoId}/{usuarioId}")]
        public ActionResult Delete(Guid usuarioDeExclusaoId, Guid usuarioId)
        {
            try
            {
                return Ok(_usuarioServices.Delete(usuarioId, usuarioDeExclusaoId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("usuario/{pagina}/{usuarioId}")]
        public ActionResult Get(int pagina, Guid usuarioId)
        {
            try
            {
                var dto = _usuarioServices.GetGridView(new BasicFilterGridViewDto { Pagina = pagina, UsuarioId = usuarioId });
                return Ok(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}