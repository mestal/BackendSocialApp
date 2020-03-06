using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class usernewfiedls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoffeFortuneTellingCount",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CoffeeFortuneTellingPoint",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoffeFortuneTellingCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoffeeFortuneTellingPoint",
                table: "AspNetUsers");
        }
    }
}
