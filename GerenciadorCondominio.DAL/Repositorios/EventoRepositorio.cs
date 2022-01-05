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
    public class EventoRepositorio : RepositorioGenerico<Evento>, IEventoRepositorio
    {
        private readonly Contexto _contexto;
        public EventoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Evento>> SelectEventoUsuarioId(string usuarioId)
        {
            try
            {
                return await _contexto.Eventos.Where(x => x.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
