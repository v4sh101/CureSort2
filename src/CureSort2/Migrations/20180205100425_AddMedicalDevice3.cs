using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CureSort2.Migrations
{
    public partial class AddMedicalDevice3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalDeviceLog",
                columns: table => new
                {
                    Key = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChangedBy = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    New = table.Column<string>(nullable: true),
                    Old = table.Column<string>(nullable: true),
                    WhatChanged = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDeviceLog", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalDeviceLog");
        }
    }
}
