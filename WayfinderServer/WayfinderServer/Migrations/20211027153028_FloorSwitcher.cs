using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class FloorSwitcher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FloorSwitcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Up = table.Column<bool>(type: "bit", nullable: false),
                    Down = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorSwitcher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FloorSwitcher_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FloorSwitcherPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorId = table.Column<int>(type: "int", nullable: true),
                    FloorSwitcherId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XCoordinate = table.Column<float>(type: "real", nullable: false),
                    YCoordinate = table.Column<float>(type: "real", nullable: false),
                    ZCoordinate = table.Column<float>(type: "real", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorSwitcherPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FloorSwitcherPoint_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FloorSwitcherPoint_FloorSwitcher_FloorSwitcherId",
                        column: x => x.FloorSwitcherId,
                        principalTable: "FloorSwitcher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FloorSwitcherPoint_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FloorSwitcher_PlaceId",
                table: "FloorSwitcher",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FloorSwitcherPoint_FloorId",
                table: "FloorSwitcherPoint",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_FloorSwitcherPoint_FloorSwitcherId",
                table: "FloorSwitcherPoint",
                column: "FloorSwitcherId");

            migrationBuilder.CreateIndex(
                name: "IX_FloorSwitcherPoint_PlaceId",
                table: "FloorSwitcherPoint",
                column: "PlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FloorSwitcherPoint");

            migrationBuilder.DropTable(
                name: "FloorSwitcher");
        }
    }
}
