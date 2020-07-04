using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetHacksPack.Data.Persistence.Abstractions
{
    public interface IRepository<T>
        where T : class
    {
        T Insert(T data);

        Task<T> InsertAsync(T data);

        IEnumerable<T> InsertRange(IEnumerable<T> data);

        Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> data);

        IQueryable<T> Get();

        IAsyncEnumerable<T> GetAsync();

        //IPagedQueryable<T> Get(int pageNumber, int pageSize);

        //Task<IPagedQueryable<T>> GetAsync(int pageNumber, int pageSize);

        T Remove(T data);

        Task<T> RemoveAsync(T data);

        IEnumerable<T> RemoveRange(IEnumerable<T> data);

        Task<IEnumerable<T>> RemoveRangeAsync(IEnumerable<T> data);

        T Update(T data);

        Task<T> UpdateAsync(T data);

        IEnumerable<T> UpdateRange(IEnumerable<T> data);

        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> data);
    }
}
