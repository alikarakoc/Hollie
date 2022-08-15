using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContDay",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "CH14",
                table: "Contracts",
                newName: "CH3");

            migrationBuilder.RenameColumn(
                name: "CH07",
                table: "Contracts",
                newName: "CH2");

            migrationBuilder.AddColumn<double>(
                name: "CH1",
                table: "Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CH1",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "CH3",
                table: "Contracts",
                newName: "CH14");

            migrationBuilder.RenameColumn(
                name: "CH2",
                table: "Contracts",
                newName: "CH07");

            migrationBuilder.AddColumn<int>(
                name: "ContDay",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
