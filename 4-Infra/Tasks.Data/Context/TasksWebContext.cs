using Tasks.Domain.Domain;
using Tasks.Domain.Entidades.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks.Data.Context
{
    public partial class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options)
            : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Domain.Entidades.Tasks.Task> Tasks { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("Defa‌​ultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegistrarIndices(modelBuilder);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IBaseEntity entidade)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.CurrentValues["DataDeAlteracao"] = DateTime.Now;
                            break;

                        case EntityState.Added:
                            entry.CurrentValues["DataDeCadastro"] = DateTime.Now;
                            break;
                    }
                }
            }
        }

        private void RegistrarIndices(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Usuario>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}