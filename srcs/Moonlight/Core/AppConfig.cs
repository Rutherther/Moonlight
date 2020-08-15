using Serilog;

namespace Moonlight.Core
{
    public delegate void LoggerConfig(LoggerConfiguration configuration);
    
    public class AppConfig
    {
        public string Database { get; set; } = "Moonlight/database.db";
        public IServiceConfiguration Configuration { get; set; }

        public LoggerConfig LoggerConfig { get; set; }
        
        public bool ReadOnlyDatabase { get; set; } = true;
    }
}