using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class News : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "CoffeeFortuneTellings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoffeePoint",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainFeeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MainPhoto = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    PublishedDateUtc = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    PicturePath = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true),
                    SurveyType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainFeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListNewsItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ListNewsId = table.Column<Guid>(nullable: true),
                    PicturePath = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListNewsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListNewsItems_MainFeeds_ListNewsId",
                        column: x => x.ListNewsId,
                        principalTable: "MainFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SurveyId = table.Column<Guid>(nullable: true),
                    PicturePath = table.Column<string>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    QuestionWeight = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    MaxSelectableAnswerNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyItems_MainFeeds_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "MainFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResultItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Point = table.Column<int>(nullable: false),
                    PicturePath = table.Column<string>(nullable: true),
                    ResultInformation = table.Column<string>(nullable: true),
                    SurveyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResultItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResultItems_MainFeeds_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "MainFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyItemAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SurveyItemId = table.Column<Guid>(nullable: true),
                    PicturePath = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    AnswerWeight = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyItemAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyItemAnswers_SurveyItems_SurveyItemId",
                        column: x => x.SurveyItemId,
                        principalTable: "SurveyItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListNewsItems_ListNewsId",
                table: "ListNewsItems",
                column: "ListNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyItemAnswers_SurveyItemId",
                table: "SurveyItemAnswers",
                column: "SurveyItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyItems_SurveyId",
                table: "SurveyItems",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResultItems_SurveyId",
                table: "SurveyResultItems",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListNewsItems");

            migrationBuilder.DropTable(
                name: "SurveyItemAnswers");

            migrationBuilder.DropTable(
                name: "SurveyResultItems");

            migrationBuilder.DropTable(
                name: "SurveyItems");

            migrationBuilder.DropTable(
                name: "MainFeeds");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "CoffeeFortuneTellings");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoffeePoint",
                table: "AspNetUsers");
        }
    }
}
