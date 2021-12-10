using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class TargetRotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "XRotation",
                table: "Targets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "YRotation",
                table: "Targets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ZRotation",
                table: "Targets",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XRotation",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "YRotation",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "ZRotation",
                table: "Targets");
        }
    }
}
