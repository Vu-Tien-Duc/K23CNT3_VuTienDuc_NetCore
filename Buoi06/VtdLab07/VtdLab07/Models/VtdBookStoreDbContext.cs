using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07.Models;

public partial class VtdBookStoreDbContext : DbContext
{
    public VtdBookStoreDbContext()
    {
    }

    public VtdBookStoreDbContext(DbContextOptions<VtdBookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VtdAccount> VtdAccounts { get; set; }

    public virtual DbSet<VtdBook> VtdBooks { get; set; }

    public virtual DbSet<VtdCategory> VtdCategories { get; set; }

    public virtual DbSet<VtdOrderBook> VtdOrderBooks { get; set; }

    public virtual DbSet<VtdOrderDetail> VtdOrderDetails { get; set; }

    public virtual DbSet<VtdPublisher> VtdPublishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-9MACGRF6\\VUDUC;Database=VtdBookStoreDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VtdAccount>(entity =>
        {
            entity.HasKey(e => e.VtdAccountId).HasName("PK__VtdAccou__E3337F2BD32EABAA");
        });

        modelBuilder.Entity<VtdBook>(entity =>
        {
            entity.HasKey(e => e.VtdBookId).HasName("PK__VtdBook__0770932F4E662871");

            entity.HasOne(d => d.VtdCategory).WithMany(p => p.VtdBooks).HasConstraintName("FK_VtdBook_VtdCategory");

            entity.HasOne(d => d.VtdPublisher).WithMany(p => p.VtdBooks).HasConstraintName("FK_VtdBook_VtdPublisher");
        });

        modelBuilder.Entity<VtdCategory>(entity =>
        {
            entity.HasKey(e => e.VtdCategoryId).HasName("PK__VtdCateg__D2DB63532C4D180F");
        });

        modelBuilder.Entity<VtdOrderBook>(entity =>
        {
            entity.HasKey(e => e.VtdOrderId).HasName("PK__VtdOrder__2829D6AB148EF681");

            entity.HasOne(d => d.VtdAccount).WithMany(p => p.VtdOrderBooks).HasConstraintName("FK_VtdOrderBook_VtdAccount");
        });

        modelBuilder.Entity<VtdOrderDetail>(entity =>
        {
            entity.HasKey(e => e.VtdOrderDetailId).HasName("PK__VtdOrder__BF06F0FBC897DF7F");

            entity.HasOne(d => d.VtdBook).WithMany(p => p.VtdOrderDetails).HasConstraintName("FK_VtdOrderDetail_Book");

            entity.HasOne(d => d.VtdOrder).WithMany(p => p.VtdOrderDetails).HasConstraintName("FK_VtdOrderDetail_Order");
        });

        modelBuilder.Entity<VtdPublisher>(entity =>
        {
            entity.HasKey(e => e.VtdPublisherId).HasName("PK__VtdPubli__E07AF95EC8335AF7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
