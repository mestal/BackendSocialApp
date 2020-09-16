using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendSocialApp.Migrations
{
    public partial class FalUserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConsumerBirthDate",
                table: "CoffeeFortuneTellings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConsumerBirthTime",
                table: "CoffeeFortuneTellings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConsumerGender",
                table: "CoffeeFortuneTellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsumerJob",
                table: "CoffeeFortuneTellings",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConsumerRelationshipStatus",
                table: "CoffeeFortuneTellings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumerBirthDate",
                table: "CoffeeFortuneTellings");

            migrationBuilder.DropColumn(
                name: "ConsumerBirthTime",
                table: "CoffeeFortuneTellings");

            migrationBuilder.DropColumn(
                name: "ConsumerGender",
                table: "CoffeeFortuneTellings");

            migrationBuilder.DropColumn(
                name: "ConsumerJob",
                table: "CoffeeFortuneTellings");

            migrationBuilder.DropColumn(
                name: "ConsumerRelationshipStatus",
                table: "CoffeeFortuneTellings");
        }
    }
}
