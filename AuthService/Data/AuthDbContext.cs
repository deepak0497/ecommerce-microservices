using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
 public class AuthDbContext : DbContext
 {
 public AuthDbContext(DbContextOptions<AuthDbContext> options)
 : base(options)
 {
 }

 public DbSet<User> Users { get; set; }

 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
 base.OnModelCreating(modelBuilder);

 modelBuilder.Entity<User>(entity =>
 {
 entity.HasKey(e => e.Id);
 entity.Property(e => e.Email).IsRequired();
 entity.Property(e => e.PasswordHash).IsRequired();
 entity.Property(e => e.Role).HasMaxLength(50);
 });
 }
 }
}
