using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CureSort2.Migrations
{
    public partial class AddMedicalDevice7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_ID",
                table: "MedicalDeviceLog");

            migrationBuilder.DropIndex(
                name: "IX_MedicalDeviceLog_ID",
                table: "MedicalDeviceLog");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "MedicalDeviceLog");

            migrationBuilder.AddColumn<int>(
                name: "MedicalDeviceID",
                table: "MedicalDeviceLog",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "MedicalDeviceLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDeviceLog_ID",
                table: "MedicalDeviceLog",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_ID",
                table: "MedicalDeviceLog",
                column: "ID",
                principalTable: "MedicalDevice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
