using GerenciadorCondominio.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interface
{
    public interface IAluguelRepositorio : IRepositorioGenerico<Aluguel>
    {
        bool AluguelExists(int mesId, int ano);

        Task<IEnumerable<Aluguel>> SelectAll();
    }
}
