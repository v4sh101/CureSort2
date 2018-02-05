using CureSort2.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CureSort2.Models
{
    public class MedicalDevice
    {
        public int ID { get; set; }
        [StringLength(80)]
        public string CreatedBy { get; set; }
        public string Barcode { get; set; }
        public int BinID { get; set; }
        public string Manufacturer { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        [StringLength(200)]
        public string PhotoUrl { get; set; }
        public bool IsApproved { get; set; }
        [Display(Name = "Submitted By")]
        public string Name { get; set; }
        public string Warehouse { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateSubmitted { get; set; }

        public Bin Bin { get; set; }
        public List<UserViewModel> Admins { get; set; }
        public ICollection<Flag> Flags { get; set; }
    }
}

