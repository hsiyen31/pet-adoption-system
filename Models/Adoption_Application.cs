using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionSystem.Models
{
    [Table("Adoption_Application")]
    public class Adoption_Application
    {
        [Key]
        [Display(Name = "申請單編號")]
        public Guid uNo { get; set; }

        [Display(Name = "申請日期時間")]
        public DateTime? ADateTime { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "申請人 Email")]
        public string aMail { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "申請人姓名")]
        public string aName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "連絡電話")]
        public string phoneN { get; set; }

        [Required]
        [Display(Name = "申請狀態")]
        public int Astatus { get; set; }

        [Required]
        [Display(Name = "認養人 ID")]
        public Guid UId_user { get; set; }

        [Required]
        [Display(Name = "寵物 ID")]
        public Guid PId { get; set; }

        [Display(Name = "審核管理員 ID")]
        public Guid? UId_manager { get; set; }
    }
}