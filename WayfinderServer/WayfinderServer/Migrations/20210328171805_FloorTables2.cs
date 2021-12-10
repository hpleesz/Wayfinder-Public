using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class FloorTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floors_floorPlan3Ds_FloorPlan3DId",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_FloorPlan3DId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "FloorPlan3DId",
                table: "Floors");

            migrationBuilder.AddColumn<int>(
                name: "FloorId",
                table: "floorPlan3Ds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_floorPlan3Ds_FloorId",
                table: "floorPlan3Ds",
                column: "FloorId",
                unique: true,
                filter: "[FloorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_floorPlan3Ds_Floors_FloorId",
                table: "floorPlan3Ds",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_floorPlan3Ds_Floors_FloorId",
                table: "floorPlan3Ds");

            migrationBuilder.DropIndex(
                name: "IX_floorPlan3Ds_FloorId",
                table: "floorPlan3Ds");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "floorPlan3Ds");

            migrationBuilder.AddColumn<int>(
                name: "FloorPlan3DId",
                table: "Floors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_FloorPlan3DId",
                table: "Floors",
                column: "FloorPlan3DId");

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_floorPlan3Ds_FloorPlan3DId",
                table: "Floors",
                column: "FloorPlan3DId",
                principalTable: "floorPlan3Ds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
