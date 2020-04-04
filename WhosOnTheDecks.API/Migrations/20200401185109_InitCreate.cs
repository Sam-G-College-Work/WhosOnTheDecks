using Microsoft.EntityFrameworkCore.Migrations;

namespace WhosOnTheDecks.API.Migrations
{
    public partial class InitCreate : Migration
    {
        //Up method applies changes to the database 
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false) // Sets Column Id to be an int and not nullable
                        .Annotation("Sqlite:Autoincrement", true), // applies an auto increment to the Id column
                    Name = table.Column<string>(nullable: true) // Adds a column Name of type string that can be nullable
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });
        }

        // Down method drops the table callled values
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Values");
        }
    }
}
