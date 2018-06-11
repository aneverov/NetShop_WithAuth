using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NetShop_With_Auth.Migrations
{
    public partial class RmvPassFieldFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OldPassword",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ChangePasswordModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OldPassword = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PasswordConfirm = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangePasswordModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangePasswordModel");

            migrationBuilder.DropTable(
                name: "UserViewModel");

            migrationBuilder.AddColumn<string>(
                name: "NewPassword",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldPassword",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
