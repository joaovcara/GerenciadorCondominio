using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class FuncaoRepositorio : RepositorioGenerico<Funcao>, IFuncaoRepositorio
    {
        public FuncaoRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
