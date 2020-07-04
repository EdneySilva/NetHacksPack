using System;

namespace NetHacksPack.Data.Persistence.Abstractions
{
    public interface IUnityOfWork<T> : IUnityOfWork
        where T : class
    {
    }

    public interface IUnityOfWork : IDisposable
    {
        bool IsInTransaction { get; }

        void BeginTransaction();

        void CheckPoint();

        void Commit();

        void Rollback();

        T GetRepositoryContext<T>() where T : class;
    }
}
