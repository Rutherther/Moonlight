namespace Moonlight.Core
{
    internal class AppConfig
    {
        public string Database { get; set; } = "Moonlight/database.db";
        public IServiceConfiguration Configuration { get; set; }
    }
}