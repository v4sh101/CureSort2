using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CureSort2.Models
{
    public class MedicalDeviceLog
    {
        public int ID { get; set; }
        public string ChangedBy { get; set; }
        public DateTime Date { get; set; }
        public string WhatChanged { get; set; }
        public string Old { get; set; }
        public string New { get; set; }


        public MedicalDevice MedicalDevice { get; set; }
    }
}
