using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Models;

namespace TodoApp.Persistence.Configurations;

public class TaskCategoryConfiguration : IEntityTypeConfiguration<TaskCategory>
{
   public void Configure(EntityTypeBuilder<TaskCategory> builder)
   {
      builder.ToTable("task_categories");
      
      builder.HasKey(t => new { t.TaskId, t.CategoryId });
   }
}