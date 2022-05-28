using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.EntityFrameworkCore.Migrations
{
    public partial class ReplaceWorkDuractionColumnWithStartStopTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpentTime",
                table: "WorkTimeData",
                newName: "StopTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "WorkTimeData",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "WorkTimeData");

            migrationBuilder.RenameColumn(
                name: "StopTime",
                table: "WorkTimeData",
                newName: "SpentTime");
        }
    }
}
