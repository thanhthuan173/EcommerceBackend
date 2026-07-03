using EcommerceBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Persistence
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(ci => new { ci.CartId, ci.ProductId });
                entity.HasOne(c => c.Cart).WithMany(ci => ci.CartItems).HasForeignKey(c => c.CartId);
                entity.HasOne(p => p.Product).WithMany(ci => ci.CartItems).HasForeignKey(p => p.ProductId);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => new { oi.OrderId, oi.ProductId });
                entity.HasOne(o => o.Order).WithMany(oi => oi.OrderItems).HasForeignKey(o => o.OrderId);
                entity.HasOne(p => p.Product).WithMany(oi => oi.OrderItems).HasForeignKey(p => p.ProductId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(cus => cus.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.HashPassword).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasOne(cus => cus.Cart).WithOne(ca => ca.User).HasForeignKey<Cart>(ca => ca.UserId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.HasOne(u => u.User).WithMany(ur => ur.UserRoles).HasForeignKey(u => u.UserId);
                entity.HasOne(r => r.Role).WithMany(ur => ur.UserRoles).HasForeignKey(r => r.RoleId);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(rt => rt.Token).IsUnique();
            });
        }
    }
}
