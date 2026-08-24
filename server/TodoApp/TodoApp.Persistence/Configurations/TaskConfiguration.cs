using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TodoApp.Domain.Models.Task>
{
   public void Configure(EntityTypeBuilder<TodoApp.Domain.Models.Task> builder)
   {
      builder.ToTable("tasks");
      
      builder.HasKey(t => t.Id);

      builder.Property(t => t.Title)
         .IsRequired()
         .HasMaxLength(200);

      builder.Property(t => t.Note)
         .HasMaxLength(3000)
         .IsRequired(false);

      builder.Property(t => t.IsCompleted)
         .HasDefaultValue(false);
      
      builder.Property(t => t.CreatedAt)
         .HasDefaultValueSql("now() at time zone 'utc'");

      builder.HasOne(t => t.User)
         .WithMany(u => u.Tasks)
         .HasForeignKey(t => t.UserId)
         .OnDelete(DeleteBehavior.Cascade);
         
      builder.HasOne(t => t.Category)
         .WithMany(c => c.Tasks)
         .HasForeignKey(t => t.CategoryId)
         .OnDelete(DeleteBehavior.SetNull);
   }
}