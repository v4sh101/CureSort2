using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CureSort2.Data;

namespace CureSort2.Migrations
{
    [DbContext(typeof(CureContext))]
    [Migration("20170329050525_Bin")]
    partial class Bin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CureSort2.Data.Bin", b =>
                {
                    b.Property<int>("BinID");

                    b.Property<string>("Name");

                    b.HasKey("BinID");

                    b.ToTable("Bin");
                });

            modelBuilder.Entity("CureSort2.Data.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<int>("BinID");

                    b.Property<string>("Brand");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("PicturePath");

                    b.HasKey("ID");

                    b.HasIndex("BinID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("CureSort2.Data.MedicalDevice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<int>("BinID");

                    b.Property<string>("Brand");

                    b.Property<string>("Description");

                    b.Property<string>("Manufacturer");

                    b.HasKey("ID");

                    b.HasIndex("BinID");

                    b.ToTable("MedicalDevice");
                });

            modelBuilder.Entity("CureSort2.Data.Item", b =>
                {
                    b.HasOne("CureSort2.Data.Bin", "Bin")
                        .WithMany()
                        .HasForeignKey("BinID")
                        .OnDelete(DeleteBehavior.Cascade);
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
