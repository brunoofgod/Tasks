using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tasks.Application.Services.Tasks;
using Tasks.WebApi.Generic;

namespace Tasks.WebApi.Controllers
{
    [Authorize("Bearer")]
    public class DashboardController : ControllerTasks
    {
        private readonly TaskServices _taskServices;
        public DashboardController(IMapper mapper, TaskServices taskServices) : base(mapper)
        {
            _taskServices = taskServices;
        }

        [HttpGet("dashboard/{usuarioId}")]
        public object Get(Guid usuarioId)
        {
            try
            {
                return Ok(_taskServices.GetTotalTasks(usuarioId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}