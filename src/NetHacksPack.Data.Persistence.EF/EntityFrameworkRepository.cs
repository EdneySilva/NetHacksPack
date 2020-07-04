using NetHacksPack.Data.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetHacksPack.Data.Persistence.EF
{
    public class EntityFrameworkRepository<T> : IRepository<T>
        where T : class
    {
        private readonly IUnityOfWork unityOfWork;

        public EntityFrameworkRepository(IUnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }

        public IQueryable<T> Get()
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            return currentContext.Set<T>();
        }

        public IAsyncEnumerable<T> GetAsync()
        {
            return this.Get().AsAsyncEnumerable();
        }

        public T Insert(T data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.Add(data);
            return data;
        }

        public async Task<T> InsertAsync(T data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            await currentContext.AddAsync(data);
            return data;
        }

        public IEnumerable<T> InsertRange(IEnumerable<T> data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.AddRange(data);
            return data;
        }

        public async Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            await currentContext.AddRangeAsync(data);
            return data;
        }

        public T Remove(T data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.Remove(data);
            return data;
        }

        public Task<T> RemoveAsync(T data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.Remove(data);
            return Task.FromResult(data);
        }

        public IEnumerable<T> RemoveRange(IEnumerable<T> data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.RemoveRange(data);
            return data;
        }

        public Task<IEnumerable<T>> RemoveRangeAsync(IEnumerable<T> data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.RemoveRange(data);
            return Task.FromResult(data);
        }

        public T Update(T data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.Update(data);
            return data;
        }

        public Task<T> UpdateAsync(T data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.Update(data);
            return Task.FromResult(data);
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.UpdateRange(data);
            return data;
        }

        public Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> data)
        {
            var currentContext = this.unityOfWork.GetRepositoryContext<DbContext>();
            currentContext.UpdateRange(data);
            return Task.FromResult(data);
        }
    }
}
