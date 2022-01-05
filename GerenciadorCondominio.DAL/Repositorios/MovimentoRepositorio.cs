using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class MovimentoRepositorio : RepositorioGenerico<Movimento>, IMovimentoRepositorio
    {
        private readonly Contexto _contexto;

        public MovimentoRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
