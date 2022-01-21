using GerenciadorCondominio.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Repositorios
{
    public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
    {

        private readonly Contexto _contexto;

        public RepositorioGenerico(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task Delete(TEntity entity)
        {
            try
            {
                _contexto.Set<TEntity>().Remove(entity);

                await _contexto.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await SelectById(id);

                _contexto.Set<TEntity>().Remove(entity);

                await _contexto.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var entity = await SelectById(id);

                _contexto.Set<TEntity>().Remove(entity);

                await _contexto.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Insert(TEntity entity)
        {
            try
            {
                await _contexto.AddAsync(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Insert(List<TEntity> entity)
        {
            try
            {
                await _contexto.AddRangeAsync(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<TEntity>> SelectAll()
        {
            try
            {
                return await _contexto.Set<TEntity>().ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<TEntity> SelectById(int id)
        {
            try
            {
                return await _contexto.Set<TEntity>().FindAsync(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<TEntity> SelectById(string id)
        {
            try
            {
                return await _contexto.Set<TEntity>().FindAsync(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                _contexto.Set<TEntity>().Update(entity);

                await _contexto.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
