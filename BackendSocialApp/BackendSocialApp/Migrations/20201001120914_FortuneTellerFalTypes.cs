using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class FortuneTellerFalTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FortuneTellerFalTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FortuneTellerId = table.Column<Guid>(nullable: true),
                    FortunrTellingType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FortuneTellerFalTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FortuneTellerFalTypes_AspNetUsers_FortuneTellerId",
                        column: x => x.FortuneTellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FortuneTellerFalTypes_FortuneTellerId",
                table: "FortuneTellerFalTypes",
                column: "FortuneTellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FortuneTellerFalTypes");
        }
    }
}
