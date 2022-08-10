using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "MaxCH",
                table: "RoomTypes",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<short>(
                name: "MaxAD",
                table: "RoomTypes",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MaxCH",
                table: "RoomTypes",
                type: "float",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<double>(
                name: "MaxAD",
                table: "RoomTypes",
                type: "float",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
