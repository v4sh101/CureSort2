using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CureSort2.Migrations
{
    public partial class AddAnnotationstoFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Warehouse",
                table: "Flag",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Problem",
                table: "Flag",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Flag",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Flag",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Warehouse",
                table: "Flag",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Problem",
                table: "Flag",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Flag",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Flag",
                nullable: true);
        }
    }
}
