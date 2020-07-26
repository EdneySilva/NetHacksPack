NetHacksPack.Data.Persistence.Abstractions
=====================
This package contains the abstractions to handle Repository pattern

### Why use NetHacksPack.Data.Persistence.Abstractions
---

This package help you to get a better application design improve the quality from the application.

#### How to use
---
A repository should inherite from IRepository<TEntity>, and a unity of work should inherite IUnitOfWork<TDataContext>

### NetHacksPack.Data.Persistence.Abstractions.IRepository<TEntity>
---
This interface defines how a repository must be implemented.

Bellow the description from each item in this interface

#### Properties

Signature    | Description
------------ | -------------
UnityOfWork | An instance of IUnityOfWork to commit any operations on your storage container

#### Methods

Signature    | Description
------------ | -------------
T Insert(T data); | Inserts the T data on your storage container
Task<T> InsertAsync(T data); | Inserts the T data on your storage container using an async operation
IEnumerable<T> InsertRange(IEnumerable<T> data); | Inserts a range of T data on your storage container
Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> data); | Inserts a range of T data on your storage container using an async operation
IQueryable<T> Get(); | Create a queryable object to be executed in your storage container
IAsyncEnumerable<T> GetAsync(); | Create a queryable object to be executed in your storage container using an async operation
T Remove(T data); | Removes the T data on your storage container
Task<T> RemoveAsync(T data); | Removes the T data on your storage container using an async operation
IEnumerable<T> RemoveRange(IEnumerable<T> data); | Removes a range of T data on your storage container
Task<IEnumerable<T>> RemoveRangeAsync(IEnumerable<T> data); | Removes a range of T data on your storage container using an async operation
T Update(T data); | Updates the T data on your storage container
Task<T> UpdateAsync(T data); | Updates the T data on your storage container using an async operation
IEnumerable<T> UpdateRange(IEnumerable<T> data); | Updates a range of the T data on your storage container
Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> data); | Updates a range of the T data on your storage container using an async operation


### NetHacksPack.Data.Persistence.Abstractions.IUnitOfWork<TDataContext>
---
This  interface defines how to perform operations that insert, update or delete data on your storage container

Bellow the description from each item in this interface

### Properties

Signature    | Description
------------ | -------------
bool IsInTransaction { get; } | Retorn if the action is being performed on a transaction context

### Methods

Signature    | Description
------------ | -------------
void BeginTransaction(); | Start a transaction context
void Commit(); | Performe the operation on your storage container and also commit the transaction if the operation is being performed on a transaction context
void Rollback(); | Rollback all performed operation on your storage container if the operation is eing performed on a transaction context
T GetRepositoryContext<T>() where T : class; | Return the unity of work context class 