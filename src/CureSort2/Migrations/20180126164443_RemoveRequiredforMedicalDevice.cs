using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CureSort2.Migrations
{
    public partial class RemoveRequiredforMedicalDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Warehouse",
                table: "MedicalDevice",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MedicalDevice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Warehouse",
                table: "MedicalDevice",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MedicalDevice",
                nullable: false);
        }
    }
}
