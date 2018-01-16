using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CureSort2.Migrations
{
    public partial class UserKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flag",
                columns: table => new
                {
                    FlagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminID = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MedicalDeviceID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Problem = table.Column<string>(nullable: true),
                    Warehouse = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flag", x => x.FlagID);
                    table.ForeignKey(
                        name: "FK_Flag_MedicalDevice_MedicalDeviceID",
                        column: x => x.MedicalDeviceID,
                        principalTable: "MedicalDevice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserViewModel",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    FlagID = table.Column<int>(nullable: true),
                    MedicalDeviceID = table.Column<int>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewModel", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserViewModel_Flag_FlagID",
                        column: x => x.FlagID,
                        principalTable: "Flag",
                        principalColumn: "FlagID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserViewModel_MedicalDevice_MedicalDeviceID",
                        column: x => x.MedicalDeviceID,
                        principalTable: "MedicalDevice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserViewModel_FlagID",
                table: "UserViewModel",
                column: "FlagID");

            migrationBuilder.CreateIndex(
                name: "IX_UserViewModel_MedicalDeviceID",
                table: "UserViewModel",
                column: "MedicalDeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Flag_MedicalDeviceID",
                table: "Flag",
                column: "MedicalDeviceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserViewModel");

            migrationBuilder.DropTable(
                name: "Flag");
        }
    }
}
