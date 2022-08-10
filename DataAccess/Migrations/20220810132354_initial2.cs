using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultPrice",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ChildPrice",
                table: "Contracts");

            migrationBuilder.CreateTable(
                name: "AMarkets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketId = table.Column<int>(type: "int", nullable: false),
                    ListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMarkets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AMarkets");

            migrationBuilder.AddColumn<float>(
                name: "AdultPrice",
                table: "Contracts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ChildPrice",
                table: "Contracts",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
