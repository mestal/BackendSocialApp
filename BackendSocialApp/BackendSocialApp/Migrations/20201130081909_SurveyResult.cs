using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class SurveyResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SurveyResultItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "SurveyResultItems");
        }
    }
}
