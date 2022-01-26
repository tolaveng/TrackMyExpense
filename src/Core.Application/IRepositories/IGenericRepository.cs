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
        Task<int> CountAsync(Expression<Func<T, bool>>? expression = null);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null
            );

        Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null
            );

        Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string>? includes = null);
        Task<bool> InsertAsync(T entity);
        Task<bool> InsertRangeAsync(IEnumerable<T> entities);
        bool Update(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Guid id);
        bool DeleteRange(IEnumerable<T> entities);
    }
}
