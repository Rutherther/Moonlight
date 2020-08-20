using System.IO;
using Microsoft.Data.Sqlite;
using Moonlight.Core;
using Moonlight.Database.DAL;

namespace Moonlight.Database
{
    internal class SqliteContextFactory : IContextFactory<MoonlightContext>
    {
        private readonly AppConfig _appConfig;

        public SqliteContextFactory(AppConfig appConfig) => _appConfig = appConfig;

        public MoonlightContext CreateContext()
        {
            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = Path.GetFullPath(_appConfig.Database),
                Mode = _appConfig.ReadOnlyDatabase ? SqliteOpenMode.ReadOnly : SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            };

            var connection = new SqliteConnection
            {
                ConnectionString = builder.ToString()
            };

            return new MoonlightContext(connection);
        }
    }
}