using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class Distances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FloorSwitcherPointDistances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorSwitcherPoint1Id = table.Column<int>(type: "int", nullable: true),
                    FloorSwitcherPoint2Id = table.Column<int>(type: "int", nullable: true),
                    Distance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorSwitcherPointDistances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FloorSwitcherPointDistances_FloorSwitcherPoints_FloorSwitcherPoint1Id",
                        column: x => x.FloorSwitcherPoint1Id,
                        principalTable: "FloorSwitcherPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FloorSwitcherPointDistances_FloorSwitcherPoints_FloorSwitcherPoint2Id",
                        column: x => x.FloorSwitcherPoint2Id,
                        principalTable: "FloorSwitcherPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TargetDistances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorSwitcherPointId = table.Column<int>(type: "int", nullable: true),
                    TargetId = table.Column<int>(type: "int", nullable: true),
                    Distance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetDistances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetDistances_FloorSwitcherPoints_FloorSwitcherPointId",
                        column: x => x.FloorSwitcherPointId,
                        principalTable: "FloorSwitcherPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetDistances_Targets_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Targets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FloorSwitcherPointDistances_FloorSwitcherPoint1Id",
                table: "FloorSwitcherPointDistances",
                column: "FloorSwitcherPoint1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FloorSwitcherPointDistances_FloorSwitcherPoint2Id",
                table: "FloorSwitcherPointDistances",
                column: "FloorSwitcherPoint2Id");

            migrationBuilder.CreateIndex(
                name: "IX_TargetDistances_FloorSwitcherPointId",
                table: "TargetDistances",
                column: "FloorSwitcherPointId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetDistances_TargetId",
                table: "TargetDistances",
                column: "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FloorSwitcherPointDistances");

            migrationBuilder.DropTable(
                name: "TargetDistances");
        }
    }
}
