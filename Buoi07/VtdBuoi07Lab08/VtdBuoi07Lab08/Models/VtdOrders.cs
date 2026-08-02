using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace VtdBuoi07Lab08.Models
{
    [Table("VtdOrders")]
    public class VtdOrders
    {
        [Key]
        public int VtdId { get; set; }

        public int VtdCustomerId { get; set; }

        [Display(Name = "Họ và tên người nhận")]
        public string VtdName { get; set; }

        [Display(Name = "Địa chỉ email người nhận")]
        public string VtdEmail { get; set; }

        [Display(Name = "Địa chỉ người nhận")]
        public string VtdAddress { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime VtdCreatedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public byte VtdStatus { get; set; }

        // Khóa ngoại tới bảng VtdCustomer
        public VtdCustomer VtdCustomer { get; set; }
    }
}