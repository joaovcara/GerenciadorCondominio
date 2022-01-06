using GerenciadorCondominio.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interface
{
    public interface IImovelRepositorio : IRepositorioGenerico<Imovel>
    {
        new Task<IEnumerable<Imovel>> SelectAll();

        Task<IEnumerable<Imovel>> SelectImovelUsuarioId(string id);
    }
}
