using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace K23CNT3_VuTienDuc_BaiTX1.Models;

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

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-9MACGRF6\\VUDUC;Database=VtdBookStoreDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VtdAccount>(entity =>
        {
            entity.HasKey(e => e.VtdAccountId).HasName("PK__VtdAccou__E3337F2BD32EABAA");

            entity.ToTable("VtdAccount");

            entity.Property(e => e.VtdAccountId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.VtdAddress).HasMaxLength(512);
            entity.Property(e => e.VtdEmail)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.VtdFullName).HasMaxLength(100);
            entity.Property(e => e.VtdPassword)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.VtdPhone)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.VtdPicture).HasMaxLength(512);
            entity.Property(e => e.VtdUsername)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VtdBook>(entity =>
        {
            entity.HasKey(e => e.VtdBookId).HasName("PK__VtdBook__0770932F4E662871");

            entity.ToTable("VtdBook");

            entity.Property(e => e.VtdBookId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.VtdAuthor).HasMaxLength(100);
            entity.Property(e => e.VtdDescription).HasColumnType("ntext");
            entity.Property(e => e.VtdPicture).HasMaxLength(100);
            entity.Property(e => e.VtdTitle).HasMaxLength(200);

            entity.HasOne(d => d.VtdCategory).WithMany(p => p.VtdBooks)
                .HasForeignKey(d => d.VtdCategoryId)
                .HasConstraintName("FK_VtdBook_VtdCategory");

            entity.HasOne(d => d.VtdPublisher).WithMany(p => p.VtdBooks)
                .HasForeignKey(d => d.VtdPublisherId)
                .HasConstraintName("FK_VtdBook_VtdPublisher");
        });

        modelBuilder.Entity<VtdCategory>(entity =>
        {
            entity.HasKey(e => e.VtdCategoryId).HasName("PK__VtdCateg__D2DB63532C4D180F");

            entity.ToTable("VtdCategory");

            entity.Property(e => e.VtdCategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<VtdOrderBook>(entity =>
        {
            entity.HasKey(e => e.VtdOrderId).HasName("PK__VtdOrder__2829D6AB148EF681");

            entity.ToTable("VtdOrderBook");

            entity.Property(e => e.VtdOrderId)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.VtdAccountId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.VtdNote).HasMaxLength(512);
            entity.Property(e => e.VtdOrderDate).HasColumnType("datetime");
            entity.Property(e => e.VtdOrderReceive).HasColumnType("datetime");
            entity.Property(e => e.VtdReceiveAddress).HasMaxLength(512);
            entity.Property(e => e.VtdReceivePhone)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.VtdStatus)
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.HasOne(d => d.VtdAccount).WithMany(p => p.VtdOrderBooks)
                .HasForeignKey(d => d.VtdAccountId)
                .HasConstraintName("FK_VtdOrderBook_VtdAccount");
        });

        modelBuilder.Entity<VtdOrderDetail>(entity =>
        {
            entity.HasKey(e => e.VtdOrderDetailId).HasName("PK__VtdOrder__BF06F0FBC897DF7F");

            entity.ToTable("VtdOrderDetail");

            entity.Property(e => e.VtdBookId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.VtdOrderId)
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.HasOne(d => d.VtdBook).WithMany(p => p.VtdOrderDetails)
                .HasForeignKey(d => d.VtdBookId)
                .HasConstraintName("FK_VtdOrderDetail_Book");

            entity.HasOne(d => d.VtdOrder).WithMany(p => p.VtdOrderDetails)
                .HasForeignKey(d => d.VtdOrderId)
                .HasConstraintName("FK_VtdOrderDetail_Order");
        });

        modelBuilder.Entity<VtdPublisher>(entity =>
        {
            entity.HasKey(e => e.VtdPublisherId).HasName("PK__VtdPubli__E07AF95EC8335AF7");

            entity.ToTable("VtdPublisher");

            entity.Property(e => e.VtdAddress).HasMaxLength(200);
            entity.Property(e => e.VtdPhone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.VtdPublisherName).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
