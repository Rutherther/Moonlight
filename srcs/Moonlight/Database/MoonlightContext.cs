using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Moonlight.Database.Entities;

namespace Moonlight.Database
{
    internal class MoonlightContext : DbContext
    {
        private readonly DbConnection _connection;

        public MoonlightContext(DbConnection connection)
        {
            _connection = connection;
            Database.EnsureCreated();
        }

        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Entities.Translation> Translations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connection);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Skip shadow types
                if (entityType.ClrType == null)
                {
                    continue;
                }

                entityType.SetTableName(entityType.ClrType.Name);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}