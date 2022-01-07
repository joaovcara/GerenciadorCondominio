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
    public class MesRepositorio : RepositorioGenerico<Mes>, IMesRepositorio
    {
        private readonly Contexto _contexto;

        public MesRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public new async Task<IEnumerable<Mes>> SelectAll()
        {
            try
            {
                return await _contexto.Meses.OrderBy(x => x.MesId).ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
