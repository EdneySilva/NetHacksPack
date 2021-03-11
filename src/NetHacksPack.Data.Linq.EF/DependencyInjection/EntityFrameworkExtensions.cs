using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Data.Linq.EF;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EntityFrameworkExtensions
    {
        public static IServiceCollection AddTransientUnityOfWork<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            return services
                .AddScoped(typeof(IQueryable<>), typeof(EfQuery<>));
        }
    }
}