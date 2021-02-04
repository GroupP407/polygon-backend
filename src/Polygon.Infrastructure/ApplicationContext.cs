using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Polygon.Domain.Entities;

namespace Polygon.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public virtual DbSet<FormSchema> FormSchemas { get; set; }
        public virtual DbSet<FormData> FormDatas { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Polygon.Infrastructure"));
        }
    }
}