using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null
            );

        Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        bool Update(T entity);
        Task<bool> Delete(int id);
        bool DeleteRange(IEnumerable<T> entities);
    }
}
