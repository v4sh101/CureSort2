using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CureSort2.Data;

namespace CureSort2.Migrations
{
    [DbContext(typeof(CureContext))]
    [Migration("20180201190936_AddChangeLog")]
    partial class AddChangeLog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CureSort2.Models.AccountViewModels.UserViewModel", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<int?>("MedicalDeviceID");

                    b.Property<string>("RoleName");

                    b.Property<string>("Username");

                    b.HasKey("UserID");

                    b.HasIndex("MedicalDeviceID");

                    b.ToTable("UserViewModel");
                });

            modelBuilder.Entity("CureSort2.Models.Bin", b =>
                {
                    b.Property<int>("BinID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BinNumber");

                    b.Property<bool>("InUse");

                    b.Property<string>("Name");

                    b.HasKey("BinID");

                    b.ToTable("Bin");
                });

            modelBuilder.Entity("CureSort2.Models.Flag", b =>
                {
                    b.Property<int>("FlagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments")
                        .IsRequired();

                    b.Property<DateTime>("DateReviewed");

                    b.Property<DateTime>("DateSubmitted");

                    b.Property<bool>("IsActive");

                    b.Property<int>("MedicalDeviceID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Problem")
                        .IsRequired();

                    b.Property<string>("ReviewedBy");

                    b.Property<string>("Warehouse")
                        .IsRequired();

                    b.HasKey("FlagID");

                    b.HasIndex("MedicalDeviceID");

                    b.ToTable("Flag");
                });

            modelBuilder.Entity("CureSort2.Models.MedicalDevice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<int>("BinID");

                    b.Property<string>("Brand");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 80);

                    b.Property<DateTime>("DateSubmitted");

                    b.Property<string>("Description");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Name");

                    b.Property<string>("PhotoUrl")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Warehouse");

                    b.HasKey("ID");

                    b.HasIndex("BinID");

                    b.ToTable("MedicalDevice");
                });

            modelBuilder.Entity("CureSort2.Models.AccountViewModels.UserViewModel", b =>
                {
                    b.HasOne("CureSort2.Models.MedicalDevice")
                        .WithMany("Admins")
                        .HasForeignKey("MedicalDeviceID");
                });

            modelBuilder.Entity("CureSort2.Models.Flag", b =>
                {
                    b.HasOne("CureSort2.Models.MedicalDevice", "MedicalDevice")
                        .WithMany("Flags")
                        .HasForeignKey("MedicalDeviceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CureSort2.Models.MedicalDevice", b =>
                {
                    b.HasOne("CureSort2.Models.Bin", "Bin")
                        .WithMany("MedicalDevices")
                        .HasForeignKey("BinID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
