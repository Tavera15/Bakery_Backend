using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.SQL.Services
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DataContext c)
        {
            _context = c;
            _dbSet = c.Set<T>();
        }

        public virtual IEnumerable<T> GetAllEntities()
        {
            return _dbSet.OrderByDescending(x => x.mTimeModified);
        }

        public virtual async Task<T> FindAsync(string id)
        {
            T target = await _dbSet.FindAsync(id);

            if(target == null)
            {
                throw new EntityNotFoundException("Entity not found: " + id);
            }

            return target;
        }

        public virtual async Task<T> InsertAsync(T newEntity)
        {
            _dbSet.Add(newEntity);
            await _context.SaveChangesAsync();

            return newEntity;
        }

        public virtual async Task<int> DeleteAsync(string entityID)
        {
            T target = await FindAsync(entityID);
            _dbSet.Remove(target);

            return await _context.SaveChangesAsync();
        }

        public virtual async Task<T> UpdateAsync(string entityId, T updatedEntity)
        {
            return await Task.FromResult(updatedEntity);
        }
    }
}
