using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class UserStarPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserStarPoint",
                table: "CoffeeFortuneTellings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserStarPointCount",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserStarPointTotal",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStarPoint",
                table: "CoffeeFortuneTellings");

            migrationBuilder.DropColumn(
                name: "UserStarPointCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserStarPointTotal",
                table: "AspNetUsers");
        }
    }
}
