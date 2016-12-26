using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace losol.EventR.Data.Migrations
{
    public partial class Datesplit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "EventInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "EventInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EventInfo");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "EventInfo");
        }
    }
}
