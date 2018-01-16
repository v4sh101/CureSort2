using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CureSort2.Migrations
{
    public partial class RemoveAd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserViewModel_Flag_FlagID",
                table: "UserViewModel");

            migrationBuilder.DropIndex(
                name: "IX_UserViewModel_FlagID",
                table: "UserViewModel");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Flag");

            migrationBuilder.DropColumn(
                name: "FlagID",
                table: "UserViewModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Flag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlagID",
                table: "UserViewModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserViewModel_FlagID",
                table: "UserViewModel",
                column: "FlagID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserViewModel_Flag_FlagID",
                table: "UserViewModel",
                column: "FlagID",
                principalTable: "Flag",
                principalColumn: "FlagID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
