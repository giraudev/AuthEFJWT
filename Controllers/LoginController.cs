using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using modeloaulaefjwt_master.Models;
using modeloaulaefjwt_master.Repositorio;
using modelobasicoefjwt.Models;
using modelobasicoefjwt.Repositorio;

namespace modeloaulaefjwt_master.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        readonly AutenticacaoContext _contexto;
        public LoginController(AutenticacaoContext contexto)
        {
            _contexto = contexto;
        }

        [HttpPost]
        //para validar temos que usar Fromservices das classes envolvidas
        public IActionResult Validar([FromBody]Usuario usuario, [FromServices] SigningConfigurations signingConfigurations,
        [FromServices] TokenConfigurations tokenConfigurations)
        {
            Usuario user = _contexto.Usuarios.FirstOrDefault(c => c.Email == usuario.Email && c.Senha == usuario.Senha);

            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.IdUsuario.ToString(), "Login"),
                    new[]{
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.IdUsuario.ToString()),
                        new Claim("Nome", user.Nome), new Claim(ClaimTypes.Email, user.Email)
                    }
                );
                //estanciar as informações que serão passadas para o token
                var handler = new JwtSecurityTokenHandler();
                //criando o token
                var SecurityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    //o token armazena alguns parametros
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity
                });

                //"pegando" o token gerado
                var token = handler.WriteToken(SecurityToken);

                //retornar um objeto
                var retorno = new
                {
                    autenticacao = true,
                    acessToken = token,
                    message = "OK"
                };

                return Ok(retorno);
            }

            var retornoerro = new
            {
                autenticacao = false,
                message = "Falha na Autenticação"
            };

            return BadRequest(retornoerro);
        }
    }
}