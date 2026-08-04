using K23CNT3_VuTienDuc_2310900023.Models;
using Microsoft.EntityFrameworkCore;
// THÊM MỚI: Thư viện để dùng Cookie Authentication
using Microsoft.AspNetCore.Authentication.Cookies;

namespace K23CNT3_VuTienDuc_2310900023
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
 

            // THÊM DÒNG NÀY: Cho phép View (HTML) truy cập vào HttpContext để đọc Session
            builder.Services.AddHttpContextAccessor();
            // Đăng ký DbContext
            builder.Services.AddDbContext<VtdAppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("VtdAppDbConnection")));

            // THÊM MỚI: Cấu hình Cookie Authentication cho trang Admin
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/VtdAdmin/VtdAuth/Login"; // Đường dẫn tới trang đăng nhập
                    options.AccessDeniedPath = "/VtdAdmin/VtdAuth/AccessDenied"; // Trang báo lỗi khi không có quyền
                    options.ExpireTimeSpan = TimeSpan.FromHours(2); // Thời gian lưu cookie
                });

            // Cấu hình sử dụng session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".Vtd.Session";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/VtdHome/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // THÊM MỚI: Bật Middleware Authentication (BẮT BUỘC phải nằm TRƯỚC UseAuthorization)
            app.UseAuthentication();

            app.UseAuthorization();

            // Sử dụng session đã khai báo ở trên
            app.UseSession();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=VtdDashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=VtdHome}/{action=Index}/{id?}");

            app.Run();
        }
    }
}