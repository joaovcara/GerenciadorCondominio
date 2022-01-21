using Microsoft.AspNetCore.Identity;
using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        private readonly Contexto _contexto;
        private readonly UserManager<Usuario> _gerenciadorUsuarios;
        private readonly SignInManager<Usuario> _gerenciadorLogin;

        public UsuarioRepositorio(Contexto contexto, UserManager<Usuario> gerenciadorUsuarios, SignInManager<Usuario> gerenciadorLogin) : base(contexto)
        {
            _contexto = contexto;
            _gerenciadorUsuarios = gerenciadorUsuarios;
            _gerenciadorLogin = gerenciadorLogin;
        }

        public async Task<IdentityResult> CreateUser(Usuario usuario, string password)
        {
            try
            {
                return await _gerenciadorUsuarios.CreateAsync(usuario, password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task InsertUserFunction(Usuario usuario, string function)
        {
            try
            {
                await _gerenciadorUsuarios.AddToRoleAsync(usuario, function);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task LoginUser(Usuario usuario, bool lembrar)
        {
            try
            {
                await _gerenciadorLogin.SignInAsync(usuario, lembrar);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeslogarUsuario()
        {
            try
            {
                await _gerenciadorLogin.SignOutAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int VerificaUsuario()
        {
            try
            {
                return _contexto.Usuarios.Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Usuario> SelectForEmail(string email)
        {
            try
            {
                return await _gerenciadorUsuarios.FindByEmailAsync(email);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            try
            {
                await _gerenciadorUsuarios.UpdateAsync(usuario);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<bool> VerificarUsuarioCadastradoFuncao(Usuario usuario, string funcao)
        {
            try
            {
                return await _gerenciadorUsuarios.IsInRoleAsync(usuario, funcao);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<string>> SelectFuncaoUsuario(Usuario usuario)
        {
            try
            {
                return await _gerenciadorUsuarios.GetRolesAsync(usuario);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IdentityResult> RemoverFuncaoUsuario(Usuario usuario, IEnumerable<string> funcoes)
        {
            try
            {
                return await _gerenciadorUsuarios.RemoveFromRolesAsync(usuario, funcoes);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IdentityResult> IncluirFuncaoUsuario(Usuario usuario, IEnumerable<string> funcoes)
        {
            try
            {
                return await _gerenciadorUsuarios.AddToRolesAsync(usuario, funcoes);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<Usuario> SelectUserName(ClaimsPrincipal usuario)
        {
            try
            {
                return await _gerenciadorUsuarios.FindByNameAsync(usuario.Identity.Name);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<Usuario> SelectUserId(string id)
        {
            try
            {
                return await _gerenciadorUsuarios.FindByIdAsync(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string CodificarSenha(Usuario usuario, string senha)
        {
            try
            {
                return _gerenciadorUsuarios.PasswordHasher.HashPassword(usuario, senha);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
