using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NetHacksPack.Data.Linq.EF
{
    public interface IQueryable<TEntity, TDbContext> : IQueryable<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {

    }
}
