using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tasks.Application.Generic;
using Tasks.Application.Services.Tasks;
using Tasks.Application.Services.Tasks.Dtos;
using Tasks.WebApi.Generic;
using Tasks.WebApi.ViewModels.Tasks;

namespace Tasks.WebApi.Controllers
{
    [Authorize("Bearer")]
    public class TaskController : ControllerTasks
    {
        public readonly TaskServices _taskServices;

        public TaskController(IMapper _mapper, TaskServices taskServices) : base(_mapper)
        {
            _taskServices = taskServices;
        }

        [HttpPost("task/novo")]
        public ActionResult Post([FromBody]TaskViewModel vm)
        {
            try
            {
                var dto = _taskServices.Add(_mapper.Map<TaskViewModel, TaskDto>(vm));

                return Ok(TasksResult(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("task/editar")]
        public ActionResult Put([FromBody]TaskViewModel vm)
        {
            try
            {
                var dto = _taskServices.Edit(_mapper.Map<TaskViewModel, TaskDto>(vm));

                return Ok(TasksResult(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("task/editar/{taskId}/{usuarioId}")]
        public ActionResult Get(Guid taskId, Guid usuarioId)
        {
            try
            {
                var vm = _mapper.Map<TaskDto, TaskViewModel>(_taskServices.GetById(taskId, usuarioId));

                return Ok(vm);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("task/excluir/{taskId}/{taskDeExclusaoId}")]
        public ActionResult Delete(Guid taskId)
        {
            try
            {
                return Ok(_taskServices.Delete(taskId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("task/{pagina}/{usuarioId}")]
        public object Get(int pagina, Guid usuarioId)
        {
            try
            {
                var dto = _taskServices.GetGridView(new BasicFilterGridViewDto { Pagina = pagina, UsuarioId = usuarioId });
                return Ok(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}