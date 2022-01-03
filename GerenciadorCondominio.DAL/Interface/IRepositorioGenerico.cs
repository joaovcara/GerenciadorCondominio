using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interface
{
    public interface IRepositorioGenerico<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> SelectAll();
        Task<TEntity> SelectById(int id);
        Task<TEntity> SelectById(string id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(int id);
        Task Delete(string id);
    }
}
