using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class ServicoImovelRepositorio : RepositorioGenerico<ServicoImovel>, IServicoImovelRepositorio
    {
        private readonly Contexto _contexto;

        public ServicoImovelRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
