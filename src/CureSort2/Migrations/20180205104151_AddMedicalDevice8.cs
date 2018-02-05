using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CureSort2.Migrations
{
    public partial class AddMedicalDevice8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLog");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalDeviceID",
                table: "MedicalDeviceLog",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLog",
                column: "MedicalDeviceID",
                principalTable: "MedicalDevice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLog");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalDeviceID",
                table: "MedicalDeviceLog",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLog",
                column: "MedicalDeviceID",
                principalTable: "MedicalDevice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
