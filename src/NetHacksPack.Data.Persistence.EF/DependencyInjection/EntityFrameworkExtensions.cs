using NetHacksPack.Data.Persistence.Abstractions;
using NetHacksPack.Data.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EntityFrameworkExtensions
    {
        public static IServiceCollection AddTransientUnityOfWork<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            return services
                .AddTransient<IUnityOfWork, UnityOfWork<TDbContext>>()
                .AddTransient<IUnityOfWork<TDbContext>, UnityOfWork<TDbContext>>();
        }
    }
}