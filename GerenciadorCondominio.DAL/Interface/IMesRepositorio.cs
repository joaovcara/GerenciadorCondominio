using GerenciadorCondominio.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interface
{
    public interface IMesRepositorio : IRepositorioGenerico<Mes>
    {
        new Task<IEnumerable<Mes>> SelectAll();
    }
}
