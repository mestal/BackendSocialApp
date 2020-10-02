using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class FalTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FortuneTellingType",
                table: "CoffeeFortuneTellings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserInput",
                table: "CoffeeFortuneTellings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FortuneTellingType",
                table: "CoffeeFortuneTellings");

            migrationBuilder.DropColumn(
                name: "UserInput",
                table: "CoffeeFortuneTellings");
        }
    }
}
