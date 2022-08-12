using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class change7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Pax",
                table: "RoomTypes",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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

            migrationBuilder.AddColumn<double>(
                name: "AdP",
                table: "RoomTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ChP",
                table: "RoomTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdP",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "ChP",
                table: "RoomTypes");

            migrationBuilder.AlterColumn<double>(
                name: "Pax",
                table: "RoomTypes",
                type: "float",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

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
