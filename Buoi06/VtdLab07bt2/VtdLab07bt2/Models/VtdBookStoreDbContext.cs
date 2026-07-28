using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07bt2.Models;

public partial class VtdBookStoreDbContext : DbContext
{
    public VtdBookStoreDbContext()
    {
    }

    public VtdBookStoreDbContext(DbContextOptions<VtdBookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VtdBanner> VtdBanners { get; set; }

    public virtual DbSet<VtdBlog> VtdBlogs { get; set; }

    public virtual DbSet<VtdCategory> VtdCategories { get; set; }

    public virtual DbSet<VtdProduct> VtdProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-9MACGRF6\\VUDUC;Database=VtdBookStoreDB22;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VtdBanner>(entity =>
        {
            entity.HasKey(e => e.VtdId).HasName("PK__VtdBanne__35D9D0918A0AC4F3");

            entity.Property(e => e.VtdStatus).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<VtdBlog>(entity =>
        {
            entity.HasKey(e => e.VtdId).HasName("PK__VtdBlog__35D9D091A802A158");

            entity.Property(e => e.VtdCreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.VtdStatus).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<VtdCategory>(entity =>
        {
            entity.HasKey(e => e.VtdId).HasName("PK__VtdCateg__35D9D0915E8BDACA");

            entity.Property(e => e.VtdCreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.VtdStatus).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<VtdProduct>(entity =>
        {
            entity.HasKey(e => e.VtdId).HasName("PK__VtdProdu__35D9D0917DF90FA6");

            entity.Property(e => e.VtdCreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.VtdSalePrice).HasDefaultValue(0.0);
            entity.Property(e => e.VtdStatus).HasDefaultValue((byte)1);

            entity.HasOne(d => d.VtdCategory).WithMany(p => p.VtdProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VtdProduct_VtdCategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
