using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CurdRepo
{
    public interface ICrud<T> where T : class,IEntity
    {
        T Add(T entity);
        T? FindById(Guid id,params Expression<Func<T, object>>[] expressions);
        Task<T?> FindByIdAsync(Guid id,params Expression<Func<T, object>>[] expressions);
        List<T> FindAll(params Expression<Func<T, object>>[] expressions);
        Task<List<T>> FindAllAsync(params Expression<Func<T, object>>[] expressions);
        T Update(T entity);
        void Delete(T entity);
    }
}
