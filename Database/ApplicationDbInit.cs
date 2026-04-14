using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Database;

public class ApplicationDbInit : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<session> sessions { get; set; }

    public ApplicationDbInit(DbContextOptions<ApplicationDbInit> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.id);
    }
}