using Microsoft.AspNetCore.SignalR;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting;

namespace VtdLab04.Models
{
    public class VtdDataLocal
    {
        public static List<VtdPeople> _peoples = new List<VtdPeople>()
        {
            new VtdPeople(){Id=0,Name="Devmaster",Email="devmaster.edu.vn@gmail.com",Phone="0978611889",Address="25 Vũ Ngọc Phan",Avatar="/images/avatar/anh.jpg",Birthday=Convert.ToDateTime("2012/09/22"),Bio="Viện Công Nghệ Devmaster",Gender=0},
            new VtdPeople(){Id=1,Name="Trịnh Văn Chung",Email="chungtrinhj@gmail.com",Phone="0978611889",Address="25 Vũ Ngọc Phan", Avatar="/images/avatar/anh.jpg",Birthday=Convert.ToDateTime("1979/05/25"),Bio="Devmaster Academy",Gender=1},
            new VtdPeople(){Id=2,Name="Nguyễn Huy",Email="huynguyen@gmail.com",Phone="0912113113",Address="Gia lâm, hà nội", Avatar="/images/avatar/anhsv1.jpg",Birthday=Convert.ToDateTime("1999/02/12"),Bio="Viện Devmaster",Gender=1},
            new VtdPeople(){Id=3,Name="Tiểu Long Nữ",Email="longnutieu@gmail.com",Phone="0904001002",Address="Ba đình, hà nội", Avatar="/images/avatar/anhsv1.jpg",Birthday=Convert.ToDateTime("2000/02/02"),Bio="Nhân vật trong phim kiếm hiệp",Gender=2},
            new VtdPeople(){Id=4,Name="Pikachu",Email="chupika@gmail.com",Phone="0902114115",Address="Quang trung, hà đông", Avatar="/images/avatar/anhsv2.jpg",Birthday=Convert.ToDateTime("1997/12/12"),Bio="Nhân vật trong phim hoạt hình",Gender=2},
            new VtdPeople(){Id=5,Name="Pikachu",Email="chupika@gmail.com",Phone="0902114115",Address="Quang trung, hà đông", Avatar="/images/avatar/anhsv3.jpg",Birthday=Convert.ToDateTime("1997/12/12"),Bio="Nhân vật trong phim hoạt hình",Gender=2},
            new VtdPeople(){Id=6,Name="Vũ Tiến Đức",Email="vtd@gmail.com",Phone="0396705566",Address="Yên bình, yên bái", Avatar="/images/avatar/anh.jpg",Birthday=Convert.ToDateTime("2005/11/10"),Bio="Học viên Devmaster",Gender=1},
        };
/// <summary>
/// GetPeoples: lấy danh sách dữ liệu peoples
/// </summary>
/// <returns></returns>
public static List<VtdPeople> GetVtdPeople()
        {
            return _peoples;
        }
        /// <summary>
        /// GetPeopleById : Lấy đối tượng peoples theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>peoples</returns>
        public static VtdPeople? GetPeopleById(int Id)
        {
            var people = _peoples.FirstOrDefault(x => x.Id == Id);
            return people;
        }
    }
}
