using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class CoffeeFortuneTelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeFortuneTellings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    FortuneTellerId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    FortuneTellerComment = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    SubmitDateUtc = table.Column<DateTime>(nullable: true),
                    SubmitByFortuneTellerDateUtc = table.Column<DateTime>(nullable: true),
                    ReadDateUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeFortuneTellings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoffeeFortuneTellings_AspNetUsers_FortuneTellerId",
                        column: x => x.FortuneTellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoffeeFortuneTellings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoffeeFortuneTellingPictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    CoffeeFortuneTellingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeFortuneTellingPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoffeeFortuneTellingPictures_CoffeeFortuneTellings_CoffeeFortuneTellingId",
                        column: x => x.CoffeeFortuneTellingId,
                        principalTable: "CoffeeFortuneTellings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeFortuneTellingPictures_CoffeeFortuneTellingId",
                table: "CoffeeFortuneTellingPictures",
                column: "CoffeeFortuneTellingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeFortuneTellings_FortuneTellerId",
                table: "CoffeeFortuneTellings",
                column: "FortuneTellerId");

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeFortuneTellings_UserId",
                table: "CoffeeFortuneTellings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeFortuneTellingPictures");

            migrationBuilder.DropTable(
                name: "CoffeeFortuneTellings");
        }
    }
}
