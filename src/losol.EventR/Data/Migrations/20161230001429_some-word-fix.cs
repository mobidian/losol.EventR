using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace losol.EventR.Data.Migrations
{
    public partial class somewordfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxAttendees",
                table: "EventInfo",
                newName: "MaxParticipants");

            migrationBuilder.RenameColumn(
                name: "LastWithdrawalDate",
                table: "EventInfo",
                newName: "LastRegistrationDate");

            migrationBuilder.RenameColumn(
                name: "LastEnrolmentDate",
                table: "EventInfo",
                newName: "LastCancellationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipants",
                table: "EventInfo",
                newName: "MaxAttendees");

            migrationBuilder.RenameColumn(
                name: "LastRegistrationDate",
                table: "EventInfo",
                newName: "LastWithdrawalDate");

            migrationBuilder.RenameColumn(
                name: "LastCancellationDate",
                table: "EventInfo",
                newName: "LastEnrolmentDate");
        }
    }
}
