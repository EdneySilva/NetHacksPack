using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace NetHacksPack.Data.Linq.EF
{
    internal class EfQuery<TEntity> : IQueryable<TEntity>
        where TEntity : class
    {
        private readonly DbContext dbContext;
        private readonly IQueryable<TEntity> dbSet;

        public EfQuery(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>().AsQueryable();
        }
        public Type ElementType => dbSet.ElementType;

        public Expression Expression => dbSet.Expression;

        public IQueryProvider Provider => dbSet.Provider;

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dbSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dbSet.GetEnumerator();
        }
    }
}
