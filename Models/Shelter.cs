using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetAdoptionSystem.Models
{
    [Table("Shelter")]
    public class Shelter
    {
        [Key]
        [Display(Name = "收容所編號")]
        public Guid SId { get; set; } // uniqueidentifier 對應 Guid

        [Required]
        [StringLength(50)]
        [Display(Name = "機構名稱")]
        public string OrgName { get; set; } // varchar(50)

        [Required]
        [StringLength(100)]
        [Display(Name = "地址")]
        public string Location { get; set; } // varchar(100)
    }
}
