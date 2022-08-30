using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirLineLibrary.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirLines",
                columns: table => new
                {
                    AirLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FromCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ToCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Fare = table.Column<int>(type: "int", nullable: false),
                    AirLineImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirLines", x => x.AirLineId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirLines_AirLineName",
                table: "AirLines",
                column: "AirLineName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirLines");
        }
    }
}
