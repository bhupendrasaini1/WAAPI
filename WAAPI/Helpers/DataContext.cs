namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WAAPI.Helpers;

public partial class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

 

    public DbSet<Users> Users { get; set; }
    public DbSet<UserPurchases> UserPurchases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => new { e.Id });
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.Property(e => e.WindowsUser).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(250);
        });
        modelBuilder.Entity<UserPurchases>(entity =>
        {
            entity.ToTable("UserPurchases");
            entity.HasKey(e => new { e.Id });
            entity.Property(e => e.UserId).HasColumnName("Users_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.PurchaseAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Store).HasMaxLength(250);
            
        });
        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}