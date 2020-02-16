using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class NewsSchemaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Information",
                table: "MainFeeds",
                newName: "DetailInformation");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MainFeeds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ListNewsItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "MainFeeds");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ListNewsItems");

            migrationBuilder.RenameColumn(
                name: "DetailInformation",
                table: "MainFeeds",
                newName: "Information");
        }
    }
}
