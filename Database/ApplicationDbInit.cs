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
        var userEntity = modelBuilder.Entity<User>()
            .ToTable("users");
        userEntity.HasKey(x => x.id);
        userEntity.Property(x => x.id)
            .HasColumnName("id")
            .UseIdentityColumn();
        userEntity.Property(x => x.login)
            .HasColumnName("login")
            .IsRequired();
        userEntity.Property(x => x.password)
            .HasColumnName("password")
            .IsRequired();

        
        var accountEntity = modelBuilder.Entity<Account>()
            .ToTable("accounts");
        
        accountEntity.HasKey(x => x.id);
        accountEntity.Property(x => x.id)
            .HasColumnName("id")
            .UseIdentityColumn();
        accountEntity.Property(x => x.balance)
            .HasColumnName("balance")
            .IsRequired();
        accountEntity.HasOne<User>()
            .WithOne()
            .HasForeignKey<Account>(x => x.id)
            .OnDelete(DeleteBehavior.Cascade);
        
        var sessionEntity = modelBuilder.Entity<Sessions>()
            .ToTable("sessions");
        sessionEntity.HasKey(x => x.userid);
        sessionEntity.Property(x => x.userid)
            .HasColumnName("userid")
            .IsRequired();
        sessionEntity.Property(x => x.token)
            .HasColumnName("token")
            .IsRequired();
        sessionEntity.Property(x => x.expiresAt)
            .HasColumnName("expiresAt")
            .IsRequired();
        sessionEntity.HasOne<User>()
            .WithOne()
            .HasForeignKey<Sessions>(x => x.userid)
            .OnDelete(DeleteBehavior.Cascade);
        
        var transactionEntity = modelBuilder.Entity<Transaction>()
            .ToTable("transactions");
        transactionEntity.HasKey(x => x.id);
        transactionEntity.Property(x => x.id)
            .HasColumnName("id")
            .UseIdentityColumn();
        transactionEntity.Property(x => x.date)
            .HasColumnName("date")
            .IsRequired();
        transactionEntity.Property(x => x.amount)
            .HasColumnName("amount")
            .IsRequired();
        transactionEntity.Property(x => x.receiverAccountId)
            .HasColumnName("receiver_AccountId")
            .IsRequired();
        transactionEntity.Property(x => x.senderAccountId)
            .HasColumnName("sender_AccountId")
            .IsRequired();
        
        transactionEntity.HasOne<Account>()
            .WithMany()
            .HasForeignKey(x => x.senderAccountId)
            .OnDelete(DeleteBehavior.Cascade);
        transactionEntity.HasOne<Account>()
            .WithMany()
            .HasForeignKey(x => x.receiverAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }   
}