using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "RoomTypes",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "Markets",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "Hotels",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "Countries",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "Boards",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "Agencies",
                newName: "UpdatedUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUser",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpdatedUser",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "RoomTypes",
                newName: "UpdateUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "Markets",
                newName: "UpdateUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "Hotels",
                newName: "UpdateUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "Countries",
                newName: "UpdateUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "Boards",
                newName: "UpdateUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "Agencies",
                newName: "UpdateUser");
        }
    }
}
