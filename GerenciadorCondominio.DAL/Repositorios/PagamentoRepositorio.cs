using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class PagamentoRepositorio : RepositorioGenerico<Pagamento>, IPagamentoRepositorio
    {
        public PagamentoRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
