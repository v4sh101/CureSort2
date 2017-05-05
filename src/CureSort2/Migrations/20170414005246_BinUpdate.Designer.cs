using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CureSort2.Data;

namespace CureSort2.Migrations
{
    [DbContext(typeof(CureContext))]
    [Migration("20170414005246_BinUpdate")]
    partial class BinUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CureSort2.Data.Bin", b =>
                {
                    b.Property<int>("BinID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BinNumber");

                    b.Property<bool>("InUse");

                    b.Property<string>("Name");

                    b.HasKey("BinID");

                    b.ToTable("Bin");
                });

            modelBuilder.Entity("CureSort2.Data.MedicalDevice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<int>("BinID");

                    b.Property<string>("Brand");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 80);

                    b.Property<string>("Description");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("PhotoUrl")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("ID");

                    b.HasIndex("BinID");

                    b.ToTable("MedicalDevice");
                });

            modelBuilder.Entity("CureSort2.Data.MedicalDevice", b =>
                {
                    b.HasOne("CureSort2.Data.Bin", "Bin")
                        .WithMany("MedicalDevices")
                        .HasForeignKey("BinID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
