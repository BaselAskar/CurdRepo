using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CurdRepo
{
    public class Crud<T,DB> : ICrud<T> where T : class,IEntity where DB : DbContext
    {
        private readonly DB _context;
        public Crud(DB context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public List<T> FindAll(params Expression<Func<T, object>>[] expressions)
        {
            IQueryable<T> query = _context.Set<T>();
            query = expressions.Aggregate(query, (curr,exp) => curr.Include(exp));
            return query.ToList();
        }

        public async Task<List<T>> FindAllAsync(params Expression<Func<T, object>>[] expressions)
        {
            IQueryable<T> query = _context.Set<T>();
            query = expressions.Aggregate(query, (curr, exp) => curr.Include(exp));
            return await query.ToListAsync();
        }

        public T? FindById(Guid id, params Expression<Func<T, object>>[] expressions)
        {
            IQueryable<T> query = _context.Set<T>();
            query = expressions.Aggregate(query, (curr, exp) => curr.Include(exp));
            return query.SingleOrDefault(e => e.Id == id);
        }

        public async Task<T?> FindByIdAsync(Guid id, params Expression<Func<T, object>>[] expressions)
        {
            IQueryable<T> query = _context.Set<T>();
            query = expressions.Aggregate(query, (curr, exp) => curr.Include(exp));
            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public T Update(T entity)
        {
            return _context.Set<T>().Update(entity).Entity;
        }
    }
}
