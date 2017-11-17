using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exodus3.Domain;
using Microsoft.EntityFrameworkCore;

namespace Exodus3.Api.Data
{
    public class Repository<T> : IRepository<T> where T : E3Entity
    {
        private readonly E3DbContext _db;
        public Repository(E3DbContext context)
        {
            _db = context;
        }

        public async Task<T> Add(T entity)
        {

            entity.CreatedOn = DateTime.UtcNow;
            entity.UpdatedOn = entity.CreatedOn;
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> Get(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();

            foreach (var property in includes)
                query = query.Include(property);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();

            foreach (var property in includes)
                query = query.Include(property);

            return await query.Where(where).ToListAsync();
        }

        public async Task<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();

            foreach (var property in includes)
                query = query.Include(property);

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(T entity)
        {
            if (entity == null)
                return false;

            var existing = await _db.Set<T>().SingleOrDefaultAsync((x => Equals(x.Id, entity.Id)));

            if (existing != null)
            {
                entity.UpdatedOn = DateTime.UtcNow;
                _db.Update(entity);
                return await _db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
                return;

            var existing = await _db.Set<T>().SingleOrDefaultAsync((x => Equals(x.Id, entity.Id)));

            if (existing != null)
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
