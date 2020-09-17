using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class PointValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointValue",
                table: "Points",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BuyPointTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    SubmitDateUtc = table.Column<DateTime>(nullable: false),
                    PointId = table.Column<Guid>(nullable: true),
                    PointValue = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    TransactionJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyPointTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyPointTransactions_Points_PointId",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyPointTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyPointTransactions_PointId",
                table: "BuyPointTransactions",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyPointTransactions_UserId",
                table: "BuyPointTransactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyPointTransactions");

            migrationBuilder.DropColumn(
                name: "PointValue",
                table: "Points");
        }
    }
}
