using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class FuncaoRepositorio : RepositorioGenerico<Funcao>, IFuncaoRepositorio
    {
        private readonly RoleManager<Funcao> _gerenciadorFuncoes;

        public FuncaoRepositorio(Contexto contexto, RoleManager<Funcao> gerenciadorFuncoes) : base(contexto)
        {
            _gerenciadorFuncoes = gerenciadorFuncoes;
        }

        public async Task AdicionarFuncao(Funcao funcao)
        {
            try
            {
                funcao.Id = Guid.NewGuid().ToString();
                await _gerenciadorFuncoes.CreateAsync(funcao);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public new async Task Update(Funcao funcao)
        {
            try
            {
                Funcao func = await SelectById(funcao.Id);

                func.Name = funcao.Name;
                func.NormalizedName = funcao.NormalizedName;
                func.Descricao = funcao.Descricao;

                await _gerenciadorFuncoes.UpdateAsync(func);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
