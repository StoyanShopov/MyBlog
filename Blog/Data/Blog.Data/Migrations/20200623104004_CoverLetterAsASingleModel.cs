using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class CoverLetterAsASingleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CoverLetter");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CoverLetter");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CoverLetter");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CoverLetter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CoverLetter",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CoverLetter",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CoverLetter",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CoverLetter",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
