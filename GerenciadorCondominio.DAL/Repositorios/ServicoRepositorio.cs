using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class ServicoRepositorio : RepositorioGenerico<Servico>, IServicoRepositorio
    {
        private readonly Contexto _contexto;

        public ServicoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Servico>> SelectServicoUsuarioId(string id)
        {
            try
            {
                return await _contexto.Servicos.Where(x => x.UsuarioId == id).ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
