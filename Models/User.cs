using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetAdoptionSystem.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Display(Name = "使用者編號")]
        public Guid UId { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string uMail { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "角色")]
        public int Role { get; set; }

        [Required]
        [Display(Name = "驗證狀態")]
        public int VerifiedStatus { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "使用者姓名")]
        public string uName { get; set; }
    }
}