using GerenciadorCondominio.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interface
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        int VerificaUsuario();
        Task LoginUser(Usuario usuario, bool lembrar);

        Task DeslogarUsuario();
        Task<IdentityResult> CreateUser(Usuario usuario, string password);
        Task InsertUserFunction(Usuario usuario, string function);

        Task<Usuario> SelectForEmail(string email);

        Task AtualizarUsuario(Usuario usuario);

        Task<bool> VerificarUsuarioCadastradoFuncao(Usuario usuario, string funcao);

        Task<IEnumerable<string>> SelectFuncaoUsuario(Usuario usuario);

        Task<IdentityResult> RemoverFuncaoUsuario(Usuario usuario, IEnumerable<string> funcoes);

        Task<IdentityResult> IncluirFuncaoUsuario(Usuario usuario, IEnumerable<string> funcoes);
    }
}
