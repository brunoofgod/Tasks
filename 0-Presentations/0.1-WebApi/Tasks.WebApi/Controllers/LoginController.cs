using Tasks.Application.Services.Logins;
using Tasks.Application.Services.Logins.Dtos;
using Tasks.WebApi.ViewModels.Login;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Tasks.WebApi.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post(
            [FromBody]LoginViewModel usuario,
            [FromServices]LoginServices _loginServices,
            [FromServices]IMapper _mapper)
        {
            try
            {
                var dto = _loginServices.FazerLogin(_mapper.Map<LoginViewModel, LoginDto>(usuario));
                if (!dto.Autenticado) return BadRequest(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}