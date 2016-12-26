using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace losol.EventR.Data.Migrations
{
    public partial class EventInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DiplomaDescription = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    LastEnrolmentDate = table.Column<DateTime>(nullable: true),
                    LastWithdrawalDate = table.Column<DateTime>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    MaxAttendees = table.Column<int>(nullable: false),
                    MoreInformation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Publish = table.Column<bool>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    VatPercent = table.Column<decimal>(nullable: false),
                    WelcomeLetter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInfo");
        }
    }
}
