using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CureSort2.Data
{
    public class Bin
    {
        public int BinID { get; set; }

        [Display (Name="Bin#")]
        public string BinNumber { get; set; }
        public string Name { get; set; }
        public bool InUse { get; set; }

        public ICollection<MedicalDevice> MedicalDevices { get; set; }
    }
}