using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RustDetector.api.DataMigrations
{
    public partial class InitialCreate : Migration
    {
        // Define the changes to be made when migrating up
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobDataSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", precision: 2, nullable: false),
                    Year = table.Column<int>(type: "int", precision: 4, nullable: false),
                    RustCount = table.Column<int>(type: "int", nullable: false),
                    GoCount = table.Column<int>(type: "int", nullable: false),
                    PythonCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDataSet", x => x.Id);
                });
        }
        // Define the changes to be made when rolling back migrations
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobDataSet");
        }
    }
}
