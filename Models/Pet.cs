using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionSystem.Models
{
    [Table("Pet")]
    public class Pet
    {
        [Display(Name = "照片")]
        public string? Photo { get; set; }

        [Key]
        [Display(Name = "寵物編號")]
        public Guid PId { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "種類")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "性別")]
        public int Gender { get; set; }

        [Required]
        [Display(Name = "年齡")]
        public int Age { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "體型")]
        public string Size { get; set; }

        [Display(Name = "健康紀錄")]
        public string? HealthRecord { get; set; }

        [Required]
        [Display(Name = "目前狀態")]
        public int pStatus { get; set; }

        [Required]
        [Display(Name = "收容所編號")]
        public Guid SId { get; set; }

        [Display(Name = "寵物名字")]
        [StringLength(50)]
        public string? PetName { get; set; }
    }
}