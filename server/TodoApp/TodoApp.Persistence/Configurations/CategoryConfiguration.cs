using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Models;

namespace TodoApp.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
   public void Configure(EntityTypeBuilder<Category> builder)
   {
      builder.ToTable("categories");
      
      builder.HasKey(c => c.Id);

      builder.Property(c => c.Name)
         .IsRequired()
         .HasMaxLength(100);
      
      builder.HasOne(c => c.User)
         .WithMany(u => u.Categories)
         .HasForeignKey(c => c.UserId)
         .OnDelete(DeleteBehavior.Cascade);
      
      builder.HasMany(c => c.TaskCategories)
         .WithOne(t => t.Category)
         .HasForeignKey(t => t.CategoryId)
         .OnDelete(DeleteBehavior.Cascade);
   }
}