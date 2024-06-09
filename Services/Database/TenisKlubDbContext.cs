using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Entities;

namespace Services.Database;

public class TenisKlubDbContext : DbContext
{
    public TenisKlubDbContext(DbContextOptions<TenisKlubDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Item> Item { get; set; } = null!;
    public virtual DbSet<Court> Court { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Country> Country { get; set; } = null!;
    public virtual DbSet<Notification> Notification { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<OrderDetails> OrderDetails { get; set; } = null!;
    public virtual DbSet<Service> Service { get; set; } = null!;
    public virtual DbSet<Reservation> Reservation { get; set; } = null!;
    public virtual DbSet<Review> Review { get; set; } = null!;
    public virtual DbSet<Result> Result { get; set; } = null!;
    public virtual DbSet<ImageModel> ImageModel { get; set; } = null!;
    public virtual DbSet<NotificationRabbit> NotificationRabbit { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetails>(ConfigureOrderDetails);
        modelBuilder.Entity<Service>(ConfigureService);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Service)
            .WithMany()
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.Cascade); 
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void ConfigureOrderDetails(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.HasKey(od => new { od.OrderId, od.ItemId });
    }
    
    private void ConfigureService(EntityTypeBuilder<Service> builder)
    {
        builder.HasOne(s => s.Coach)
            .WithMany()
            .HasForeignKey(s => s.CoachId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(s => s.Court)
            .WithMany()
            .HasForeignKey(s => s.CourtId)
            .OnDelete(DeleteBehavior.Cascade);  
    }

}