using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CureSort2.Migrations
{
    public partial class AddMedicalDevice4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalDeviceID",
                table: "MedicalDeviceLog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDeviceLog_MedicalDeviceID",
                table: "MedicalDeviceLog",
                column: "MedicalDeviceID");

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

            migrationBuilder.DropIndex(
                name: "IX_MedicalDeviceLog_MedicalDeviceID",
                table: "MedicalDeviceLog");

            migrationBuilder.DropColumn(
                name: "MedicalDeviceID",
                table: "MedicalDeviceLog");
        }
    }
}
