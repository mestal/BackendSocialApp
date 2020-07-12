using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class newstitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NewsItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "NewsItems");
        }
    }
}
