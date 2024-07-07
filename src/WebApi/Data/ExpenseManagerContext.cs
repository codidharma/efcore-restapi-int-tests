using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Data
{
    public class ExpenseManagerContext(DbContextOptions<ExpenseManagerContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>(entity => 
            {
                entity
                .Property(prop => prop.DateModified)
                .HasDefaultValueSql("GETDATE()");

                entity
                .HasIndex(prop => prop.Name)
                .IsUnique();

                entity
                .Property(prop => prop.Name)
                .HasMaxLength(50);
            });
        }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    }
}
