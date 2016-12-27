using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace losol.EventR.Data.Migrations
{
    public partial class addedCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "EventInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "EventInfo");
        }
    }
}
