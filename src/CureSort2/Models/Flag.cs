using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CureSort2.Models
{
    public class Flag
    {
        public int FlagID { get; set; }
        public int MedicalDeviceID { get; set; }
        [Required]
        [Display(Name = "Submitted By")]
        public string Name { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string Warehouse { get; set; }
        [Required]
        public string Problem { get; set; }
        [Display(Name = "Under Review")]
        public bool IsActive { get; set; }
        public string ReviewedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateSubmitted { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateReviewed { get; set; }

        public MedicalDevice MedicalDevice { get; set; }
    }
}
