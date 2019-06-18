using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasks.Application.Services.Logins.Dtos;
using Tasks.Application.Services.Usuarios;
using Tasks.Application.Services.Usuarios.Dtos;
using Tasks.Data.Context;

namespace Tasks.Application.Services.Logins
{
    public class LoginServices
    {
        private readonly AppSettings _appSettings;
        private readonly TasksContext _context;
        private readonly UsuarioServices _usuarioServices;

        public LoginServices(TasksContext context, UsuarioServices usuarioServices, IOptions<AppSettings> appSettings)
        {
            _usuarioServices = usuarioServices;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public AutenticacaoDoLogin FazerLogin(LoginDto loginDto)
        {
            bool credenciaisValidas = false;
            UsuarioDto usuarioBase = new UsuarioDto();

            if (loginDto != null && !string.IsNullOrWhiteSpace(loginDto.Login))
            {
                usuarioBase = _usuarioServices.GetByEmail(loginDto.Login);
                credenciaisValidas = usuarioBase.Id != Guid.Empty && !usuarioBase.Excluido && loginDto.Login == usuarioBase.Email && EncryptProvider.Md5(loginDto.Senha).ToUpper() == usuarioBase.Senha.ToUpper();
            }

            if (credenciaisValidas)
            {
                var dataDeExpiracao = DateTime.Now.AddDays(1);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usuarioBase.Id.ToString())
                    }),
                    Expires = dataDeExpiracao,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(tokenObj);

                _context.SaveChanges();
                return new AutenticacaoDoLogin
                {
                    Autenticado = true,
                    DataDeCriacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    DataDeExpiracao = dataDeExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    TokenDeAcesso = token,
                    Mensagem = "OK",
                    UsuarioId = usuarioBase.Id,
                    Usuario = usuarioBase.Apelido,
                };
            }
            else
            {
                return new AutenticacaoDoLogin
                {
                    Autenticado = false,
                    Mensagem = "Falha ao autenticar"
                };
            }
        }
    }
}