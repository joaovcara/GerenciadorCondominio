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
    public class AluguelRepositorio : RepositorioGenerico<Aluguel>, IAluguelRepositorio
    {
        private readonly Contexto _contexto;

        public AluguelRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public bool AluguelExists(int mesId, int ano)
        {
            try
            {
                return _contexto.Alugueis.Any(x => x.MesId == mesId && x.Ano == ano);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public new async Task<IEnumerable<Aluguel>> SelectAll()
        {
            try
            {
                return await _contexto.Alugueis.Include(x => x.Mes).ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
