using NetHacksPack.Data.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace NetHacksPack.Data.Persistence.EF
{
    class UnityOfWork<T> : IUnityOfWork<T>
        where T : DbContext
    {
        private T dataContext;
        private IDbContextTransaction dbTransaction;

        public bool IsInTransaction => throw new NotImplementedException();

        public UnityOfWork(T dataContext)
        {
            this.dataContext = dataContext;
        }

        public void BeginTransaction()
        {
            this.dbTransaction = this.dataContext.Database.BeginTransaction();
        }
        
        public void CheckPoint()
        {
            this.dataContext.SaveChanges();
        }

        public void Commit()
        {
            if (dbTransaction == null)
                return;
            this.dataContext.SaveChanges();
            this.dbTransaction?.Commit();
            this.dbTransaction = null;
        }

        public void Dispose()
        {
            this.Rollback();
            if (this.dataContext == null)
                return;
            this.TryDisposeDataContext();
        }

        public D GetRepositoryContext<D>() where D : class
        {
            return this.dataContext as D;
        }

        public void Rollback()
        {
            if (this.dbTransaction == null)
                return;
            this.dbTransaction.Rollback();
            this.dbTransaction = null;
            if (this.dataContext == null)
                return;
            this.TryDisposeDataContext();
        }

        private void TryDisposeDataContext()
        {
            try
            {
                if (this.dataContext.Database.GetDbConnection() != null && this.dataContext.Database.GetDbConnection().State == System.Data.ConnectionState.Open)
                    this.dataContext.Database.CloseConnection();
                this.dataContext.Dispose();
                this.dataContext = null;
            }
            finally
            {
                dataContext = null;
            }
        }
    }
}
