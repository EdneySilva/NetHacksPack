namespace NetHacksPack.Hosting.Abstractions.Constants
{
    public static class Prefixies
    {
        public const string ENVIRONMENT_PREFIX = "NETCORE_";
        public const string ENVIRONMENT_NAME = "ASPNETCORE_ENVIRONMENT";
        public const string APPSETTINGS_JSON = "appsettings.json";
        public const string APPSETTINGS_ENV_JSON = "appsettings.{0}.json";
        public const string APPSETTINGS_DATABASE_JSON = "appsettings.database.json";
        public const string APPSETTINGS_EVENT_BUS_JSON = "appsettings.event-bus.json";
        public const string APPSETTINGS_LOG_JSON = "appsettings.log.json";
        public const string APPSETTINGS_CACHE_JSON = "appsettings.cache.json";
    }
}
