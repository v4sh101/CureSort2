using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CureSort2.Migrations
{
    public partial class AddMedicalDeviceLogRepository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalDeviceLog",
                table: "MedicalDeviceLog");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "MedicalDeviceLog");

            migrationBuilder.AddColumn<long>(
                name: "Key",
                table: "MedicalDeviceLog",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalDeviceLog",
                table: "MedicalDeviceLog",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalDeviceLog",
                table: "MedicalDeviceLog");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "MedicalDeviceLog");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "MedicalDeviceLog",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalDeviceLog",
                table: "MedicalDeviceLog",
                column: "ID");
        }
    }
}
