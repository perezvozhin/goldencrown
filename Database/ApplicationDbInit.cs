using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Database;

public class ApplicationDbInit : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Session> Sessions { get; set; }

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
        
        SeedUserData(userEntity);
        
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
        
        var sessionEntity = modelBuilder.Entity<Session>()
            .ToTable("sessions");
        sessionEntity.HasKey(x => x.Userid);
        sessionEntity.Property(x => x.Userid)
            .HasColumnName("userid")
            .IsRequired();
        sessionEntity.Property(x => x.Token)
            .HasColumnName("token")
            .IsRequired();
        sessionEntity.Property(x => x.ExpiresAt)
            .HasColumnName("expiresAt")
            .IsRequired();
        sessionEntity.HasOne<User>()
            .WithOne()
            .HasForeignKey<Session>(x => x.Userid)
            .OnDelete(DeleteBehavior.Cascade);
        
        var transactionEntity = modelBuilder.Entity<Transaction>()
            .ToTable("transactions");
        transactionEntity.HasKey(x => x.id);
        transactionEntity.Property(x => x.id)
            .HasColumnName("id")
            .UseIdentityColumn();
        transactionEntity.Property(x => x.Date)
            .HasColumnName("date")
            .IsRequired();
        transactionEntity.Property(x => x.Amount)
            .HasColumnName("amount")
            .IsRequired();
        transactionEntity.Property(x => x.ReceiverAccountId)
            .HasColumnName("receiver_AccountId")
            .IsRequired();
        transactionEntity.Property(x => x.SenderAccountId)
            .HasColumnName("sender_AccountId")
            .IsRequired();
        
        transactionEntity.HasOne<Account>()
            .WithMany()
            .HasForeignKey(x => x.SenderAccountId)
            .OnDelete(DeleteBehavior.Cascade);
        transactionEntity.HasOne<Account>()
            .WithMany()
            .HasForeignKey(x => x.ReceiverAccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }

    private void SeedUserData(EntityTypeBuilder<User> UserEntity)

    {
        UserEntity.HasData(
            new User
            {
                id = 1,
                login = "admin",
                password = "admin",
                name = "admin",
            },
            new User
            {
                id = 2,
                login = "user",
                password = "user",
                name = "user",
            }
        );
    }
}