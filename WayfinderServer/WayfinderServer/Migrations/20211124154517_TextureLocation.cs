using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class TextureLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextureLocation",
                table: "VirtualObjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextureLocation",
                table: "VirtualObjects");
        }
    }
}
