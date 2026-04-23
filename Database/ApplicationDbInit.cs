using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Database;

public class ApplicationDbInit : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Sessions> Sessions { get; set; }

    public ApplicationDbInit(DbContextOptions<ApplicationDbInit> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.id);
            builder.Property(p => p.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.name)
                .HasColumnName("name")
                .IsRequired();
            builder.Property(p => p.password)
                .HasColumnName("password")
                .IsRequired();
            
        });
        modelBuilder.Entity<Account>(builder =>
        {
            builder.ToTable("Accounts");
            builder.HasKey(p => p.id);
            builder.Property(p => p.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            
            builder.HasOne(p => p.Users)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(f => f.userId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}