using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tactoro.QuantDev.Models
{
    public partial class QuantDevDBContext : DbContext
    {
        public QuantDevDBContext(DbContextOptions<QuantDevDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost;Database=QuantDevDB;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "PPL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__Customer__Manage__339FAB6E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__UserID__32AB8735");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager", "PPL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Manager)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Manager__UserID__2FCF1A8A");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "PPL");

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__User__C9F28456F971ACC6")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Alias).HasMaxLength(1000);

                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.FirstName).HasMaxLength(1000);

                entity.Property(e => e.LastName).HasMaxLength(1000);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
