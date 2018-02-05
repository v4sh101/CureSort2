using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CureSort2.Migrations
{
    public partial class AddMedicalDeviceLogRepository2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalDeviceLog",
                table: "MedicalDeviceLog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalDeviceLogs",
                table: "MedicalDeviceLog",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDeviceLogs_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLog",
                column: "MedicalDeviceID",
                principalTable: "MedicalDevice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_MedicalDeviceLog_MedicalDeviceID",
                table: "MedicalDeviceLog",
                newName: "IX_MedicalDeviceLogs_MedicalDeviceID");

            migrationBuilder.RenameTable(
                name: "MedicalDeviceLog",
                newName: "MedicalDeviceLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDeviceLogs_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalDeviceLogs",
                table: "MedicalDeviceLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalDeviceLog",
                table: "MedicalDeviceLogs",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDeviceLog_MedicalDevice_MedicalDeviceID",
                table: "MedicalDeviceLogs",
                column: "MedicalDeviceID",
                principalTable: "MedicalDevice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_MedicalDeviceLogs_MedicalDeviceID",
                table: "MedicalDeviceLogs",
                newName: "IX_MedicalDeviceLog_MedicalDeviceID");

            migrationBuilder.RenameTable(
                name: "MedicalDeviceLogs",
                newName: "MedicalDeviceLog");
        }
    }
}
