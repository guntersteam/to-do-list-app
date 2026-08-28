using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoApp.Domain.Models;
using TodoApp.Persistence.Configurations;
using Task = TodoApp.Domain.Models.Task;

namespace TodoApp.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
   public DbSet<User> Users { get; set; }
   public DbSet<Category> Categories { get; set; }
   public DbSet<Task> Tasks { get; set; }
   public DbSet<TaskCategory> TaskCategories { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new CategoryConfiguration());
      modelBuilder.ApplyConfiguration(new TaskConfiguration());
      modelBuilder.ApplyConfiguration(new TaskCategoryConfiguration());
   }
}