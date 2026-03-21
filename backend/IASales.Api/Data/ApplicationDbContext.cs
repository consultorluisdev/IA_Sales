using Microsoft.EntityFrameworkCore;
using IASales.Api.Entities;

namespace IASales.Api.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

// existend 
  public DbSet<User> Users { get; set; }

  public DbSet<Product> Products { get; set; }

  public DbSet<Lead> Leads { get; set; }

// novos
  public DbSet<Tenant> Tenants { get; set; }

  public DbSet<Customer> Customers { get; set; }

  public DbSet<Order> Orders { get; set; }

  public DbSet<Campaing> Campaigns { get; set; }

  public DbSet<IAConversation> AIConversations { get; set; }

  public DbSet<VirtualTryOn> VirtualTryOns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(e =>
        {
          e.HasKey(x => x.Id);
          e.HasIndex(x => x.Slug).IsUnique();
        });
        // customer
        modelBuilder.Entity<Customer>(e =>
        {
          e.HasKey(x => x.Id);
          e.HasIndex(x => x.TenantId);
          e.Property(x => x.Interests).HasColumnType("jsonb");
          e.HasOne(x => x.Tenant)
          .WithMany(t => t.Customer)
          .HasForeignKey(x => x.TenantId);
        });

        // Order
        modelBuilder.Entity<Order>(e =>
        {
          e.HasKey(x => x.Id);
          e.HasIndex(x => x.TenantId);
          e.Property(x => x.Items).HasColumnType("jsonb");
          e.HasOne(x => x.Customer)
          .WithMany(c => c.Orders)
          .HasForeignKey(x => x.CustomerId);
        });
        // Campaign
        modelBuilder.Entity<Campaing>(e =>
        {
          e.HasKey(x => x.Id);
          e.HasIndex(x => x.TenantId);
        });
        // IAConversation
        modelBuilder.Entity<IAConversation>(e =>
        {
          e.HasKey(x => x.Id);
          e.Property(x => x.Messages).HasColumnType("jsonb");
    });
    // virtualTryOn
    modelBuilder.Entity<VirtualTryOn>(e =>
    {
      e.HasKey(x => x.Id);
    });
  }

}