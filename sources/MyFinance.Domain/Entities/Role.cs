using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Domain
{
    public class Role
    {
        [Key]
        [Display(Name = "Role Name")]
        [Required]
       // [StringLength]
        public string RoleName { get; set; }
       // public virtual ICollection<User> Users { get; set; }
    }

    public class Ticket {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenKhachHang { get; set; }
        [MaxLength(500)]
        public string TenKhachDat { get; set; }
        [MaxLength(500)]
        public string CodeVe { get; set; }
        [MaxLength(500)]
        public string SoVe { get; set; }
        [MaxLength(500)]
        public string ChieuDi{ get; set; }
        [MaxLength(500)]
        public string ChieuVe { get; set; }
        [MaxLength(100)]
        public string HangBay { get; set; }
       
        public DateTime NgayBay { get; set; }
        public DateTime NgayDat { get; set; }
         
        public decimal TongTien{ get; set; }
        public decimal TongPhaiTra{ get; set; }
        public decimal TongThuKhach { get; set; }
        public decimal ChietKhauCTV { get; set; }
        public decimal PhiDaiLy { get; set; }
        public decimal PhiGioiThieu { get; set; }
        public decimal ChietKhauF1 { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }



        
    }
}
