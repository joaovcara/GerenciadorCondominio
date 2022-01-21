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
    public class ImovelRepositorio : RepositorioGenerico<Imovel>, IImovelRepositorio
    {
        private readonly Contexto _contexto;

        public ImovelRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Imovel>> SelectImovelUsuarioId(string id)
        {
            try
            {
                return await _contexto.Imoveis
                    .Include(x => x.Locatario)
                    .Include(x => x.Proprietario)
                    .Where(x => x.LocatarioId == id || x.ProprietarioId == id).ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public new async Task<IEnumerable<Imovel>> SelectAll()
        {
            try
            {
                return await _contexto.Imoveis
                    .Include(x => x.Locatario)
                    .Include(x => x.Proprietario).ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
