using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using TodoApp.Domain.Models;

namespace TodoApp.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
   public void Configure(EntityTypeBuilder<User> builder)
   {
      builder.ToTable("users");
      
      builder.HasKey(u => u.Id);

      builder.Property(u => u.Email)
         .IsRequired();

      builder.Property(u => u.Password)
         .IsRequired();
      
      builder.Property(u => u.Username)
         .IsRequired();

      builder.HasIndex(u => u.Email).IsUnique();
      
      builder.HasMany(u => u.Categories)
         .WithOne(c => c.User)
         .HasForeignKey(c => c.UserId);

      builder.HasMany(u => u.Tasks)
         .WithOne(t => t.User)
         .HasForeignKey(t => t.UserId);
   }
}