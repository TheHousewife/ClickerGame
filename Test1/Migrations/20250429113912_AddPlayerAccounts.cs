using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test1.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentCount = table.Column<double>(type: "REAL", nullable: false),
                    AutoClickerAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    AutoClickerLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    AutoClickerStrength = table.Column<int>(type: "INTEGER", nullable: false),
                    ClickStrength = table.Column<int>(type: "INTEGER", nullable: false),
                    ClickMultiplier = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
