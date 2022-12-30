using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


#nullable disable

namespace Xchanger_RestApi.Models
{
    public partial class XchangerDbContext : DbContext
    {
        public XchangerDbContext()
        {
        }

        public XchangerDbContext(DbContextOptions<XchangerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Exchange> Exchanges { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-VNCTSJ83\\SQLEXPRESS;Initial Catalog=XCHANGER; Trusted_Connection=True; User ID=sa;Password=***********");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Exchange>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcceptDate)
                    .HasColumnName("accept_date");
                    //.HasColumnType("date");

                entity.Property(e => e.InitiatorId).HasColumnName("initiator_id");

                entity.Property(e => e.Item2Id).HasColumnName("item_2_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.RequestDate)
                    .HasColumnName("request_date");
                    //.HasColumnType("date");

                entity.Property(e => e.State).HasColumnName("state");

                entity.HasOne(d => d.Initiator)
                    .WithMany(p => p.Exchanges)
                    .HasForeignKey(d => d.InitiatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Exchanges_Users");

                entity.HasOne(d => d.Items2)
                    .WithMany(p => p.ExchangeItems2)
                    .HasForeignKey(d => d.Item2Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Exchanges_Items2");

                entity.HasOne(d => d.Items)
                    .WithMany(p => p.ExchangeItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Exchanges_Items1");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(1700);

                entity.Property(e => e.IsNew).HasColumnName("is_new");

                entity.Property(e => e.PublicationDate)
                    .HasColumnName("publication_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(60);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Categories)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Items_Categories");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Items_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(60);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(40);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
