using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class change6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "HotelFeatures");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HotelFeatures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "HotelFeatures");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "HotelFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
