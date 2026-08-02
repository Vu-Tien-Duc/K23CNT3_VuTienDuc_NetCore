using Microsoft.EntityFrameworkCore;

namespace VtdBuoi07Lab08.Models
{
    public class VtdAppDbContext : DbContext
    {
        public VtdAppDbContext(DbContextOptions<VtdAppDbContext> options) : base(options)
        {
        }

        public DbSet<VtdAccount> VtdAccounts { get; set; }
        public DbSet<VtdBanner> VtdBanners { get; set; }
        public DbSet<VtdOrders> VtdOrders { get; set; }
        public DbSet<VtdCustomer> VtdCustomers { get; set; }
        public DbSet<VtdCategory> VtdCategories { get; set; }
        public DbSet<VtdProduct> VtdProducts { get; set; }
        public DbSet<VtdBlog> VtdBlogs { get; set; }
        public DbSet<VtdOrderDetail> VtdOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // khai báo thêm ràng buộc UNIQUE (đã cập nhật thuộc tính c.VtdName)
            modelBuilder.Entity<VtdCategory>()
                .HasIndex(c => c.VtdName)
                .IsUnique();

            // khai báo khóa chính trên nhiều trường cho bảng OrderDetail
            //modelBuilder.Entity<VtdOrderDetail>()
            //    .HasKey(c => new { c.VtdOrderId, c.VtdProductId })

            // Đã cập nhật thuộc tính c.VtdOrderId và c.VtdProductId
            modelBuilder.Entity<VtdOrderDetail>()
                .HasIndex(c => new { c.VtdOrderId, c.VtdProductId })
                .IsUnique();
        }
    }
}