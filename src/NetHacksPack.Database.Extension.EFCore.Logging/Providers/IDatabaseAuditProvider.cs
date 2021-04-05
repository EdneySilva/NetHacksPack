using NetHacksPack.Database.Extension.EFCore.Logging.Infrastructure;

namespace NetHacksPack.Database.Extension.EFCore.Logging.Providers
{
    public delegate string SchemmaNameProvider();

    public interface IDatabaseAuditProvider
    {
        void Initialize();
    }
}
