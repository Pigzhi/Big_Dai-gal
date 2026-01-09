using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API大專.Models;

public partial class ProxyContext : DbContext
{
    public ProxyContext()
    {
    }

    public ProxyContext(DbContextOptions<ProxyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Commission> Commissions { get; set; }

    public virtual DbSet<CommissionHistory> CommissionHistories { get; set; }

    public virtual DbSet<CommissionOrder> CommissionOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commission>(entity =>
        {
            entity.HasKey(e => e.CommissionId).HasName("PK__Commissi__D19D7CC9A721000E");

            entity.ToTable("Commission");

            entity.Property(e => e.CommissionId).HasColumnName("commission_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatorId)
                .HasMaxLength(50)
                .HasColumnName("creator_id");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EscrowAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("escrowAmount");
            entity.Property(e => e.FailCount).HasColumnName("fail_count");
            entity.Property(e => e.Fee)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("fee");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("未審核")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.Commissions)
                .HasForeignKey(d => d.CreatorId)
                .HasConstraintName("FK_Commission_Users_Creator");
        });

        modelBuilder.Entity<CommissionHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Commissi__096AA2E939BE86B4");

            entity.ToTable("CommissionHistory");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("changed_at");
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(50)
                .HasColumnName("changed_by");
            entity.Property(e => e.CommissionId).HasColumnName("commission_id");
            entity.Property(e => e.NewData).HasColumnName("new_data");
            entity.Property(e => e.OldData).HasColumnName("old_data");

            entity.HasOne(d => d.Commission).WithMany(p => p.CommissionHistories)
                .HasForeignKey(d => d.CommissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommissionHistory_Commission");
        });

        modelBuilder.Entity<CommissionOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__4659622962A69E3E");

            entity.ToTable("CommissionOrder");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(50)
                .HasColumnName("buyer_id");
            entity.Property(e => e.CommissionId).HasColumnName("commission_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FinishedAt)
                .HasColumnType("datetime")
                .HasColumnName("finished_at");
            entity.Property(e => e.SellerId)
                .HasMaxLength(50)
                .HasColumnName("seller_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.Commission).WithMany(p => p.CommissionOrders)
                .HasForeignKey(d => d.CommissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Commission");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__Users__DD701264B453AD75");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616411AF85A7").IsUnique();

            entity.Property(e => e.Uid)
                .HasMaxLength(50)
                .HasColumnName("uid");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
