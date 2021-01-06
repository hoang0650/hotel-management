using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using MyFinance.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using MyFinance.Domain.Entities;

namespace MyFinance.Domain
{
    public class User 
    {
       
        [Display(Name = "User Name")]
        [MaxLength(100)]
        public string UserName { get; set; }

        
        [Key]
        [Required]
        public int Id { get; set; }

        public int HotelId { get; set; }

        public bool IsOwner { get; set; }
        public UserType UserType { get; set; }
        //[Required]
        //[MaxLength(64)]
        //public byte[] PasswordHash { get; set; }

        
        [MaxLength(200)]
        public string Password { get; set; }
        [MaxLength(200)]
        public string FullName { get; set; }

        
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Comment { get; set; }

        [Display(Name = "Approved?")]
        public bool IsApproved { get; set; }

        
        public bool IsActive { get; set; }
        /// <summary>
        /// Khoa tam thoi khi user login, de khong cho user khác dang nhap
        /// </summary>
        public bool IsLocked { get; set; }
        public bool IsInShift { get; set; }

        public bool IsDeteled { get; set; }

        [Display(Name = "Crate Date")]
        public DateTime DateCreated { get; set; }

        
        public Nullable< DateTime> DateExpired { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime? DateLastLogin { get; set; }

        [Display(Name = "Last Activity Date")]
        public DateTime? DateLastActivity { get; set; }

        [Display(Name = "Last Password Change Date")]
        public DateTime DateLastPasswordChange { get; set; }

       // public virtual ICollection<Role> Roles { get; set; }

    }


    public class OwnerHotel
    {
        [Key]
        [Required]
        public int Id { get; set; }
       
        public int HotelId { get; set; }
        public int UserOwnerId { get; set; }
        public bool IsSelected { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        [ForeignKey("UserOwnerId")]
        public virtual User User { get; set; }
    }
    public class InsideUser
    {

        [Display(Name = "User Name")]
        [MaxLength(100)]
        public string UserName { get; set; }


        [Key]
        [Required]
        public int Id { get; set; }


        public bool IsOwner { get; set; }
        //[Required]
        //[MaxLength(64)]
        //public byte[] PasswordHash { get; set; }


        [MaxLength(200)]
        public string Password { get; set; }
        [MaxLength(200)]
        public string FullName { get; set; }


        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Comment { get; set; }

        [Display(Name = "Approved?")]
        public bool IsApproved { get; set; }


        public bool IsActive { get; set; }

        public bool IsDeteled { get; set; }

        [Display(Name = "Crate Date")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime? DateLastLogin { get; set; }

        [Display(Name = "Last Activity Date")]
        public DateTime? DateLastActivity { get; set; }

        [Display(Name = "Last Password Change Date")]
        public DateTime DateLastPasswordChange { get; set; }

        // public virtual ICollection<Role> Roles { get; set; }

    }
}