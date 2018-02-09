using System;
using System.Linq;
using modelobasicoefjwt.Models;

namespace modelobasicoefjwt.Repositorio
{
    public class CodeFirstBanco
    {
        public static void Inicializar(AutenticacaoContext contexto){

            if(contexto.Usuarios.Any()) return;

            var usuario = new Usuario(){
                Nome = "Fernando",
                Email = "fernando.guerra@corujasdev.com.br",
                Senha = "123456", 
                //dados colocados posteriormente para fazer updates
                DataNascimento = Convert.ToDateTime("10-10-1978"),
                Cpf = "339.265.568-82"
            };

            contexto.Usuarios.Add(usuario);

            var permissao = new Permissao(){
                //nome alterado de Controle
                Nome = "Converso"
            };

            contexto.Permissoes.Add(permissao);

            var usuariopermissao = new UsuarioPermissao(){
                IdUsuario = usuario.IdUsuario,
                IdPermissao = permissao.IdPermissao
            };

            contexto.UsuariosPermissoes.Add(usuariopermissao);
            contexto.SaveChanges();
            
        }
    }
}