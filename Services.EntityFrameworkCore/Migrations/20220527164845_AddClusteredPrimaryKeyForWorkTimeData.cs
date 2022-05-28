using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.EntityFrameworkCore.Migrations
{
    public partial class AddClusteredPrimaryKeyForWorkTimeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTimeData",
                table: "WorkTimeData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTimeData",
                table: "WorkTimeData",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTimeData",
                table: "WorkTimeData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTimeData",
                table: "WorkTimeData",
                column: "Id");
        }
    }
}
