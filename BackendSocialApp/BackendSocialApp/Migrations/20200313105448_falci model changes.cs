using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class falcimodelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoffeeFortuneTellingPoint",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CoffeePoint",
                table: "AspNetUsers",
                newName: "CoffeePointPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoffeePointPrice",
                table: "AspNetUsers",
                newName: "CoffeePoint");

            migrationBuilder.AddColumn<float>(
                name: "CoffeeFortuneTellingPoint",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
