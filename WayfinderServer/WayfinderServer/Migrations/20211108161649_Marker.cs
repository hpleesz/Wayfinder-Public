using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class Marker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudAnchorId",
                table: "Markers");

            migrationBuilder.AddColumn<float>(
                name: "XRotation",
                table: "Markers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "YRotation",
                table: "Markers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ZRotation",
                table: "Markers",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XRotation",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "YRotation",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "ZRotation",
                table: "Markers");

            migrationBuilder.AddColumn<string>(
                name: "CloudAnchorId",
                table: "Markers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
