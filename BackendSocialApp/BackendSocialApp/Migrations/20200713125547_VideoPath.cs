using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class VideoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoPath",
                table: "SurveyItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoPath",
                table: "NewsItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoPath",
                table: "SurveyItems");

            migrationBuilder.DropColumn(
                name: "VideoPath",
                table: "NewsItems");
        }
    }
}
