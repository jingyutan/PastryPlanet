using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PastryPlanet.Migrations
{
    public partial class AuditAddDatetimeuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeStamp",
                table: "ProductAudit",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ProductAudit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeStamp",
                table: "ProductAudit");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ProductAudit");
        }
    }
}
