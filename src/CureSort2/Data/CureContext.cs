using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CureSort2.Models;
using Microsoft.EntityFrameworkCore;

namespace CureSort2.Data
{
    public class CureContext : DbContext
    {
        public CureContext(DbContextOptions<CureContext> options) : base(options)
        {
        }

        public DbSet<Bin> Bins { get; set; }
        public DbSet<MedicalDevice> MedicalDevices { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<MedicalDeviceLog> MedicalDeviceLogs {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bin>().ToTable("Bin");
            modelBuilder.Entity<MedicalDevice>().ToTable("MedicalDevice");
            modelBuilder.Entity<Flag>().ToTable("Flag");
            modelBuilder.Entity<MedicalDeviceLog>().ToTable("MedicalDeviceLog");
        }
    }
}
